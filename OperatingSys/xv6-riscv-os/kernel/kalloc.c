// Physical memory allocator, for user processes,
// kernel stacks, page-table pages,
// and pipe buffers. Allocates whole 4096-byte pages.

#include "types.h"
#include "param.h"
#include "memlayout.h"
#include "spinlock.h"
#include "riscv.h"
#include "defs.h"

void freerange(void *pa_start, void *pa_end);
void heap_init(void *heapEnd, int size);

extern char end[]; // first address after kernel.
                   // defined by kernel.ld.

struct run {
  struct run *next;
};

struct {
  struct spinlock lock;
  struct run *freelist;
} kmem;

void
kinit()     //初始化内存空间
{
  void *heapEnd = (void*)(PHYSTOP - HEAP);
  initlock(&kmem.lock, "kmem");
  freerange(end, (void*)PHYSTOP);
  heap_init(heapEnd, HEAP);//初始化堆
}

void
freerange(void *pa_start, void *pa_end)
{
  char *p;
  p = (char*)PGROUNDUP((uint64)pa_start);
  for(; p + PGSIZE <= (char*)pa_end; p += PGSIZE)
    kfree(p);
}

// Free the page of physical memory pointed at by pa,
// which normally should have been returned by a
// call to kalloc().  (The exception is when
// initializing the allocator; see kinit above.)
void
kfree(void *pa)
{
  struct run *r;

  if(((uint64)pa % PGSIZE) != 0 || (char*)pa < end || (uint64)pa >= PHYSTOP)
    panic("kfree");

  // Fill with junk to catch dangling refs.
  memset(pa, 1, PGSIZE);

  r = (struct run*)pa;

  acquire(&kmem.lock);
  r->next = kmem.freelist;
  kmem.freelist = r;
  release(&kmem.lock);
}

// Allocate one 4096-byte page of physical memory.
// Returns a pointer that the kernel can use.
// Returns 0 if the memory cannot be allocated.
void *
kalloc(void)
{
  struct run *r;

  acquire(&kmem.lock);
  r = kmem.freelist;
  if(r)
    kmem.freelist = r->next;
  release(&kmem.lock);

  if(r)
    memset((char*)r, 5, PGSIZE); // fill with junk
  return (void*)r;
}



struct heap_block {       
    int size;             
    int free;             
    struct heap_block *next;  
};

struct {                 
    struct spinlock lock; 
    struct heap_block *freeList;  
} heap;


void heap_init(void *heapEnd, int size) { 
    initlock(&heap.lock, "heap"); 
    heap.freeList = (struct heap_block*)heapEnd; 
    heap.freeList->size = size - sizeof(struct heap_block);
    heap.freeList->next = 0; 
    heap.freeList->free = 1; // 1 is free
}

void *malloc(int size) { 
    struct heap_block *curr;
    void *result = 0;
    acquire(&heap.lock); 

    for (curr = heap.freeList; curr != 0; curr = curr->next) {
        if (curr->free && curr->size > size + sizeof(struct heap_block)) {
           
            struct heap_block *newBlock = (struct heap_block*)((char*)curr + sizeof(struct heap_block) + size);
            newBlock->size = curr->size - size - sizeof(struct heap_block);
            newBlock->next = curr->next;
            newBlock->free = 1;

            curr->size = size + sizeof(struct heap_block);
            curr->next = newBlock;
            curr->free = 0;
            result = (void*)((char*)curr); // return the address allocated
            break;
        }
       else{
        printf("This block is not fitted, goes to the nest block\n");
       }
    }
if(curr == 0){
printf("Allocation failed\n");
}
    release(&heap.lock); 
    return result;
}



void *mallocBFS(int size){
    struct heap_block *curr;
    struct heap_block *bestFit = 0;      //bestFit uesd for mark
    int minDif = HEAP;      
    void *result = 0;

    acquire(&heap.lock);
   
    for (curr = heap.freeList; curr != 0; curr = curr->next) {      //find
        if (curr->free && curr->size >= size + sizeof(struct heap_block)) {
            int sizeDif = curr->size - size - sizeof(struct heap_block);

            if (sizeDif < minDif) {
                bestFit = curr;
                minDif = sizeDif;
            }
        }
    }


    if (bestFit) {
       
        if (bestFit->size == size + sizeof(struct heap_block)) {
            bestFit->free = 0;
        } else {
            struct heap_block *newBlock = (struct heap_block*)((char*)bestFit + sizeof(struct heap_block) + size);
            newBlock->size = bestFit->size - size - sizeof(struct heap_block);
            newBlock->next = bestFit->next;
            newBlock->free = 1;

            bestFit->size = size + sizeof(struct heap_block);
            bestFit->next = newBlock;
            bestFit->free = 0;
        }
        result = (void*)((char*)bestFit);
    }else{
      printf("No block could be allocated\n");
     }

    release(&heap.lock);
    return result;
}

void *mallocWFS(int size){
    struct heap_block *curr;
    struct heap_block *worstFit = 0;   
    int maxDif = -1;             // init is -1 indicating that doesnt find the fit block
    void *result = 0;

    acquire(&heap.lock);
    
    for (curr = heap.freeList; curr != 0; curr = curr->next) {
        if (curr->free && curr->size >= size + sizeof(struct heap_block)) {
            int sizeDif = curr->size - size - sizeof(struct heap_block);

            if (sizeDif > maxDif) {
                worstFit = curr;
                maxDif = sizeDif;
            }
        }
    }

    if (worstFit) {
        if (worstFit->size == size + sizeof(struct heap_block)) {
            worstFit->free = 0;
        } else {
            struct heap_block *newBlock = (struct heap_block*)((char*)worstFit + sizeof(struct heap_block) + size);
            newBlock->size = worstFit->size - size - sizeof(struct heap_block);
            newBlock->next = worstFit->next;
            newBlock->free = 1;

            worstFit->size = size + sizeof(struct heap_block);
            worstFit->next = newBlock;
            worstFit->free = 0;
        }
        result = (void*)((char*)worstFit);
    } else {
        printf("No block could be allocated\n");
    }

    release(&heap.lock);
    return result;
}


void free(void *ptr) {

    if(!ptr) return;
    struct heap_block *block = (struct heap_block*)((char*)ptr); // find the head of the heap
    acquire(&heap.lock);
    block->free = 1; // set the block free

    // mix the neighboured blocks
   struct heap_block *curr = heap.freeList;
    while (curr != 0){
        if (curr->free && curr->next && curr->next->free) {
            curr->size += curr->next->size;
            curr->next = curr->next->next;
        } else {
            curr = curr->next;
        }
    }


    release(&heap.lock);
}


void printheap() {
    acquire(&heap.lock);
    
    for (struct heap_block *curr = heap.freeList; curr != 0; curr = curr->next) {
      int freeStatus=curr->free;
      if(freeStatus==1)
        printf("The address is  %p ,the block is FREE,the size is %d\n", curr,curr->size);
      else
        printf("The address is  %p ,the block is ALLOC,the size is %d\n", curr,curr->size);
    }
    release(&heap.lock);
}

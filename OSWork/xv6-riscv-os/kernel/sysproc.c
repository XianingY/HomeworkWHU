#include "types.h"
#include "riscv.h"
#include "defs.h"
#include "param.h"
#include "memlayout.h"
#include "spinlock.h"
#include "proc.h"

#include "stat.h"



uint64
sys_exit(void)
{
  int n;
  argint(0, &n);
  exit(n);
  return 0;  // not reached
}

uint64
sys_getpid(void)
{
  return myproc()->pid;
}

uint64
sys_fork(void)
{
  return fork();
}

uint64
sys_wait(void)
{
  uint64 p;
  argaddr(0, &p);
  return wait(p);
}

uint64
sys_sbrk(void)
{
  uint64 addr;
  int n;

  argint(0, &n);
  addr = myproc()->sz;
  if(growproc(n) < 0)
    return -1;
  return addr;
}

uint64
sys_sleep(void)
{
  int n;
  uint ticks0;

  argint(0, &n);
  acquire(&tickslock);
  ticks0 = ticks;
  while(ticks - ticks0 < n){
    if(killed(myproc())){
      release(&tickslock);
      return -1;
    }
    sleep(&ticks, &tickslock);
  }
  release(&tickslock);
  return 0;
}

uint64
sys_kill(void)
{
  int pid;

  argint(0, &pid);
  return kill(pid);
}

// return how many clock tick interrupts have occurred
// since start.
uint64
sys_uptime(void)
{
  uint xticks;

  acquire(&tickslock);
  xticks = ticks;
  release(&tickslock);
  return xticks;
}

int sys_getprocs(void) 
{
  return getprocs();
}

struct Allocation{
  int size;
  void *dst;
  int isAllocate;
};

// //1.Random small but huge allocation    may be improved
// #define LOOP 50
// uint64 sys_heap(void) {
//   struct Allocation alloc[LOOP];
//   for(int i=0;i<LOOP;i++) alloc[i].isAllocate=0; //init allocation status

//   void *dst[LOOP];
//   unsigned int next = 3;

//   printheap();    //check the init status
//   for(int i =0;i<LOOP;i++){
//     printf("The %d loop(allocate):\n",i);

    
//     next = next * 1073 ;     //random init
    
//     alloc[i].size=next%1024;
//     alloc[i].dst = malloc(alloc[i].size);
//     alloc[i].isAllocate=1;
//     printf("Allocates %d bytes at %p\n",alloc[i].size,alloc[i],dst);
//     printheap();        
//   }

  //   for(int i =LOOP/2;i<LOOP;i++){    //free half randomly
  //   printf("The %d loop(free):\n",i);
  //   int freeDst=(i*11073/10) %LOOP;
  //   printf("%d %d\n",freeDst,alloc[freeDst].isAllocate); 
    
  //   while(alloc[freeDst].isAllocate==0){
  //     freeDst=(freeDst+1)%(LOOP/2) +LOOP/2; 
  //     printf("1"); //find a free dst
  //   }
  //   free(alloc[freeDst].dst);
  //   alloc[freeDst].isAllocate=0;
  //   printf("Free %d bytes from %p\n",alloc[freeDst].size,alloc[freeDst].dst);
  //   printheap();    
  // }


//    return 0;
// }

// //2.Large and small mixed allocation
// #define LOOP 10
// uint64 sys_heap(void) {

//   struct Allocation alloc[LOOP];
//   unsigned int next = 3;

//   printheap();    //check the init status
//   for(int i =0;i<LOOP;i++){
//     printf("The %d loop:\n",i);

    
//     next = next * 11073 /10;     //depends on the second
    
//     if(next%4 <= 1){
//       alloc[i].size=next%1024;   //small block
//       }
//     else{
//       alloc[i].size=next%1024*1024; //large block
//       }
//     alloc[i].dst= malloc(alloc[i].size);
//     printf("Allocates %d bytes at %p\n",alloc[i].size,alloc[i].dst);
//     printheap();        
//   }
//    return 0;
// }

// //3.Fixed allocation 1024 ,in the end try over-large block
// #define LOOP 10
// uint64 sys_heap(void) {
//   struct Allocation alloc[LOOP];


//   printheap();    //check the init status
//   for(int i =0;i<LOOP-1;i++){
//     printf("The %d loop:\n",i);

    
   
//     alloc[i].size=1024;         //the size is 1024+16=1040
//     alloc[i].dst = malloc(alloc[i].size);
//     printf("Allocates %d bytes at %p\n",alloc[i].size,alloc[i].dst);
//     printheap();        
//   }

//   printf("The %d loop:\n",LOOP-1);
//   alloc[LOOP-1].size=HEAP+1;
//   alloc[LOOP-1].dst = malloc(alloc[LOOP-1].size);
//   printf("Allocates %d bytes at %p\n",alloc[LOOP-1].size,alloc[LOOP-1].dst);
//   printheap();
//   return 0;
// }


// //4.Test Free
// #define LOOP 10   //to be even
// uint64 sys_heap(void) {

//   struct Allocation alloc[LOOP];
//   for(int i=0;i<LOOP;i++) alloc[i].isAllocate=0; //init allocation status
//   unsigned int next = 3;

//   printheap();    //check the init status
//   for(int i =0;i<LOOP;i++){
    
//     printf("The %d loop(alloc):\n",i);

    
//     next = next * 11073 /10;     //depends on the second
    
//     if(next%4 <= 1){
//       alloc[i].size=next%1024;   //small block
//       }
//     else{
//       alloc[i].size=next%1024*1024; //large block
//       }
//     alloc[i].dst= malloc(alloc[i].size);
//     alloc[i].isAllocate=1;
//     printf("Allocates %d bytes at %p\n",alloc[i].size,alloc[i].dst);
//     printheap();        
//   }
  

//   for(int i =0;i<LOOP/2;i++){     //free half sequently
//     printf("The %d loop(free):\n",i);
//     free(alloc[i].dst);
//     alloc[i].isAllocate=0;
//     printf("Free %d bytes from %p\n",alloc[i].size,alloc[i].dst);
//     printheap();
//   }

//   for(int i =LOOP/2;i<LOOP;i++){    //free half randomly
//     printf("The %d loop(free):\n",i);
//     int freeDst=(i*11073/10) %(LOOP/2) +LOOP/2;
//     printf("%d %d\n",freeDst,alloc[freeDst].isAllocate); 
    
//     while(alloc[freeDst].isAllocate==0){
//       freeDst=(freeDst+1)%(LOOP/2) +LOOP/2; 
//       printf("1"); //find a free dst
//     }
//     free(alloc[freeDst].dst);
//     alloc[freeDst].isAllocate=0;
//     printf("Free %d bytes from %p\n",alloc[freeDst].size,alloc[freeDst].dst);
//     printheap();
//   }

//    return 0;
// }


//5.Test BFS
#define LOOP 11
uint64 sys_heap(void) {

  struct Allocation alloc[LOOP];

  printheap();
 

  for(int i=0;i<LOOP;i++){

    printf("The %d loop(allocate):\n",i);
    alloc[i].size=2<<(LOOP-i);
    alloc[i].dst = mallocBFS(alloc[i].size);

    printf("Allocate %d bytes at %p\n",alloc[i].size,alloc[i].dst);
    printheap();
  }

  for(int i=0;i<LOOP;i++){
    printf("The %d loop(free):\n",i);
    free(alloc[i].dst);
    printf("Free %d bytes from %p\n",alloc[i].size,alloc[i].dst);
    printheap();
  }

  for(int i=0;i<LOOP;i++){

    printf("The %d loop(allocate):\n",i);
    alloc[i].size=2<<(i+1);
    alloc[i].dst = mallocBFS(alloc[i].size);

    printf("Allocate %d bytes at %p\n",alloc[i].size,alloc[i].dst);
    printheap();
  }

   return 0;
}

// //6.Test WFS
// #define LOOP 11

// uint64 sys_heap(void) {
//     struct Allocation alloc[LOOP];

//     printheap();

    
//     for (int i = 0; i < LOOP; i++) {
//         printf("The %d loop(allocate):\n", i);
//         alloc[i].size = 2 << (LOOP - i);
//         alloc[i].dst = mallocWFS(alloc[i].size);

//         printf("Allocate %d bytes at %p\n", alloc[i].size, alloc[i].dst);
//         printheap();
//     }

    
//     for (int i = 0; i < LOOP; i++) {
//         printf("The %d loop(free):\n", i);
//         free(alloc[i].dst);
//         printf("Free %d bytes from %p\n", alloc[i].size, alloc[i].dst);
//         printheap();
//     }

    
//     for (int i = 0; i < LOOP; i++) {
//         printf("The %d loop(allocate):\n", i);
//         alloc[i].size = 2 << (i + 1);
//         alloc[i].dst = mallocWFS(alloc[i].size);

//         printf("Allocate %d bytes at %p\n", alloc[i].size, alloc[i].dst);
//         printheap();
//     }

//     return 0;
// }


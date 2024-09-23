using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyDataStruct
{
    [Serializable]
    public struct MessageStruct
    {
        public MessageType Type;
        public string Content;
        public DateTime Timestamp;
    }
}

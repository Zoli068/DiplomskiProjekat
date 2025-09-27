using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FIFOQueue
{
    public class FIFOQueue : IFIFOQueue
    {
        private Queue<short> fifoQueue;

        public FIFOQueue()
        {
            fifoQueue = new Queue<short>();
            Init();
        }

        public short[] GetValuesFromQueue(ushort startingAddress)
        {
            List<short> responseValues = new List<short>();

            if(!CheckAddress(startingAddress))
            {
                return null;
            }

            int i = 0;

            while((startingAddress+i)<fifoQueue.Count())
            { 
                responseValues.Add(fifoQueue.ElementAt(startingAddress+i));
                i++;
            }

            return responseValues.ToArray();
        }

        public bool CheckAddress(ushort address)
        {
            if(fifoQueue.Count - 1 >= address)
            {
                if(fifoQueue.Count - address > 31)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        private void Init()
        {
            fifoQueue.Enqueue(100);
            fifoQueue.Enqueue(200);
            fifoQueue.Enqueue(300);
            fifoQueue.Enqueue(400);
            fifoQueue.Enqueue(500);
            fifoQueue.Enqueue(600);
            fifoQueue.Enqueue(700);
            fifoQueue.Enqueue(800);
        }
    }
}

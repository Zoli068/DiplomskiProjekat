using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FIFOQueue
{
    public interface IFIFOQueue
    {
        short[] GetValuesFromQueue(ushort startingAddress);

        bool CheckAddress(ushort address);
    }
}

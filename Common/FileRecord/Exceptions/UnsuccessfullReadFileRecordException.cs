using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FileRecord
{
    public class UnsuccessfullReadFileRecordException:Exception
    {
        public UnsuccessfullReadFileRecordException() : base("Read file record was unsuccessfull") { }

        public UnsuccessfullReadFileRecordException(string message) : base(message) { }
    }
}

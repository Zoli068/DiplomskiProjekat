using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FileRecord
{
    public class UnsuccessfullWriteFileRecordException : Exception
    {
        public UnsuccessfullWriteFileRecordException() : base("Write file record was unsuccessfull") { }

        public UnsuccessfullWriteFileRecordException(string message) : base(message) { }
    }
}

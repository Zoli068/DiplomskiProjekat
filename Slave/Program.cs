using System;

namespace Slave
{
    public class Program
    {
        static void Main(string[] args)
        {
            Slave slave = new Slave();
            slave.Start();
            Console.ReadKey();
        }
    }
}

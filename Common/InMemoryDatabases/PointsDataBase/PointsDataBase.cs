using Common.IPointsDataBase;
using System.Collections.Generic;

namespace Common.PointsDataBase
{
    /// <summary>
    /// Implementation of <see cref="IPointsDataBase"/> interface, in memory database for modbus points
    /// </summary>
    public class PointsDataBase : IPointsDataBase.IPointsDataBase
    {
        private Dictionary<ushort, (PointsType, short)> RegistersDictionary = new Dictionary<ushort, (PointsType, short)>();
        private Dictionary<ushort, (PointsType, byte)> DiscreteDictionary = new Dictionary<ushort, (PointsType, byte)>();

        public PointsDataBase()
        {
            InitializePoints();
        }

        public bool CheckAddress(ushort address)
        {
            bool addressIsReal = false;

            if (DiscreteDictionary.ContainsKey(address) || RegistersDictionary.ContainsKey(address))
            {
                addressIsReal = true;
            }

            return addressIsReal;
        }

        public byte ReadDiscreteValue(ushort address, PointsType pointType)
        {
            (PointsType, byte) value;

            if (DiscreteDictionary.TryGetValue(address, out value))
            {
                if (pointType == value.Item1)
                {
                    return value.Item2;
                }
                else
                {
                    throw new PointTypeDifferenceException();
                }
            }
            else
            {
                throw new InvalidAddressException();
            }
        }

        public short ReadRegisterValue(ushort address, PointsType pointType)
        {
            (PointsType, short) value;

            if (RegistersDictionary.TryGetValue(address, out value))
            {
                if (pointType == value.Item1)
                {
                    return value.Item2;
                }
                else
                {
                    throw new PointTypeDifferenceException();
                }
            }
            else
            {
                throw new InvalidAddressException();
            }
        }

        public void WriteDiscreteValue(ushort address, PointsType pointType, byte value)
        {
            (PointsType, byte) point;

            if (!DiscreteDictionary.TryGetValue(address, out point))
            {
                throw new InvalidAddressException();
            }

            if (point.Item1 == PointsType.COILS)
            {
                if (point.Item1 == pointType)
                {
                    point.Item2 = value;
                    DiscreteDictionary[address] = point;
                }
                else
                {
                    throw new PointTypeDifferenceException();
                }
            }
            else
            {
                throw new CantWriteInputException();
            }

        }

        public void WriteRegisterValue(ushort address, PointsType pointType, short value)
        {
            (PointsType, short) point;

            if (RegistersDictionary.TryGetValue(address, out point))
            {
                if (point.Item1 == PointsType.HOLDING_REGISTERS)
                {
                    if (point.Item1 == pointType)
                    {
                        point.Item2 = value;
                        RegistersDictionary[address] = point;
                    }
                    else
                    {
                        throw new PointTypeDifferenceException();
                    }
                }
                else
                {
                    throw new CantWriteInputException();
                }
            }
            else
            {
                throw new InvalidAddressException();
            }
        }

        private void InitializePoints()
        {
            // Discrete Outputs (Coils) - Addresses 00001 to 10000
            DiscreteDictionary.Add(1, (PointsType.COILS, 1));
            DiscreteDictionary.Add(2, (PointsType.COILS, 0));
            DiscreteDictionary.Add(3, (PointsType.COILS, 1));
            DiscreteDictionary.Add(4, (PointsType.COILS, 0));
            DiscreteDictionary.Add(5, (PointsType.COILS, 1));
            DiscreteDictionary.Add(6, (PointsType.COILS, 1));
            DiscreteDictionary.Add(7, (PointsType.COILS, 0));
            DiscreteDictionary.Add(8, (PointsType.COILS, 1));
            DiscreteDictionary.Add(9, (PointsType.COILS, 1));
            DiscreteDictionary.Add(10, (PointsType.COILS, 0));
            DiscreteDictionary.Add(11, (PointsType.COILS, 1));
            DiscreteDictionary.Add(12, (PointsType.COILS, 0));
            DiscreteDictionary.Add(13, (PointsType.COILS, 1));
            DiscreteDictionary.Add(14, (PointsType.COILS, 0));
            DiscreteDictionary.Add(15, (PointsType.COILS, 1));
            DiscreteDictionary.Add(16, (PointsType.COILS, 1));
            DiscreteDictionary.Add(17, (PointsType.COILS, 0));
            DiscreteDictionary.Add(18, (PointsType.COILS, 1));
            DiscreteDictionary.Add(19, (PointsType.COILS, 0));
            DiscreteDictionary.Add(20, (PointsType.COILS, 1));

            // Discrete Inputs - Addresses 10001 to 20000
            DiscreteDictionary.Add(10001, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10002, (PointsType.DISCRETE_INPUTS, 0));
            DiscreteDictionary.Add(10003, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10004, (PointsType.DISCRETE_INPUTS, 0));
            DiscreteDictionary.Add(10005, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10006, (PointsType.DISCRETE_INPUTS, 0));
            DiscreteDictionary.Add(10007, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10008, (PointsType.DISCRETE_INPUTS, 0));
            DiscreteDictionary.Add(10009, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10010, (PointsType.DISCRETE_INPUTS, 0));
            DiscreteDictionary.Add(10011, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10012, (PointsType.DISCRETE_INPUTS, 0));
            DiscreteDictionary.Add(10013, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10014, (PointsType.DISCRETE_INPUTS, 0));
            DiscreteDictionary.Add(10015, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10016, (PointsType.DISCRETE_INPUTS, 0));
            DiscreteDictionary.Add(10017, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10018, (PointsType.DISCRETE_INPUTS, 0));
            DiscreteDictionary.Add(10019, (PointsType.DISCRETE_INPUTS, 1));
            DiscreteDictionary.Add(10020, (PointsType.DISCRETE_INPUTS, 0));

            // Input Registers - Addresses 30001 to 40000
            RegistersDictionary.Add(30001, (PointsType.INPUT_REGISTERS, 100));
            RegistersDictionary.Add(30002, (PointsType.INPUT_REGISTERS, 200));
            RegistersDictionary.Add(30003, (PointsType.INPUT_REGISTERS, 300));
            RegistersDictionary.Add(30004, (PointsType.INPUT_REGISTERS, 400));
            RegistersDictionary.Add(30005, (PointsType.INPUT_REGISTERS, 500));
            RegistersDictionary.Add(30006, (PointsType.INPUT_REGISTERS, 600));
            RegistersDictionary.Add(30007, (PointsType.INPUT_REGISTERS, 700));
            RegistersDictionary.Add(30008, (PointsType.INPUT_REGISTERS, 800));
            RegistersDictionary.Add(30009, (PointsType.INPUT_REGISTERS, 900));
            RegistersDictionary.Add(30010, (PointsType.INPUT_REGISTERS, 1000));
            RegistersDictionary.Add(30011, (PointsType.INPUT_REGISTERS, 1100));
            RegistersDictionary.Add(30012, (PointsType.INPUT_REGISTERS, 1200));
            RegistersDictionary.Add(30013, (PointsType.INPUT_REGISTERS, 1300));
            RegistersDictionary.Add(30014, (PointsType.INPUT_REGISTERS, 1400));
            RegistersDictionary.Add(30015, (PointsType.INPUT_REGISTERS, 1500));
            RegistersDictionary.Add(30016, (PointsType.INPUT_REGISTERS, 1600));
            RegistersDictionary.Add(30017, (PointsType.INPUT_REGISTERS, 1700));
            RegistersDictionary.Add(30018, (PointsType.INPUT_REGISTERS, 1800));
            RegistersDictionary.Add(30019, (PointsType.INPUT_REGISTERS, 1900));
            RegistersDictionary.Add(30020, (PointsType.INPUT_REGISTERS, 2000));

            // Holding Registers - Addresses 40001 to 50000
            RegistersDictionary.Add(40001, (PointsType.HOLDING_REGISTERS, 1000));
            RegistersDictionary.Add(40002, (PointsType.HOLDING_REGISTERS, 1100));
            RegistersDictionary.Add(40003, (PointsType.HOLDING_REGISTERS, 1200));
            RegistersDictionary.Add(40004, (PointsType.HOLDING_REGISTERS, 1300));
            RegistersDictionary.Add(40005, (PointsType.HOLDING_REGISTERS, 1400));
            RegistersDictionary.Add(40006, (PointsType.HOLDING_REGISTERS, 1500));
            RegistersDictionary.Add(40007, (PointsType.HOLDING_REGISTERS, 1600));
            RegistersDictionary.Add(40008, (PointsType.HOLDING_REGISTERS, 1700));
            RegistersDictionary.Add(40009, (PointsType.HOLDING_REGISTERS, 1800));
            RegistersDictionary.Add(40010, (PointsType.HOLDING_REGISTERS, 1900));
            RegistersDictionary.Add(40011, (PointsType.HOLDING_REGISTERS, 2000));
            RegistersDictionary.Add(40012, (PointsType.HOLDING_REGISTERS, 2100));
            RegistersDictionary.Add(40013, (PointsType.HOLDING_REGISTERS, 2200));
            RegistersDictionary.Add(40014, (PointsType.HOLDING_REGISTERS, 2300));
            RegistersDictionary.Add(40015, (PointsType.HOLDING_REGISTERS, 2400));
            RegistersDictionary.Add(40016, (PointsType.HOLDING_REGISTERS, 2500));
            RegistersDictionary.Add(40017, (PointsType.HOLDING_REGISTERS, 2600));
            RegistersDictionary.Add(40018, (PointsType.HOLDING_REGISTERS, 2700));
            RegistersDictionary.Add(40019, (PointsType.HOLDING_REGISTERS, 2800));
            RegistersDictionary.Add(40020, (PointsType.HOLDING_REGISTERS, 2900));
        }
    }
}
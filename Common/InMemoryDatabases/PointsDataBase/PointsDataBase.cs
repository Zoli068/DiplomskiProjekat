using Common.IPointsDataBase;
using System;
using System.Collections.Generic;
using System.IO;

namespace Common.PointsDataBase
{
    /// <summary>
    /// Implementation of <see cref="IPointsDataBase"/> interface, in memory database for modbus points
    /// </summary>
    public class PointsDataBase : IPointsDataBase.IPointsDataBase
    {
        public event Action DataChanged;

        public Dictionary<ushort, (PointsType, short)> RegistersDictionary = new Dictionary<ushort, (PointsType, short)>();
        public Dictionary<ushort, (PointsType, byte)> DiscreteDictionary = new Dictionary<ushort, (PointsType, byte)>();

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
                    OnDataChanged();
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
                        OnDataChanged();
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
            InitializePointsFromFile("pointsConfiguration.txt");
        }

        public void OnDataChanged()
        {
            DataChanged?.Invoke();
        }

        private void InitializePointsFromFile(string filePath)
        {
            foreach (var line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 3)
                    throw new FormatException($"Invalid line format: {line}");

                if (!ushort.TryParse(parts[0], out ushort address))
                    throw new FormatException($"Invalid address: {parts[0]}");

                if (!Enum.TryParse(parts[1], out PointsType type))
                    throw new FormatException($"Invalid PointsType: {parts[1]}");

                if (!int.TryParse(parts[2], out int value))
                    throw new FormatException($"Invalid value: {parts[2]}");

                if (type == PointsType.COILS || type == PointsType.DISCRETE_INPUTS)
                {
                    DiscreteDictionary[address] = (type, (byte)value);
                }
                else if (type == PointsType.INPUT_REGISTERS || type == PointsType.HOLDING_REGISTERS)
                {
                    RegistersDictionary[address] = (type, (short)value);
                }
                else
                {
                    throw new ArgumentException($"Unsupported point type: {type}");
                }
            }
        }
    }
}
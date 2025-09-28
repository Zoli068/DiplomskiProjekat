using Common.IPointsDataBase;
using System;
using System.Collections.Generic;
using System.IO;

namespace MasterGUI.Points
{
    public class PointsDatabase
    {
        public event Action? DataChanged;

        public Dictionary<ushort, Point<short>> Registers { get; private set; } = new();
        public Dictionary<ushort, Point<byte>> Coils { get; private set; } = new();

        public PointsDatabase() {
            InitializePoints("pointsConfiguration.txt");
        }

        public void AddRegister(ushort address,PointsType type, short min, short max)
        {
            Registers[address] = new Point<short>(address,type, min, max);
        }

        public void AddCoil(ushort address,PointsType type)
        {
            Coils[address] = new Point<byte>(address,type, 0, 1); // 0 = off, 1 = on
        }

        public void UpdateRegister(ushort address, short value)
        {
            if (Registers.TryGetValue(address, out var point))
            {
                point.UpdateValue(value);
                DataChanged?.Invoke();
            }
            else
            {
                Registers[address].Value = value;
                DataChanged?.Invoke();
            }
        }

        public void UpdateCoil(ushort address, byte value)
        {
            if (Coils.TryGetValue(address, out var point))
            {
                point.UpdateValue(value);
                DataChanged?.Invoke();
            }
            else
            {
                Coils[address].Value = value;
                DataChanged?.Invoke();
            }
        }


        private void InitializePoints(string filePath)
        {
            foreach (var line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                    continue;

                if (!ushort.TryParse(parts[0], out ushort address))
                    continue;

                if (!Enum.TryParse(parts[1], out PointsType type))
                    continue;

                switch (type)
                {
                    case PointsType.COILS:
                    case PointsType.DISCRETE_INPUTS:
                        byte coilMin = 0, coilMax = 1;
                        if (parts.Length >= 4)
                        {
                            coilMin = byte.Parse(parts[2]);
                            coilMax = byte.Parse(parts[3]);
                        }
                        Coils[address] = new Point<byte>(address, type, coilMin, coilMax);
                        break;

                    case PointsType.HOLDING_REGISTERS:
                    case PointsType.INPUT_REGISTERS:
                        if (parts.Length < 4)
                            throw new FormatException($"Registers require min and max values: {line}");

                        short regMin = short.Parse(parts[2]);
                        short regMax = short.Parse(parts[3]);
                        Registers[address] = new Point<short>(address, type, regMin, regMax);
                        break;

                }
            }
        }
    }
}
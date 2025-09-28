using Common.IPointsDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MasterGUI.Points
{
    public enum PointStatus
    {
        Unknown,
        Normal,
        Warning
    }

    public class Point<T>
    {
        public ushort Address { get; set; }
        public T Value { get; set; }
        public T Min { get; set; }
        public T Max { get; set; }
        public PointStatus Status { get; set; } = PointStatus.Unknown;
        public PointsType Type { get; set; }

        public Point(ushort address, PointsType type, T min, T max)
        {
            Address = address;
            Min = min;
            Max = max;
            Type = type;
            Value = default;
            Status = PointStatus.Unknown;
        }

        public void UpdateValue(T value)
        {
            Value = value;

            if (Comparer<T>.Default.Compare(value, Min) < 0 || Comparer<T>.Default.Compare(value, Max) > 0)
            {
                Status = PointStatus.Warning;
            }
            else
            {
                Status = PointStatus.Normal;
            }

            if(Type==PointsType.COILS || Type == PointsType.DISCRETE_INPUTS)
            {
                if (Comparer<T>.Default.Compare(value,Max)==0)
                {
                    Status = PointStatus.Warning;
                }
                else
                {
                    Status = PointStatus.Normal;
                }
            }
        }
    }
}
using System;

namespace Ex03.GarageLogic
{
    internal class ValueOutOfRangeException : Exception
    {
        public float MaxValue { get; }
        public float MinValue { get; }

        public ValueOutOfRangeException()
        {
        }

        public ValueOutOfRangeException(string message)
            : base(message)
        {
        }

        public ValueOutOfRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ValueOutOfRangeException(string message, float minValue, float maxValue)
            : base(message)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, MinValue: {MinValue}, MaxValue: {MaxValue}";
        }
    }
}
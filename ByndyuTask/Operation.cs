using System;

namespace ByndyuTask
{
    public class Operation
    {
        public int Priority { get; set; }
        public Func<double[], double> Function;
        public int NumberOfArguments { get; set; }

        public Operation(int priority, int numberOfArguments, Func<double[], double> function)
        {
            Priority = priority;
            NumberOfArguments = numberOfArguments;
            Function = function;
        }
    }
}
using System;

namespace ByndyuTask
{
    public class Operation
    {
        public int Priority { get; set; }

        public Func<double, double, double> Function;

        public Operation(int priority, Func<double, double, double> function)
        {
            Priority = priority;
            Function = function;
        }
    }
}
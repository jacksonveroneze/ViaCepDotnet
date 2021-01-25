using System;

namespace JacksonVeroneze.ViaCep.Tests.Config
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority)
            => Priority = priority;

        public int Priority { get; }
    }
}

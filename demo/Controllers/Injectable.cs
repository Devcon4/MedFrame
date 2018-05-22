using System;

namespace demo
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true )]
    public class Injectable : Attribute { }
}
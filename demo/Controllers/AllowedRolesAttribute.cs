using System;

namespace demo
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AllowedRolesAttribute : Attribute
    {
        public string[] Roles { get; }

        public AllowedRolesAttribute(params string[] roles)
        {
            Roles = roles;
        }
    }
}
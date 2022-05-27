using System;

namespace Sorskoot.Ioc
{
    internal class RegistrationModel
    {
        public IocScope Scope { get; set; }
        public Type Type { get; set; } 
    }
}
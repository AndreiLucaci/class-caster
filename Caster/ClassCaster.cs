using System;

namespace Caster
{
    public static class ClassCaster
    {
        public static T Cast<T>(this object obj)
        {
            return (T) Convert.ChangeType(obj, typeof(T));
        }
    }
}

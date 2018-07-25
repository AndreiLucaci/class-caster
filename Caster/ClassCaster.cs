﻿using System;

namespace Caster
{
    public static class ClassCaster
    {
        public static T Cast<T>(this object obj)
            where T : class
        {
            if (obj != null && obj.GetType() == typeof(T))
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }

            return default(T);
        }

        public static T Cast<T>(this object obj, bool throwException)
            where T : class
        {
            if (obj != null)
            {
                if (obj.GetType() == typeof(T))
                {
                    return (T) Convert.ChangeType(obj, typeof(T));
                }

                if (throwException)
                {
                    throw new InvalidCastException($"{obj.GetType()} cannot be converted to type {typeof(T)}.");
                }
            }

            return default(T);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KopLibrary.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> Get<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}

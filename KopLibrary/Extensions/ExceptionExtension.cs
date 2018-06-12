using System;

namespace KopLibrary.Extensions
{
    public static class ExceptionExtension
    {
        public static Exception MostInnerException(this Exception e)
        {
            if (e == null)
                return null;

            while (e.InnerException != null)
                e = e.InnerException;

            return e;
        }
    }
}

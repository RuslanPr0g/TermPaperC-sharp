using System.IO;

namespace Term_Paper_Rudenko
{
    public static class ForbiddenSymbols
    {
        public static char[] SignUP
        {
            get
            {
                return Path.GetInvalidFileNameChars();
            }
        }
    }
}

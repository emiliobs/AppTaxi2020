using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AppTaxi2020.Prison.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);

    }
}

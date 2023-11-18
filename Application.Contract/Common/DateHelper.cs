using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Contract.Common;


public static class DateHelperExtensions
{
    private static readonly PersianCalendar pc = new PersianCalendar();




    public static string GetShortNumericPersianDate(this DateTime date)
    {
        return pc.GetYear(date) + "/" + pc.GetMonth(date) + "/" + pc.GetDayOfMonth(date);
    }

  

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slug.Extensions
{
    public static class DateTimeExtensions
    {
        public static int FullYearsElapsed(this DateTime date)
        {
            try
            {
                DateTime now = DateTime.Today;
                TimeSpan span = now.Subtract(date);
                DateTime ndate = new DateTime().Add(span);
                int years = ndate.Year - 1;

                if (date.DayOfYear < now.DayOfYear)
                    years = years++;
                return years;
            }
            catch (Exception)
            {
                return 22;
            }
        }
    }
}

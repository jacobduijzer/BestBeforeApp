using System;
namespace BestBeforeApp.Helpers
{
    public static class DateCalculator
    {
        public static string HumanizeDate(this DateTime inputDate)
        {
            var ts = new TimeSpan(Math.Abs(DateTime.Now.Ticks - inputDate.Ticks));

            if (ts.TotalHours < 24)
            {
                return "vandaag";
            }

            if (ts.TotalHours < 48)
            {
                var days = Math.Abs((inputDate.Date - DateTime.Now.Date).Days);
                return $"{days} dagen";
            }

            if (ts.TotalDays < 28)
            {
                var days = Math.Abs((inputDate.Date - DateTime.Now.Date).Days);
                return $"{days} dagen-1";
            }

            if (ts.TotalDays >= 28 && ts.TotalDays < 30)
            {
                //if (DateTime.Now.Date.AddMonths(tense == Tense.Future ? 1 : -1) == input.Date)
                //{
                //    return formatter.DateHumanize(TimeUnit.Month, tense, 1);
                //}
                return $"1 maand";
                //return formatter.DateHumanize(TimeUnit.Day, tense, ts.Days);
            }

            if (ts.TotalDays < 345)
            {
                var months = Convert.ToInt32(Math.Floor(ts.TotalDays / 29.5));
                return $"{months} maanden";
            }

            var years = Convert.ToInt32(Math.Floor(ts.TotalDays / 365));
            if (years == 0)
            {
                years = 1;
            }

            return $"{years} jaar";
        }
    }
}

using System;

namespace _0_FrameWork.Application
{
    public static class DateExtension
    {
        public static string TimeAgo(this DateTime date)
        {

            TimeSpan timeSince = DateTime.Now.Subtract(date);

            if (timeSince.TotalMilliseconds < 1)
                return "not yet";
            if (timeSince.TotalMinutes < 1)
                return "همین الان";
            if (timeSince.TotalMinutes < 2)
                return "1 قیقه قبل";
            if (timeSince.TotalMinutes < 60)
                return $"{timeSince.Minutes} دقیقه قبل";
            if (timeSince.TotalMinutes < 120)
                return "1 ساعت قبل";
            if (timeSince.TotalHours < 24)
                return string.Format("{0} ساعت قبل", timeSince.Hours);
            if (timeSince.TotalDays == 1)
                return "دیروز";
            if (timeSince.TotalDays < 7)
                return string.Format("{0} روز قبل", timeSince.Days);
            if (timeSince.TotalDays < 14)
                return "هفته گذشته";
            if (timeSince.TotalDays < 21)
                return "2 هفته گذشته";
            if (timeSince.TotalDays < 28)
                return "3 هفته گذشته";
            if (timeSince.TotalDays < 60)
                return "یک ماه پیش";
            if (timeSince.TotalDays < 365)
                return $"{Math.Round(timeSince.TotalDays / 30)} ماه پیش";
            if (timeSince.TotalDays < 730)
                return "last year";

            //last but not least...
            return string.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365));

        }
    }
}

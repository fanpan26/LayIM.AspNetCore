using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Extensions
{
    /// <summary>
    /// 时间类型扩展
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 将时间转换为几秒前
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime time)
        {
            long totalSeconds = (long)(DateTime.Now - time).TotalSeconds;

            if (totalSeconds <= 2)
            {
                return "刚刚";
            }
            if (totalSeconds > 2 && totalSeconds < 60)
            {
                return $"{totalSeconds}秒前";
            }
            if (totalSeconds >= 60 && totalSeconds < 3600)
            {
                return $"{totalSeconds / 60}分钟前";
            }
            if (totalSeconds >= 3600 && totalSeconds < 3600 * 24)
            {
                return $"{totalSeconds / 3600}小时前";
            }
            if (totalSeconds >= 3600 * 24 && totalSeconds < 3600 * 24 * 2)
            {
                return $"{totalSeconds / (3600 * 24)}天前";
            }

            if (totalSeconds >= 3600 * 24 * 2 && totalSeconds < 3600 * 24 * 7)
            {
                return time.ToString("MM月dd日 HH:mm");
            }
            return time.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 转换为UTC的时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime time)
        {
            return (long)(time.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        /// <summary>
        /// 转换为UTC的毫秒时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToMilliTimestamp(this DateTime time)
        {
            return (long)(time.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// 时间戳转时间（秒）
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime FromTimestamp(this long timestamp)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            DateTime dt = startTime.AddSeconds(timestamp);
            return dt;
        }

        static string[] weekdays = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        /// <summary>
        /// 将时间转换为具体的时间字符串
        /// </summary>
        /// <param name="time"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static string ToTimeDetailString(this DateTime time, int style = 1)
        {
            long totalSeconds = (long)(DateTime.UtcNow - time).TotalSeconds;

            bool styleList = style == 1;

            if (totalSeconds < 3600 * 24)
            {
                return styleList ? time.ToString("HH:mm") : time.ToString("HH:mm");
            }

            if (time.ToString("yyyy-MM-dd") == DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"))
            {
                return styleList ? "昨天" : $"昨天 {time.ToString("HH:mm")}";
            }
            if (totalSeconds >= 3600 * 24 && totalSeconds < 3600 * 24 * 4)
            {

                return styleList ? weekdays[(int)time.DayOfWeek] : $"{weekdays[(int)time.DayOfWeek]} {time.ToString("HH:mm")}"; ;
            }

            return styleList ? time.ToString("yyyy-MM-dd") : time.ToString("yyyy-MM-dd HH:mm");
        }
    }
}

﻿/****************************************************************************
THIS FILE IS PART OF Foolish Server PROJECT
THIS PROGRAM IS FREE SOFTWARE, IS LICENSED UNDER MIT

Copyright (c) 2022-2030 ChenYiZh
https://space.bilibili.com/9308172

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
****************************************************************************/

using FoolishGames.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoolishGames.Timer
{
    /// <summary>
    /// 时间管理类
    /// </summary>
    public static class TimeLord
    {
        /// <summary>
        /// 当前使用的计时控件
        /// </summary>
        public static IPacketWatch PacketWatch { get; private set; } = null;

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object syncRoot = new object();

        /// <summary>
        /// 设置时间控件
        /// </summary>
        /// <param name="watch"></param>
        public static void SetPacketWatch(IPacketWatch watch)
        {
            lock (syncRoot)
            {
                PacketWatch = watch;
            }
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        public static DateTime Now
        {
            get
            {
                try
                {
                    //lock (syncRoot)
                    //{
                    if (PacketWatch == null)
                    {
                        return DateTime.Now;
                    }
                    else
                    {
                        return PacketWatch.Now;
                    }
                    //}
                }
                catch (Exception e)
                {
                    FConsole.WriteExceptionWithCategory(Categories.TIME_LORD, e);
                    return DateTime.Now;
                }
            }
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        public static DateTime UTC
        {
            get
            {
                try
                {
                    //lock (syncRoot)
                    //{
                    if (PacketWatch == null)
                    {
                        return DateTime.UtcNow;
                    }
                    else
                    {
                        return PacketWatch.UTC;
                    }
                    //}
                }
                catch (Exception e)
                {
                    FConsole.WriteExceptionWithCategory(Categories.TIME_LORD, e);
                    return DateTime.UtcNow;
                }
            }
        }

        /// <summary>
        /// 内部的时间计划管理者
        /// </summary>
        public static TimeWorker Worker { get; private set; }

        /// <summary>
        /// 添加新的时间计划
        /// </summary>
        public static void Append(TimeSchedule schedule)
        {
            Worker.Append(schedule);
        }

        /// <summary>
        /// 移除时间计划
        /// </summary>
        public static void Remove(TimeSchedule schedule)
        {
            Worker.Remove(schedule);
        }

        /// <summary>
        /// 初始化操作
        /// </summary>
        static TimeLord()
        {
            Worker = new TimeWorker();
        }

        /// <summary>
        /// 需要与GameDefines中的格式对上
        /// </summary>
        public static DateTime FromString(string value)
        {
            return DateTime.Parse(value);
        }

        public static string GetNowDate()
        {
            return ConvertDate(Now);
        }

        public static string GetUTCDate()
        {
            return ConvertDate(UTC);
        }

        public static string GetNowDateTime()
        {
            return ConvertDateTime(Now);
        }

        public static string GetUTCDateTime()
        {
            return ConvertDateTime(UTC);
        }

        public static string ConvertDateTime(DateTime dateTime)
        {
            return Convert(dateTime, GameDefines.Instance.DateTimeFormat);
        }

        public static string ConvertDate(DateTime dateTime)
        {
            return Convert(dateTime, GameDefines.Instance.DateFormat);
        }

        public static string Convert(DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }
    }
}
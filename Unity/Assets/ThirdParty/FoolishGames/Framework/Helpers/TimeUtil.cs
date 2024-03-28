using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUtil
{
    private static TimeSpan deltaTime;

    public static void CheckTime(DateTime current)
    {
        deltaTime = current - DateTime.Now;
    }

    public static DateTime Now
    {
        get
        {
            return DateTime.Now + deltaTime;
        }
    }
}

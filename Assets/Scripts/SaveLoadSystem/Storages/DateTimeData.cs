using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DateTimeData
{
    [SerializeField] private int year;
    [SerializeField] private int month;
    [SerializeField] private int day;
    [SerializeField] private int hour;
    [SerializeField] private int minute;


    public DateTimeData() : this(new DateTime())
    {

    }
    public DateTimeData(int year, int month, int day, int hour, int minute)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }
    public DateTimeData(int year, int month, int day) : this(year, month, day, 0, 0)
    {

    }
    public DateTimeData(DateTime dateTime) : this(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute)
    {

    }


    public static DateTimeData operator +(DateTimeData date, TimeSpan timeSpan)
    {
        return new DateTimeData(date.Date + timeSpan);
    }
    public static DateTimeData operator -(DateTimeData date, TimeSpan timeSpan)
    {
        return new DateTimeData(date.Date - timeSpan);
    }


    public DateTime Date => new DateTime(year, month, day, hour, minute, 0);
}

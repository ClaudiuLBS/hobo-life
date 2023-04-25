using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle instance;
    public float cycleDuration = 360; // seconds
    [Range(0.01f, 0.99f)]
    public float dayTime = 0.5f;
    public int hour, minute = 0;
    public int currentDay = 0;
    public TextMeshProUGUI dateTimeDisplay;

    private float dayDuration, nightDuration;
    private float currentRotation;
    private float startHour = 6, startMinute = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        currentRotation = transform.localEulerAngles.x;

        dayDuration = cycleDuration * dayTime;
        nightDuration = cycleDuration * (1 - dayTime);

        startHour += (24 / cycleDuration) * Time.deltaTime;
        startMinute += (60 * 24 / cycleDuration) * Time.deltaTime;
        int previousHour = hour;
        hour = (int)startHour % 24;
        minute = (int)startMinute % 60;
        if (hour < previousHour)
            DayPass();

        setDateTimeDisplay(currentDay, hour, minute);
        if (currentRotation <= 90)
        {
            transform.Rotate((180 / dayDuration) * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Rotate((180 / nightDuration) * Time.deltaTime, 0, 0);
        }

    }
    void setDateTimeDisplay(int day, int hour, int minute)
    {
        string hourString = (hour < 10 ? "0" : "") + hour;
        string minuteString = (minute < 10 ? "0" : "") + minute;

        dateTimeDisplay.text = $"Day {day}, {hourString}:{minuteString}";
    }

    public event Action OnDayPass;
    public void DayPass()
    {
        currentDay++;
        OnDayPass?.Invoke();
    }
}

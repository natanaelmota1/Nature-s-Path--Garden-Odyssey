using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LastPlayedChecker : MonoBehaviour
{
    private DateTime lastPlayedDate;
    private TimeSpan timeSinceLastPlayed;
    private TimeSpan timeBetween6amTo4pm;

    private void Start()
    {
        // Retrieve the last played date from PlayerPrefs
        int lastPlayedTicks = PlayerPrefs.GetInt("LastPlayedTicks", 0);
        lastPlayedDate = new DateTime(lastPlayedTicks);

        // Calculate the time since the last play
        timeSinceLastPlayed = DateTime.Now - lastPlayedDate;
        Debug.Log("Time since last played: " + timeSinceLastPlayed);

        // Calculate the time between 6 a.m. and 4 p.m.
        DateTime startOfToday = DateTime.Today;
        DateTime sixAM = startOfToday.AddHours(6);
        DateTime fourPM = startOfToday.AddHours(16);
        if (lastPlayedDate < sixAM)
        {
            timeBetween6amTo4pm = TimeSpan.Zero;
        }
        else if (lastPlayedDate >= sixAM && lastPlayedDate <= fourPM)
        {
            timeBetween6amTo4pm = DateTime.Now - sixAM;
        }
        else
        {
            timeBetween6amTo4pm = fourPM - sixAM;
        }
        Debug.Log("Time between 6 a.m. and 4 p.m.: " + timeBetween6amTo4pm);
    }

    private void OnApplicationQuit()
    {
        long present = System.DateTime.Now.Ticks;
        // Save the current date and time as the last played date
        //PlayerPrefs.SetInt("LastPlayedTicks", System.DateTime.Now.Ticks);
        PlayerPrefs.Save();
    }
}
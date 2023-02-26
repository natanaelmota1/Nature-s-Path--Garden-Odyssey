using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public long present_day, present_second;
    public GameObject HourText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        long present = System.DateTime.Now.Ticks;
        long present_day = present / 864000000000;
        long present_second = present % 864000000000 / 10000000;

        HourText.GetComponent<Text>().text = "" + (present_second / 3600) + ":" + (present_second % 3600 / 60) + ":" + (present_second % 3600 % 60);
    }
}

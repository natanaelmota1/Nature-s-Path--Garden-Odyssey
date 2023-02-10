using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject display;
    int ciclo_da_planta;
    int tempo_de_crescimento = 7200;
    int sec;
    int day;
    long present = System.DateTime.Now.Ticks;
    long present_day, present_second;


    void Start()
    {
        
        int day = PlayerPrefs.GetInt("DayPlanted");
        //int hour = PlayerPrefs.GetInt("HourPlanted");
        //int min = PlayerPrefs.GetInt("MinutePlanted");
        int sec = PlayerPrefs.GetInt("SecondPlanted");

        long present = System.DateTime.Now.Ticks;
        long present_day = present / 864000000000;
        long present_second = present % 864000000000 / 10000000;

        int hour = sec / 3600;
        int min = sec % 3600 / 60; 
        int secshow = sec % 3600 % 60;
        int pshow = (int)present_second % 3600 % 60;

        if (secshow >= pshow)
        {
            int valorshow = secshow - pshow;
        }

        print("Sua planta foi plantada Dia "+day+" às "+hour+":"+min+":"+secshow);
        print("Já se passaram: "+(present_day - day)+" Dias "+" e "+(present_second - sec)+" Segundos desde que sua planta foi plantada");
    
    }

    // Update is called once per frame
    void Update()
    {
        int day = PlayerPrefs.GetInt("DayPlanted");
        int sec = PlayerPrefs.GetInt("SecondPlanted");

        long present = System.DateTime.Now.Ticks;
        long present_day = present / 864000000000;
        long present_second = present % 864000000000 / 10000000;
        long restante = tempo_de_crescimento + sec - present_second;
        display.GetComponent<Text>().text = "Faltam " + (restante) + " Segundos para sua planta crescer";

        
        
    }

    public void Plant()
    {
        
        long PlantedTime = System.DateTime.Now.Ticks;
        long lday = PlantedTime / 864000000000;
        //long lhour = PlantedTime % 864000000000 / 36000000000;
        //long lmin = PlantedTime % 864000000000 % 36000000000 / 600000000;
        long lsec = PlantedTime % 864000000000 / 10000000;

        PlayerPrefs.SetInt("DayPlanted",(int)lday);
        //PlayerPrefs.SetInt("HourPlanted",(int)lhour);
        //PlayerPrefs.SetInt("MinutePlanted",(int)lmin);
        PlayerPrefs.SetInt("SecondPlanted",(int)lsec);

    }
}

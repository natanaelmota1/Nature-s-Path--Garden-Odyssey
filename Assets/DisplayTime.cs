using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayTime : MonoBehaviour
{
    public GameObject theDisplay, second, third;
    long timeItWasPlanted = 638114639836038899;
    long beningin = System.DateTime.Now.Ticks;
    // Start is called before the first frame update
    void Start()
    {
        //timeItWasPlanted = System.DateTime.Now.Ticks;
        print("Estamos no " + (beningin / 10000000 / 60 / 24) + "° Dia");
    }

    // Update is called once per frame
    void Update()
    {
        
        long ticks = System.DateTime.Now.Ticks;
        theDisplay.GetComponent<Text>().text = "Now " + ticks;
        long thirdc = ((beningin+600000000) - ticks) / 10000000;
        long seconds = (ticks-timeItWasPlanted) / 10000000;
        long minutes = seconds / 60;

        second.GetComponent<Text>().text = "It's been " + minutes + "minutes and" + (seconds % 60) + " SECONDS since your plant was planted";
        third.GetComponent<Text>().text = "Subtraction" + thirdc;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    int day,sec;
    int tempo_de_crescimento = 7200;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    void Start()
    {
        int day = PlayerPrefs.GetInt("DayPlanted");
        int sec = PlayerPrefs.GetInt("SecondPlanted");
    }

    // Update is called once per frame
    void Update()
    {
        int day = PlayerPrefs.GetInt("DayPlanted");
        int sec = PlayerPrefs.GetInt("SecondPlanted");
        long present = System.DateTime.Now.Ticks;
        long present_day = present / 864000000000;
        long present_second = present % 864000000000 / 10000000;
        long segundos_restantes = tempo_de_crescimento + sec - present_second;
        print(day);

        if(segundos_restantes <= tempo_de_crescimento * 4 / 5)
        {
            print("Fase 1 krl");
            spriteRenderer.sprite = newSprite; 

        }
        else if(segundos_restantes <= tempo_de_crescimento * 3 / 5)
        {
            print("Fase 2 krl");

        }
        else if(segundos_restantes <= tempo_de_crescimento * 2 / 5)
        {
            print("Fase 3 krl");

        }
        else if(segundos_restantes <= tempo_de_crescimento / 5)
        {
            print("Fase 4 krl");

        }
        else if(segundos_restantes <= 0)
        {
            print("A planta cresceu completamente");
        }
    }
}

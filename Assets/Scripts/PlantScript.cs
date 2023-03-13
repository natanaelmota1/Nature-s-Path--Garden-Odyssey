using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantScript : MonoBehaviour
{
    public long thisplantTick;
    public int thisplantnum;
    public GameObject plantseedButton;
    public GameObject sellplantButton;
    public string Plantype;
    public float totalTimeToGrow = 7200f; // Tempo total de crescimento da planta em segundos
    private float timeToDecrement; // Tempo restante para a planta crescer
    private float timePerPhase; // Tempo correspondente a cada fase de crescimento
    public Text timeText;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        string p = PlayerPrefs.GetString("ThisPlanType" + thisplantnum);
        if (p != "")
        {
            spriteRenderer.sprite = Resources.Load<Sprite>(p);
        }
        int plant_days = PlayerPrefs.GetInt("ThisPlantDays" + thisplantnum);
        int plant_seconds = PlayerPrefs.GetInt("ThisPlantDays" + thisplantnum);
        thisplantTick = (plant_days * 864000000000) + (plant_seconds * 10000000);

        timeToDecrement = totalTimeToGrow;
        timePerPhase = totalTimeToGrow / 4f; // Divide o tempo total por 4 para obter o tempo de cada fase
    }

    // Update is called once per frame
    void Update()
    {
        timeToDecrement -= Time.deltaTime;

        if (timeToDecrement <= timePerPhase * 0.25f) // Verifica se o tempo restante é menor ou igual a 25% do tempo total
        {
            timeText.text = timeToDecrement.ToString() + "/n" + "Fase 4";
        }
        else if (timeToDecrement <= timePerPhase * 0.5f) // Verifica se o tempo restante é menor ou igual a 50% do tempo total
        {
            timeText.text = timeToDecrement.ToString() + "/n" + "Fase 3";
        }
        else if (timeToDecrement <= timePerPhase * 0.75f) // Verifica se o tempo restante é menor ou igual a 75% do tempo total
        {
            timeText.text = timeToDecrement.ToString() + "/n" + "Fase 2";
        }
        else if (timeToDecrement <= timePerPhase) // Verifica se o tempo restante é menor ou igual ao tempo de uma fase completa
        {
            timeText.text = timeToDecrement.ToString() + "/n" + "Fase 1";
        }
        else // Se ainda não atingiu nenhuma das condições anteriores, significa que a planta ainda não começou a crescer
        {
            timeText.text = timeToDecrement.ToString() + "/n" + "Aguardando";
        }

        if (timeToDecrement <= 0) // Se o tempo restante for menor ou igual a zero, significa que a planta já cresceu completamente
        {
            timeText.text = timeToDecrement.ToString() + "/n" + "Cresceu";
        }
    }

    public void PlantSeedButton(bool bo)
    {
        plantseedButton.SetActive(bo);
    }

    public void SellPlantButton(bool bo)
    {
        sellplantButton.SetActive(bo);
    }

    public void PlantSeed()
    {
        timeText.enabled = true;
        Plantype = GameObject.Find("UIManager").GetComponent<ManagerUI>().SeedSelection;
        spriteRenderer.sprite = Resources.Load<Sprite>(Plantype);
        PlayerPrefs.SetString("ThisPlanType" + thisplantnum, Plantype);

        long present = System.DateTime.Now.Ticks;
        long present_day = present / 864000000000;
        long present_second = present % 864000000000 / 10000000;
        PlayerPrefs.SetInt("ThisPlantDays" + thisplantnum, (int)present_day);
        PlayerPrefs.SetInt("ThisPlantSeconds" + thisplantnum, (int)present_second);
        thisplantTick = present;

        int plant_days = PlayerPrefs.GetInt("ThisPlantDays" + thisplantnum);
        int plant_seconds = PlayerPrefs.GetInt("ThisPlantDays" + thisplantnum);

        print((plant_days * 864000000000) + (plant_seconds * 10000000));
        print(present);

    }

    public void SellPlant()
    {
        spriteRenderer.sprite = null;
        PlayerPrefs.SetString("ThisPlanType" + thisplantnum, "");
        PlayerPrefs.SetInt("ThisPlantDays" + thisplantnum, 0);
        PlayerPrefs.SetInt("ThisPlantSeconds" + thisplantnum, 0);
        timeText.enabled = false;
    }
}

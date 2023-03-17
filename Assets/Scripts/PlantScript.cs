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
    public float totalTimeToGrow = 60; // Tempo total de crescimento da planta em segundos
    private float timeToDecrement; // Tempo restante para a planta crescer
    private float timePerPhase; // Tempo correspondente a cada fase de crescimento
    public Text timeText;
    SpriteRenderer spriteRenderer;
    string p = "";
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        p = PlayerPrefs.GetString("ThisPlanType" + thisplantnum);
        if (p != "")
        {
           spriteRenderer.sprite = Resources.Load<Sprite>(p);
        }
        int plant_days = PlayerPrefs.GetInt("ThisPlantDays" + thisplantnum);
        int plant_seconds = PlayerPrefs.GetInt("ThisPlantSeconds" + thisplantnum);
        thisplantTick = (plant_days * 864000000000) + (plant_seconds * 10000000);

        timeToDecrement = PlayerPrefs.GetInt("ThisPlantSeconds" + thisplantnum);
        timePerPhase = totalTimeToGrow / 4f; // Divide o tempo total por 4 para obter o tempo de cada fase
    }

    // Update is called once per frame
    void Update()
    {
        p = PlayerPrefs.GetString("ThisPlanType" + thisplantnum);
        timeToDecrement -= Time.deltaTime;
        PlayerPrefs.SetInt("ThisPlantSeconds" + thisplantnum, (int)Mathf.RoundToInt(timeToDecrement));
        timeText.text = Mathf.RoundToInt(timeToDecrement).ToString() + " seconds";
        Debug.Log(p);
        timeText.enabled = false;

        if (timeToDecrement <= 0) // Se o tempo restante for menor ou igual a zero, significa que a planta já cresceu completamente
        {
            if (p != "")
            {
                timeText.enabled = true;
                spriteRenderer.sprite = Resources.Load<Sprite>(p+"_fase2");
            }
        }
        else if (timeToDecrement <= totalTimeToGrow * 0.5f) // Verifica se o tempo restante é menor ou igual a 50% do tempo total
        {
            if (p != "")
            {
                timeText.enabled = true;
                spriteRenderer.sprite = Resources.Load<Sprite>(p+"_fase1");
            }
        }
        else if (timeToDecrement <= totalTimeToGrow * 0.75f) // Verifica se o tempo restante é menor ou igual a 75% do tempo total
        {
            if (p != "")
            {
                timeText.enabled = true;
                spriteRenderer.sprite = Resources.Load<Sprite>(p+"_fase0");
            }
        }
        else // Se ainda não atingiu nenhuma das condições anteriores, significa que a planta ainda não começou a crescer
        {
            if (p != "")
            {
                timeText.enabled = true;
                spriteRenderer.sprite = Resources.Load<Sprite>(p+"_fase0");
            }
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
        timeToDecrement = totalTimeToGrow;

        long present = System.DateTime.Now.Ticks;
        long present_day = present / 864000000000;
        long present_second = present % 864000000000 / 10000000;
        PlayerPrefs.SetInt("ThisPlantDays" + thisplantnum, (int)present_day);
        PlayerPrefs.SetInt("ThisPlantSeconds" + thisplantnum, (int)totalTimeToGrow);
        thisplantTick = present;

        int plant_days = PlayerPrefs.GetInt("ThisPlantDays" + thisplantnum);
        int plant_seconds = PlayerPrefs.GetInt("ThisPlantDays" + thisplantnum);

        print((plant_days * 864000000000) + (plant_seconds * 10000000));
        print(present);
        gameObject.SetActive(true); // reativa o objeto da planta
    }

    public void SellPlant()
    {
        
        spriteRenderer.sprite = null;
        timeToDecrement = totalTimeToGrow;
        PlayerPrefs.SetString("ThisPlanType" + thisplantnum, "");
        PlayerPrefs.SetInt("ThisPlantDays" + thisplantnum, 0);
        PlayerPrefs.SetInt("ThisPlantSeconds" + thisplantnum, 0);
        timeText.enabled = false;
        Plantype = "";
        gameObject.SetActive(false); // desativa o objeto da planta
    }
}
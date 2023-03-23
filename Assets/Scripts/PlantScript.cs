using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantScript : MonoBehaviour
{
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
    private string weather;
    private float temperature;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        p = PlayerPrefs.GetString("ThisPlanType" + thisplantnum);
        if (p != "")
        {
           spriteRenderer.sprite = Resources.Load<Sprite>(p);
        }
        int plant_seconds = PlayerPrefs.GetInt("ThisPlantTimeToGrow" + thisplantnum);

        timeToDecrement = PlayerPrefs.GetInt("ThisPlantTimeToGrow" + thisplantnum);
        timePerPhase = totalTimeToGrow / 4f; // Divide o tempo total por 4 para obter o tempo de cada fase
    }

    // Update is called once per frame
    void Update()
    {
        weather = PlayerPrefs.GetString("Weather");
        temperature = PlayerPrefs.GetFloat("Temperature");
        Debug.Log($"Temperature: {(int)Mathf.Round(temperature)} ºC");
        Debug.Log($"Clima: {weather}");
        int currentTime = DateTime.Now.Hour;
        Debug.Log($"Hour: {currentTime}");
        
        // Calcula o fator de crescimento com base nas informações de clima e horário
        float growthFactor = CalculateGrowthFactor(temperature, currentTime);
        
        if (timeToDecrement > 0){
            timeToDecrement -= Time.deltaTime * growthFactor;
            timeText.text = Mathf.RoundToInt(timeToDecrement).ToString() + " seconds";
        }
        else {
            timeText.text = "Planta Crescida";
        }

        p = PlayerPrefs.GetString("ThisPlanType" + thisplantnum);
        PlayerPrefs.SetInt("ThisPlantTimeToGrow" + thisplantnum, (int)Mathf.RoundToInt(timeToDecrement));
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

        if (Plantype == "blueberry")
        {
            PlayerPrefs.SetFloat("ThisPlantIdealTemperature" + thisplantnum, 25.0f);
            PlayerPrefs.SetInt("ThisPlantIdealHour" + thisplantnum, 12);
        }
        else
        {
            PlayerPrefs.SetFloat("ThisPlantIdealTemperature" + thisplantnum, 22.0f);
            PlayerPrefs.SetInt("ThisPlantIdealHour" + thisplantnum, 18);
        }
        
        PlayerPrefs.SetInt("ThisPlantTimeToGrow" + thisplantnum, (int)totalTimeToGrow);

        int plant_seconds = PlayerPrefs.GetInt("ThisPlantDays" + thisplantnum);

        gameObject.SetActive(true); // reativa o objeto da planta
    }

    public void SellPlant()
    {
        
        spriteRenderer.sprite = null;
        timeToDecrement = totalTimeToGrow;
        PlayerPrefs.SetString("ThisPlanType" + thisplantnum, "");
        PlayerPrefs.SetInt("ThisPlantTimeToGrow" + thisplantnum, 0);
        timeText.enabled = false;
        Plantype = "";
        gameObject.SetActive(false); // desativa o objeto da planta
    }
    
    private float CalculateGrowthFactor(float temperature, int currentHour)
    {
        // Defina o fator de crescimento máximo para uma temperatura ideal
        float maxGrowthFactor = 1.0f;

        // Defina a temperatura ideal para o crescimento da planta
        float idealTemperature = PlayerPrefs.GetFloat("ThisPlantIdealTemperature" + thisplantnum);

        // Calcule a diferença entre a temperatura atual e a temperatura ideal
        float temperatureDiff = Mathf.Abs(temperature - idealTemperature);

        // Calcule o fator de crescimento com base na diferença de temperatura
        float temperatureFactor = Mathf.Clamp01(1.0f - temperatureDiff / idealTemperature);

        // Defina o horário ideal para o crescimento da planta
        int idealHour = PlayerPrefs.GetInt("ThisPlantIdealHour" + thisplantnum);;

        // Calcule a diferença entre o horário atual e o horário ideal
        int hourDiff = Mathf.Abs(currentHour - idealHour);

        // Calcule o fator de crescimento com base na diferença de horário
        float hourFactor = Mathf.Clamp01(1.0f - hourDiff / 6.0f);

        // Calcule o fator de crescimento total multiplicando os fatores de temperatura e horário
        float growthFactor = temperatureFactor * hourFactor * maxGrowthFactor;

        return growthFactor;
    }
}
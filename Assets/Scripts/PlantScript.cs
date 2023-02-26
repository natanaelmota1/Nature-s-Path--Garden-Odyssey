using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public long thisplantTick;
    public int thisplantnum;
    public GameObject plantseedButton;
    public GameObject sellplantButton;
    public string Plantype,thisPlantype;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        string p = PlayerPrefs.GetString("ThisPlanType"+thisplantnum);
        if (p != "")
        {
            spriteRenderer.sprite = Resources.Load<Sprite>(p);
        }
        int plant_days = PlayerPrefs.GetInt("ThisPlantDays"+thisplantnum);
        int plant_seconds = PlayerPrefs.GetInt("ThisPlantDays"+thisplantnum);
        thisplantTick = (plant_days * 864000000000) + (plant_seconds * 10000000);
    }

    // Update is called once per frame
    void Update()
    {
        long present = System.DateTime.Now.Ticks;
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
        Plantype = GameObject.Find("UIManager").GetComponent<ManagerUI>().SeedSelection;
        spriteRenderer.sprite = Resources.Load<Sprite>(Plantype);
        PlayerPrefs.SetString("ThisPlanType"+thisplantnum,Plantype);

        long present = System.DateTime.Now.Ticks;
        long present_day = present / 864000000000;
        long present_second = present % 864000000000 / 10000000;
        PlayerPrefs.SetInt("ThisPlantDays"+thisplantnum,(int)present_day);
        PlayerPrefs.SetInt("ThisPlantSeconds"+thisplantnum,(int)present_second);
        thisplantTick = present;

        int plant_days = PlayerPrefs.GetInt("ThisPlantDays"+thisplantnum);
        int plant_seconds = PlayerPrefs.GetInt("ThisPlantDays"+thisplantnum);

        print((plant_days * 864000000000) + (plant_seconds * 10000000));
        print(present);

        
    }

    public void SellPlant()
    {
        spriteRenderer.sprite = null;
        PlayerPrefs.SetString("ThisPlanType"+thisplantnum,"");
        PlayerPrefs.SetInt("ThisPlantDays"+thisplantnum,0);
        PlayerPrefs.SetInt("ThisPlantSeconds"+thisplantnum,0);
    }
}

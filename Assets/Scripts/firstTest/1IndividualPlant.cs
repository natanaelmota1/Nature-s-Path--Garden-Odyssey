using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualPlant : MonoBehaviour
{
    public int thisPlantNumber;
    public string thisplantType;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        /*
        PlayerPrefs.SetInt("PlantDays"+thisPlantNumber,0);
        PlayerPrefs.SetInt("PlantSeconds"+thisPlantNumber,0);
        PlayerPrefs.SetString("PlantType"+thisPlantNumber,"");
        GameObject.Find("VaseButton"+thisPlantNumber).GetComponent<VaseInfo>().thisVaseIsFull(true);
        GameObject.Find("RemovePlant"+thisPlantNumber).GetComponent<SellPlant>().thisVaseIsFull(false);
        */

        thisplantType = PlayerPrefs.GetString("PlantType"+thisPlantNumber);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(thisplantType != "")
        {
            //GameObject.Find("VaseButton"+thisPlantNumber).GetComponent<VaseInfo>().thisVaseIsFull(true);
            //GameObject.Find("RemovePlant"+thisPlantNumber).GetComponent<SellPlant>().thisVaseIsFull(true);
            spriteRenderer.sprite = Resources.Load<Sprite>(thisplantType);
        }
        else
        {
            //GameObject.Find("VaseButton"+thisPlantNumber).GetComponent<VaseInfo>().thisVaseIsFull(false);
            //GameObject.Find("RemovePlant"+thisPlantNumber).GetComponent<SellPlant>().thisVaseIsFull(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void thisplant(string PlantType)
    {
        spriteRenderer.sprite = Resources.Load<Sprite>(PlantType);
        long present = System.DateTime.Now.Ticks;
        long present_day = present / 864000000000;
        long present_second = present % 864000000000 / 10000000;

        print("Sua semente de tipo " + PlantType + " foi plantada às " + (present_second / 3600) + ":" + (present_second % 3600 / 60) + ":" + (present_second % 3600 % 60));

        PlayerPrefs.SetInt("PlantDays"+thisPlantNumber,(int)present_day);
        PlayerPrefs.SetInt("PlantSeconds"+thisPlantNumber,(int)present_second);
        PlayerPrefs.SetString("PlantType"+thisPlantNumber,PlantType);

        //GameObject.Find("VaseButton"+thisPlantNumber).GetComponent<VaseInfo>().thisVaseIsFull(true);
        //GameObject.Find("RemovePlant"+thisPlantNumber).GetComponent<SellPlant>().thisVaseIsFull(true);
    }

    public void SellPlant()
    {
        PlayerPrefs.SetInt("PlantDays"+thisPlantNumber,0);
        PlayerPrefs.SetInt("PlantSeconds"+thisPlantNumber,0);
        PlayerPrefs.SetString("PlantType"+thisPlantNumber,"");
        //GameObject.Find("VaseButton"+thisPlantNumber).GetComponent<VaseInfo>().thisVaseIsFull(false);
        //GameObject.Find("RemovePlant"+thisPlantNumber).GetComponent<SellPlant>().thisVaseIsFull(false);
        spriteRenderer.sprite = Resources.Load<Sprite>("");
    }
}

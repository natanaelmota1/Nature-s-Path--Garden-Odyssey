using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    GameObject[] Plant;
    //GameObject[] PlantSeed;
    //GameObject[] SellPlant;
    public string SeedSelection;

    void Start()
    {
        Plant = GameObject.FindGameObjectsWithTag("Plant");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void seedType(string type)
    {
        SeedSelection = type;
    }

    public void CheckIfPlantExists_toPlantSeed()
    {
        //Esse void vai checkar se a planta tem algum sprite. Se não tiver, o botão de plantar aparece, caso contrário, ele não aparece
        foreach (GameObject p in Plant)
        {
            Sprite sprite = p.GetComponent<SpriteRenderer>().sprite;
            if(Object.ReferenceEquals(sprite,null))
            {
                p.GetComponent<PlantScript>().PlantSeedButton(true);
            }
            else
            {
                p.GetComponent<PlantScript>().PlantSeedButton(false);
            }
        }
    }

    public void CheckIfPlantExists_toSellPlant()
    {
        //Esse void vai checkar se a planta tem algum sprite. Se não tiver, o botão de plantar aparece, caso contrário, ele não aparece
        foreach (GameObject p in Plant)
        {
            Sprite sprite = p.GetComponent<SpriteRenderer>().sprite;
            if(Object.ReferenceEquals(sprite,null))
            {
                p.GetComponent<PlantScript>().SellPlantButton(false);
            }
            else
            {
                p.GetComponent<PlantScript>().SellPlantButton(true);
            }
        }
    }

    public void Clear()
    {
        foreach(GameObject p in Plant)
        {
            p.GetComponent<PlantScript>().PlantSeedButton(false);
            p.GetComponent<PlantScript>().SellPlantButton(false);
        }
    }
}

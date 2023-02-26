using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPlant : MonoBehaviour
{
    public bool vasefull;
    public int vasenumber;
    GameObject planty;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.Find("Planta"+vasenumber))
        {
            planty = GameObject.Find("Planta"+vasenumber);
        }
        
    }

    public void VaseFu()
    {
        vasefull = false;
        planty.GetComponent<IndividualPlant>().SellPlant();
        //GameObject.Find("Planta"+vasenumber).GetComponent<IndividualPlant>().SellPlant();     
        print(planty);    
    }

    public void thisVaseIsFull(bool n)
    {
        vasefull = n;
    }
}

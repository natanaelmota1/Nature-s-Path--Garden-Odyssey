using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseInfo : MonoBehaviour
{
    public bool vasefull;
    public int vasenumber;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VaseFull()
    {
        string Plantype = GameObject.Find("UIManager").GetComponent<UiManager>().SeedSelection;
        vasefull = true;
        GameObject.Find("Planta"+vasenumber).GetComponent<IndividualPlant>().thisplant(Plantype);        
    }

    public void thisVaseIsFull(bool n)
    {
        vasefull = n;
    }
    
}

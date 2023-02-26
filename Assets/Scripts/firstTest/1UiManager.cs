using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public string SeedSelection;
    GameObject[] vaseButtonTag;
    GameObject[] RemoveButtonTag;
    void Start()
    {
        vaseButtonTag = GameObject.FindGameObjectsWithTag("VaseButton");
        RemoveButtonTag = GameObject.FindGameObjectsWithTag("RemovePlant");
        print(vaseButtonTag);
        
        foreach (GameObject vase in vaseButtonTag) {
        print(vase);
        vase.SetActive(false);
        }

        foreach (GameObject remove in RemoveButtonTag) {
        remove.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckVaseFull()
    {
        // Este void irá mostrar os vasos que estão disponíveis para plantil.
        // Quando o jogador selecionar qual semente ele quer plantar, esse void será ativado
        foreach(GameObject vase in vaseButtonTag)
        {
            
            if (vase.GetComponent<VaseInfo>().vasefull == false)
            {
                vase.SetActive(true);
            }
            else
            {
                vase.SetActive(false);
            }
        }
        
    }

    public void SeedSelect(string seed)
    {
        SeedSelection = seed;
    }

    public void ClearCheckVase()
    {
        foreach(GameObject vase in vaseButtonTag)
        {
            vase.SetActive(false);
        }
        SeedSelection = "";
        foreach (GameObject remove in RemoveButtonTag) 
        {
            remove.SetActive(false);
        }
    }

    public void CheckRemovePlant()
    {
        foreach(GameObject remove in RemoveButtonTag)
        {
            
            if (remove.GetComponent<SellPlant>().vasefull == true)
            {
                remove.SetActive(true);
            }
            else
            {
                remove.SetActive(false);
            }
        }
    }

    public void isFull(bool bo, int i)
    {
        GameObject this_vase = vaseButtonTag[1-i];
        GameObject this_remove = RemoveButtonTag[1-i];
        this_vase.GetComponent<VaseInfo>().thisVaseIsFull(bo);
        this_remove.GetComponent<SellPlant>().thisVaseIsFull(bo);
    }

}

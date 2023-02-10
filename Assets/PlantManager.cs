using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    private List<int> myList = new List<int> {};
    void Start()
    {
        // Serialize the list into a JSON string
        string json = JsonUtility.ToJson(myList);

        // Save the string to PlayerPrefs
        PlayerPrefs.SetString("MyList", json);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Plant(int PlantNum)
    {
        // GameObject vase = GameObject.Find("Vase"+PlantNum);
        if (!GameObject.Find("Planta"+1))
        {
            long PlantedTime = System.DateTime.Now.Ticks;
            long lday = PlantedTime / 864000000000;
            long lsec = PlantedTime % 864000000000 / 10000000;

            PlayerPrefs.SetInt("DayPlanted"+PlantNum,(int)lday);
            PlayerPrefs.SetInt("SecondPlanted"+PlantNum,(int)lsec);
        }
        
    }

    private void OnApplicationQuit()
    {
        // Retrieve the JSON string from PlayerPrefs
        string json = PlayerPrefs.GetString("MyList", string.Empty);

        // Deserialize the JSON string back into a list
        myList = JsonUtility.FromJson<List<int>>(json);
    }
}

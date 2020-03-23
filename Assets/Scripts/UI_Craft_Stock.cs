using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Craft_Stock : MonoBehaviour
{
    public GameObject prefabImage;
    public GameObject prefabText;
    List<GameObject> liste_prefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AffichetStock();
        
    }
    public void AffichetStock()
    {
        clear();
        Populate();
    }

    public void clear()
    {
        foreach (GameObject g in liste_prefabs)
        {
            Destroy(g);
        }
    }

    public void Populate()
    {
        GameObject newObjImage;
        GameObject newObjText;
        foreach (RessourceManager.MaterialRessourceType ressource in Enum.GetValues(typeof(RessourceManager.MaterialRessourceType)))
        {
            if (ressource > 0)
            {
                newObjText = (GameObject)Instantiate(prefabText, transform);
                string s = " " + RessourceManager.Instance.get_Ressource(ressource).nb + " x";
                if ((int)ressource > 1)
                {
                    s = "+" + s;
                }
                newObjText.GetComponent<Text>().text = s;
                liste_prefabs.Add(newObjText);
                newObjImage = (GameObject)Instantiate(prefabImage, transform);
                newObjImage.GetComponent<Image>().sprite = RessourceManager.Instance.get_Ressource(ressource).image;
                liste_prefabs.Add(newObjImage);
            }
        }
    }
}


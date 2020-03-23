using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Sell_Stock : MonoBehaviour
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
        foreach (RessourceManager.WeaponRessourceType weapon in Enum.GetValues(typeof(RessourceManager.WeaponRessourceType)))
        {
            if (weapon > 0)
            {
                newObjText = (GameObject)Instantiate(prefabText, transform);
                string s = " " + RessourceManager.Instance.get_Arme(weapon).nb + " x";
                if ((int)weapon > 1)
                {
                    s = "+" + s;
                }
                newObjText.GetComponent<Text>().text = s;
                liste_prefabs.Add(newObjText);
                newObjImage = (GameObject)Instantiate(prefabImage, transform);
                newObjImage.GetComponent<Image>().sprite = RessourceManager.Instance.get_Arme(weapon).image;
                liste_prefabs.Add(newObjImage);
            }
        }
    }
}



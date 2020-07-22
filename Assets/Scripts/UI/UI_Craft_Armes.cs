using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Craft_Armes : MonoBehaviour
{
    public GameObject prefab;
    public GameObject UI;
    public Button close;
    private GameObject button_selected= null;
    
    void Start()
    {
        Populate();
        close.onClick.AddListener(OnclickClose);
        Canvas c = GetComponentInParent<Canvas>();
        c.gameObject.SetActive(false);
    }


    void Update()
    {
        
        if (button_selected != null)
        {
            button_selected.transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }

    }

    public void Populate()
    {
       
        foreach(RessourceManager.WeaponRessourceType weapon in Enum.GetValues(typeof(RessourceManager.WeaponRessourceType)))
        {
            if(weapon != RessourceManager.WeaponRessourceType.None)
            {
                GameObject newObj;
                RessourceManager.Arme arme = RessourceManager.Instance.get_Arme(weapon);
                newObj = (GameObject)Instantiate(prefab, transform);
                newObj.transform.GetChild(0).GetComponent<Image>().sprite = arme.image;
                newObj.GetComponent<Button>().onClick.AddListener(delegate { OnclickArme(weapon,newObj); });
               
            }
        }
    }

    public void OnclickArme(RessourceManager.WeaponRessourceType type, GameObject b)
    {
        UI.GetComponentInChildren<UI_Craft_ressources>().Afficher_Ressources_Necessaires(type);
        if (button_selected != null)
        {
            button_selected.transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        }
        button_selected = b;
    }

    public void OnclickClose()
    {
        Canvas c = GetComponentInParent<Canvas>();
        c.gameObject.SetActive(false);
        RessourceManager.Instance.set_UI_Craft_Active(false);
        Time.timeScale = 1f;
    }
}

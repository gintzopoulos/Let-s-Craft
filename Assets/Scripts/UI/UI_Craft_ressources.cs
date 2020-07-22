using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class UI_Craft_ressources : MonoBehaviour
{
    public GameObject prefabImage;
    public GameObject prefabText;
    List<GameObject> liste_prefabs = new List<GameObject>();
    public Text TextBubble;
    public Text text_nb_to_create;

    public Button boutonAjout;
    public Button boutonEnlever;
    public Button bouton_produire;
    public Button boutonClose;

    public AudioSource audioSourceAjout;
    public AudioSource audioSourceMoins;
    public AudioSource audioSourceCreer;

    public GameObject m_base;
    public Text puissance;
    public Text vente;
    private RessourceManager.WeaponRessourceType m_type = RessourceManager.WeaponRessourceType.None;
    private int nb_to_create=0;
    private uint max_to_create = 0;

    void Start()
    {
        boutonAjout.interactable = false;
        boutonEnlever.interactable = false;
        boutonAjout.onClick.AddListener(delegate { Set_nb_to_create(1); audioSourceAjout.Play(); });
        boutonEnlever.onClick.AddListener(delegate { Set_nb_to_create(-1); audioSourceMoins.Play(); });
        bouton_produire.onClick.AddListener(Produire);
    }

    public void Produire()
    {
        audioSourceCreer.Play();
        m_base.GetComponent<Batiment_Production_Arme>().set_Production(m_type,nb_to_create);
        foreach (RessourceManager.Ressources_necessaire r in RessourceManager.Instance.get_Arme(m_type).ressources_necessaire)
        {
            uint mult = r.nb*(uint)nb_to_create;
            RessourceManager.Instance.Supprimer(r.type, mult);
        }

        if(nb_to_create != 0) { boutonClose.onClick.Invoke(); }
        

    }
    public bool PeutEtreActif(bool btn)
    {
        if (btn)
        {
            return nb_to_create < max_to_create;
        }
        else
        {
            return nb_to_create > 0;
        }
    }
    public void Afficher_Ressources_Necessaires(RessourceManager.WeaponRessourceType type)
    {
        this.m_type = type;
        clear();
        Populate();
    }
    public void clear()
    {
        foreach(GameObject g in liste_prefabs)
        {
            Destroy(g);
        }
    }
    void Update()
    {
        Update_Max_Production();
        bouton_produire.interactable = nb_to_create > 0;
        if(m_type!= RessourceManager.WeaponRessourceType.None)
        {
            RessourceManager.Arme arme = RessourceManager.Instance.get_Arme(m_type);
            puissance.text = "Puissance : "+arme.puissance.ToString();
            vente.text = "Vente : " + arme.prix.ToString();
        }
       
    }
    void Set_nb_to_create(int nb)
    {
        nb_to_create += nb;
        text_nb_to_create.text = nb_to_create.ToString();
    }
    public void Update_Max_Production()
    {
        if (m_type != RessourceManager.WeaponRessourceType.None)
        {
            max_to_create = m_base.GetComponent<Batiment_Production_Arme>().Calcul_Max_Production(m_type);

            TextBubble.text = "Vous pouvez créer au maximum " + max_to_create + " " + m_type;

            boutonAjout.interactable = PeutEtreActif(true);
            boutonEnlever.interactable = PeutEtreActif(false);
            if (nb_to_create > max_to_create) { Set_nb_to_create(-((int)nb_to_create - (int)max_to_create)); }
        }
    }
    public void Populate()
    {
        GameObject newObjImage;
        GameObject newObjText;
        RessourceManager.Ressources_necessaire[] ressources_n = RessourceManager.Instance.get_Arme(m_type).ressources_necessaire;
        List<RessourceManager.Ressources_necessaire> list = new List<RessourceManager.Ressources_necessaire>(ressources_n);
        foreach (RessourceManager.Ressources_necessaire material in ressources_n)
        {
            newObjText = (GameObject)Instantiate(prefabText, transform);
            string s = " " + material.nb + " x";
            if (list.IndexOf(material) > 0)
            {
                s = "+"+s;
            }
            newObjText.GetComponent<Text>().text = s;
            liste_prefabs.Add(newObjText);
            newObjImage = (GameObject)Instantiate(prefabImage, transform);
            newObjImage.GetComponent<Image>().sprite = RessourceManager.Instance.get_Ressource(material.type).image;
            liste_prefabs.Add(newObjImage);            
        }
    }
}

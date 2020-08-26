using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class UI_Sell_Ressources : MonoBehaviour
{
    //public GameObject prefabImage;
    //public GameObject prefabText;
    public Toggle tglArmy1;
    public Toggle tglArmy2;
    public Text txtArmy1;
    public Text txtArmy2;

    int armyChoice;

    List<GameObject> liste_prefabs = new List<GameObject>();
    public Text TextBubble;
    public Text text_nb_to_sell;

    public Button boutonAjout;
    public Button boutonEnlever;
    public Button button_sell;

    public AudioSource audioSourceAjout;
    public AudioSource audioSourceMoins;
    public AudioSource audioSourceSell;

    public GameObject m_warSystem;
    public Text puissance;
    public Text vente;
    private RessourceManager.WeaponRessourceType m_type = RessourceManager.WeaponRessourceType.None;
    private uint nb_to_sell = 0;
    private uint max_to_sell = 0;

    void Start()
    {
        boutonAjout.interactable = false;
        boutonEnlever.interactable = false;
        boutonAjout.onClick.AddListener(delegate { Set_nb_to_sell(1); audioSourceAjout.Play(); });
        boutonEnlever.onClick.AddListener(delegate { Set_nb_to_sell(-1); audioSourceMoins.Play(); });
        button_sell.onClick.AddListener(Sell);
        armyChoice = 1;
        tglArmy1.onValueChanged.AddListener(delegate
        {
            if (tglArmy1.isOn) { armyChoice = 1; }
            else { armyChoice = 0; }
            audioSourceAjout.Play();
            Debug.Log("CHANGED : " + armyChoice);
        });
        txtArmy1.text = m_warSystem.GetComponent<WarSystem>().army1.name;
        txtArmy2.text = m_warSystem.GetComponent<WarSystem>().army2.name;
    }

    public void Sell()
    {
        //audioSourceSell.Play();

        if (nb_to_sell > RessourceManager.Instance.get_Arme(m_type).nb) { return; }
        RessourceManager.Instance.Supprimer(m_type, nb_to_sell);

        m_warSystem.GetComponent<WarSystem>().SellWeapon(armyChoice, m_type, nb_to_sell);
    }

    public bool PeutEtreActif(bool btn)
    {
        if (btn)
        {
            return nb_to_sell < max_to_sell;
        }
        else
        {
            return nb_to_sell > 0;
        }
    }
    public void SetWeaponToSell(RessourceManager.WeaponRessourceType type)
    {
        this.m_type = type;
        clear();
    }

    public void clear()
    {
        foreach (GameObject g in liste_prefabs)
        {
            Destroy(g);
        }
    }

    void Update()
    {
        Update_Max_Selling();
        button_sell.interactable = nb_to_sell > 0;
        if (m_type != RessourceManager.WeaponRessourceType.None)
        {
            RessourceManager.Arme arme = RessourceManager.Instance.get_Arme(m_type);
            float prixArme = (float)arme.prix;
            float reputation = RessourceManager.Instance.getReputation();
            float percent = reputation / 100;
            float prix_final = prixArme * percent;
            
            puissance.text = "Puissance : " +arme.puissance.ToString();
            vente.text = "Vente : " + prix_final.ToString();
        }
    }

    void Set_nb_to_sell(int nb)
    {
        nb_to_sell += (uint)nb;
        text_nb_to_sell.text = nb_to_sell.ToString();
    }

    public void Update_Max_Selling()
    {
        if (m_type != RessourceManager.WeaponRessourceType.None)
        {
            max_to_sell = RessourceManager.Instance.get_Arme(m_type).nb;

            TextBubble.text = "Vous pouvez vendre au maximum " + max_to_sell + " " + m_type;

            boutonAjout.interactable = PeutEtreActif(true);
            boutonEnlever.interactable = PeutEtreActif(false);
            if (nb_to_sell > max_to_sell) { Set_nb_to_sell(-((int)nb_to_sell - (int)max_to_sell)); }
        }
    }
}

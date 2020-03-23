using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Batiment_Production : Interactable
{
    public Button boutonPlus;
    public Button boutonMoins;
    protected int m_nb_serviteur = 0;
    protected int m_nb_serviteur_arrives = 0;
    public Text myText;
    protected string m_nb_serviteur_string;
    protected float tpsProd;
    protected float tpsProdDépart;
    protected float mutliplicateur_tps = 0.0f;
    public AudioSource audioSourcePlus;
    public AudioSource audioSourceMoins;
    public AudioSource audioSourceRessource;
    public AudioSource audioSourceErreur;
    public Slider progress;
    public GameObject prefab_serviteur;
    public int serviteurArrivee=0;
    

    protected void Start()
    {
        boutonPlus.onClick.AddListener(OnClickPlus);
        boutonMoins.onClick.AddListener(OnClickMoins);
        progress.maxValue = 100;
        progress.minValue = 0;
        progress.value = 100;
        progress.interactable = false;
    }
    public override void interagir(GameObject serviteur) 
    {
        StartCoroutine("interagir_Coroutine", serviteur);

    }

    IEnumerator interagir_Coroutine(GameObject serviteur)
    {
        serviteur.GetComponent<Serviteur>().disparaitre();
        yield return new WaitForSeconds(2f);
        if (serviteur.GetComponent<Serviteur>().get_Est_assigne()==false)
        {
            mutliplicateur_tps += 0.5f;
            m_nb_serviteur_arrives += 1;
        }
       
        Destroy(serviteur);
        serviteurArrivee += 1;
    }
    void OnClickPlus()
    {
        audioSourcePlus.Play();
        m_nb_serviteur += 1;
        RessourceManager.Instance.utiliser_serviteur();
        Spawn_Ajout();
    }
    void OnClickMoins()
    {       
        if (m_nb_serviteur > 0)
        {
            audioSourceMoins.Play();
            m_nb_serviteur -= 1;
            m_nb_serviteur_arrives -= 1;
            mutliplicateur_tps -= 0.5f;
            RessourceManager.Instance.utiliser_serviteur(true);
            Spawn_Supprime();
            serviteurArrivee -= 1;
        }
        if (m_nb_serviteur <= 0)
        {
            audioSourceErreur.Play();
            tpsProd = tpsProdDépart;
            mutliplicateur_tps = 0;
            progress.value = 100;
        }
    }

    protected abstract void Spawn_Ajout();
    protected abstract void Spawn_Supprime();
    protected void Update()
    {
        DisplayServiteur();
        Produire();
        boutonMoins.interactable = Isactivatble(true);
        boutonPlus.interactable = Isactivatble();
        
   }

    bool Isactivatble(bool moins=false)
    {
        if (moins)
        {
            return m_nb_serviteur_arrives == m_nb_serviteur && m_nb_serviteur > 0;
        }
        else
        {
            return RessourceManager.Instance.get_Nb_Serviteurs_restants() > 0;
        }
    }
    void DisplayServiteur()
    {
       myText.text = m_nb_serviteur_string;
        m_nb_serviteur_string = m_nb_serviteur.ToString();
    }

    public abstract void Produire();


}

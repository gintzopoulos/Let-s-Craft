using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject prefab_serviteur;
    public Sprite transparent_Image;
    public Canvas Warning_Bubble_Ressources;
    public Canvas Warning_Bubble_Arme;
    public AudioSource audioSourceVoix;

    public void Ajouter_Serviteur(GameObject destination,GameObject intermediaire)
    {
        GameObject s = Instantiate(prefab_serviteur, RessourceManager.Instance.get_target(RessourceManager.Target.chambre).transform.position, Quaternion.identity);
        s.GetComponent<Serviteur>().init(destination, null, s,Warning_Bubble_Ressources,Warning_Bubble_Arme, audioSourceVoix, intermediaire);
        s.GetComponentInChildren<Image>().sprite = transparent_Image;
        RessourceManager.Instance.get_target(RessourceManager.Target.chambre).GetComponent<Chambre>().supp_serviteur();

    }
    public void Supprimer_Serviteur(GameObject depart, GameObject intermediaire)
    {
        GameObject s = Instantiate(prefab_serviteur, depart.transform.position, Quaternion.identity);
        s.GetComponentInChildren<Image>().sprite = transparent_Image;
        //prefab_serviteur.GetComponent<Serviteur>().set_assigne(false);
        s.GetComponent<Serviteur>().init(RessourceManager.Instance.get_target(RessourceManager.Target.chambre), null, s, Warning_Bubble_Ressources, Warning_Bubble_Arme, audioSourceVoix, intermediaire);
        
    }
    public void Ajouter_Au_Depot(GameObject depot, GameObject depart, Sprite type, GameObject intermediaire)
    {
        GameObject s = Instantiate(prefab_serviteur, depart.transform.position, Quaternion.identity);
        s.GetComponentInChildren<Image>().sprite = type;
        s.GetComponent<Serviteur>().set_assigne(true);
        s.GetComponent<Serviteur>().init(depot, depart, s, Warning_Bubble_Ressources, Warning_Bubble_Arme, audioSourceVoix, intermediaire);
        

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

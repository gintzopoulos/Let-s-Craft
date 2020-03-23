using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Batiment_Production_Arme : Batiment_Production
{
    public AudioSource audioSourceOpen;
    public AudioSource audioSourceCreer;
    public Button boutonProduction;
    public Canvas UICraft;
    public SpriteRenderer bubble;
    private GameObject depot;
    public SpriteRenderer forgeron;
    public SpriteRenderer forgeron_qui_sennuie;
    public ParticleSystem particle;


    private RessourceManager.WeaponRessourceType m_type_ressource_produite = RessourceManager.WeaponRessourceType.Epee;
    private int nb_to_create =0;

    protected override void Spawn_Ajout()
    {
        SpawnManager.Instance.Ajouter_Serviteur(this.gameObject, null);

    }

    protected override void Spawn_Supprime()
    {
        SpawnManager.Instance.Supprimer_Serviteur(this.gameObject, null);

    }
    public uint Calcul_Max_Production(RessourceManager.WeaponRessourceType type)
    {
        List<uint> liste_max = new List<uint>();
        RessourceManager.Ressources_necessaire[] ressources_n = RessourceManager.Instance.get_Arme(type).ressources_necessaire;
        foreach (RessourceManager.Ressources_necessaire ressource in ressources_n)
        {
            uint nb_in_stock = RessourceManager.Instance.get_Ressource(ressource.type).nb;
            if (nb_in_stock <= 0)
            {
                return 0;
            }
            uint nb = nb_in_stock / ressource.nb;
            liste_max.Add(nb);
        }
        uint max = liste_max.Min();
        return max;
    }
    public override void Produire()
    {
       if (m_nb_serviteur > 0)
        {
            
            if (m_type_ressource_produite != RessourceManager.WeaponRessourceType.None)
                    {
                        if (nb_to_create!=0) { 
                            tpsProd -= Time.deltaTime * mutliplicateur_tps;       
                            if (tpsProd <= 0)
                            {
                                tpsProd = tpsProdDépart;
                                SpawnManager.Instance.Ajouter_Au_Depot(depot, this.gameObject, RessourceManager.Instance.get_Arme(m_type_ressource_produite).image, RessourceManager.Instance.get_target(RessourceManager.Target.porteHaut));
                                nb_to_create -= 1;
                                audioSourceRessource.Play();
                            }
                            if (tpsProd != tpsProdDépart)
                            {
                                progress.value = 100 - ((tpsProd / tpsProdDépart) * 100);
                            }
                        }
                        else
                        {
                            set_Production(RessourceManager.WeaponRessourceType.None, 0);
                        }
                    }
   
        }

        if (serviteurArrivee > 0) {
            if (nb_to_create > 0) {
                forgeron.gameObject.SetActive(true);
                forgeron_qui_sennuie.gameObject.SetActive(false);
                particle.gameObject.SetActive(true);
            }
            else {
                forgeron_qui_sennuie.gameObject.SetActive(true);
                forgeron.gameObject.SetActive(false);
                particle.gameObject.SetActive(false);
            }
            
        }
        else { forgeron.gameObject.SetActive(false); forgeron_qui_sennuie.gameObject.SetActive(false); }
    }

    public void set_Production(RessourceManager.WeaponRessourceType type, int nb)
    {
        audioSourceCreer.Play();

        if (nb_to_create <= 0)
        {
            m_type_ressource_produite = type;
            nb_to_create = nb;
            tpsProdDépart = RessourceManager.Instance.get_Arme(m_type_ressource_produite).temps_producion;
            tpsProd = tpsProdDépart;
            Image[] images = progress.GetComponentsInChildren<Image>();
            for(int i =0; i<images.Length; i++)
            {
                images[i].sprite = RessourceManager.Instance.get_Arme(type).image;
                if (i == 0)
                {
                    images[i].color =new Color(0.235f, 0.235f, 0.235f);
                }
            }

            if (m_type_ressource_produite == RessourceManager.WeaponRessourceType.None){bubble.gameObject.SetActive(true);}
            else { bubble.gameObject.SetActive(false); this.depot = RessourceManager.Instance.get_target(RessourceManager.Instance.get_depot(m_type_ressource_produite));}
        }     
    }
    new void Update()
    {
        base.Update();
        if (nb_to_create <= 0)
        {
            boutonProduction.interactable = true;
        }
        else { boutonProduction.interactable = false; }
       
        
    }
    new void Start()
    {      
        base.Start();
        boutonProduction.onClick.AddListener(open);
        boutonMoins.onClick.Invoke();
        particle.gameObject.SetActive(false);
    }

    public void open()
    {
        audioSourceOpen.Play();
        UICraft.gameObject.SetActive(true);
        RessourceManager.Instance.set_UI_Craft_Active(true);
    }

}

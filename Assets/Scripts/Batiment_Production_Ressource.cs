using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Batiment_Production_Ressource : Batiment_Production
{
    public RessourceManager.MaterialRessourceType type_ressource_produite;
    private GameObject depot;
    public ParticleSystem particle;

    public override void Produire()
    {
        tpsProd -= Time.deltaTime * mutliplicateur_tps;
        if (tpsProd <= 0)
        {
            tpsProd = tpsProdDépart;
            SpawnManager.Instance.Ajouter_Au_Depot(depot,this.gameObject, RessourceManager.Instance.get_Ressource(type_ressource_produite).image, null);
            audioSourceRessource.Play();
        }
        if (tpsProd != tpsProdDépart)
        {
            progress.value = 100 - ((tpsProd / tpsProdDépart) * 100);
        }
    }

    protected override void Spawn_Ajout()
    {
        SpawnManager.Instance.Ajouter_Serviteur(this.gameObject, RessourceManager.Instance.get_target(RessourceManager.Target.porteHaut));

    }

    protected override void Spawn_Supprime()
    {
        SpawnManager.Instance.Supprimer_Serviteur(this.gameObject, RessourceManager.Instance.get_target(RessourceManager.Target.porteBas));

    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        tpsProdDépart = RessourceManager.Instance.get_Ressource(type_ressource_produite).temps_producion;
        tpsProd = tpsProdDépart;
        this.depot = RessourceManager.Instance.get_target(RessourceManager.Instance.get_depot(type_ressource_produite));
        particle.gameObject.SetActive(false);
    }

    new void Update()
    {
        base.Update();
        if (m_nb_serviteur_arrives > 0)
        {
            particle.gameObject.SetActive(true);
        }
        else { particle.gameObject.SetActive(false); }
    }
}

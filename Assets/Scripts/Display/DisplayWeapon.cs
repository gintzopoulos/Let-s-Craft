using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWeapon : DisplayRessource
{

    public RessourceManager.WeaponRessourceType type;

    public override void interagir(GameObject serviteur)
    {
        serviteur.GetComponent<Serviteur>().Agir_Stock(type);
        //Debug.Log("hello" + this.ToString());
        //Destroy(serviteur);
        //RessourceManager.Instance.Ajouter(type);
        //RessourceManager.Instance.Ajouter(type);
        //serviteur.GetComponent<Serviteur>().retour();
    }
    protected override void Update()
    {
        nb = RessourceManager.Instance.get_Arme(type).nb.ToString();
        DisplayInfo();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contrat : MonoBehaviour
{
    private int army;
    private RessourceManager.WeaponRessourceType typeArme;
    private int quantitee; 
    //limite de temps

    // Start is called before the first frame update
    void Start()
    {
        //InitContrat();
        //Debug.Log(army);
        //Debug.Log(typeArme);
        //Debug.Log(quantitee);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Contrat() 
    {
        
        int rand = Random.Range(1,11);
        if (rand >= 5)
        {
            army = 2;
        }
        else { army = 1; }

        quantitee = Random.Range(1,31);
        switch (quantitee)
        {
           case 1:
                quantitee = 1;
                break;
           case 2:
                quantitee = 10;
                break;
           case 3:
           case 4:
                quantitee = 9;
                break;
           case 5:
           case 6:
                quantitee = 2;
                break;
           case 7:
           case 8:
           case 9:
                quantitee = 8;
                break;
           case 10:
           case 11:
           case 12:
                quantitee = 3;
                break;
            case 13:
            case 14:
            case 15:
            case 16:
                quantitee = 7;
                break;
            case 17:
            case 18:
            case 19:
            case 20:
                quantitee = 4;
                break;
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
                quantitee = 5;
                break;
            case 26:
            case 27:
            case 28:
            case 29:
            case 30:
                quantitee = 6;
                break;

        }

        typeArme = (RessourceManager.WeaponRessourceType)Random.Range(1, System.Enum.GetValues(typeof(RessourceManager.WeaponRessourceType)).Length+1);
    }
    public int getArmy(){return army;}
    public RessourceManager.WeaponRessourceType getTypeArme() { return typeArme; }
    public int getQuantity() { return quantitee; }

}

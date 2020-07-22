using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerAcheteur : MonoBehaviour
{
    public GameObject prefab_Acheteur;
    public GameObject target;
    private ArrayList posAcheteur;
    private GameObject last;
    private GameObject first;
    private int limiteSpawn = 2;
    private int cpt = 0;
    


    void Start()
    {
        posAcheteur = new ArrayList();
        spawnAcheteur();
    }

    void Update()
{
        first = (GameObject)posAcheteur[0];
        last = (GameObject)posAcheteur[posAcheteur.Count - 1];
        Acheteur lastAcheteur = last.GetComponent<Acheteur>();
        
        if (lastAcheteur.getBool_estArrive() == true && cpt < limiteSpawn)
        {
            spawnAcheteur();
        }

        DeSpawnAcheteur();
        MoveInLine();
        setPremier();
    }

    void spawnAcheteur()
    {
        GameObject s = Instantiate(prefab_Acheteur, new Vector2(49f,-3.45f), Quaternion.identity);
        Acheteur acheteur = s.GetComponent<Acheteur>();
        if (posAcheteur.Count == 0)
        {
            acheteur.setTargetA(target.transform.position);
        }
        else
        {
            acheteur.setTargetA(last.transform.position);
        }
        acheteur.setTargetF(this.transform.position); // defini la target de despawn de l'acheteur
        cpt++;
        posAcheteur.Add(s);
    }

    void DeSpawnAcheteur()
    { 
        if (first.GetComponent<Acheteur>().getBool_aTermine())
        {
            posAcheteur.Remove(first);
            cpt--;
        }
    }

    void MoveInLine()
    {
        GameObject indiceTab;
        Acheteur acheteur;
        
        for (int i = 0; i < posAcheteur.Count; i++)
        {
            indiceTab = (GameObject)posAcheteur[i];
            acheteur = indiceTab.GetComponent<Acheteur>();
            //StartCoroutine("Wait_Coroutine");
            if (i == 0)
            {
                acheteur.setTargetA(target.transform.position);
            }
            else
            {
                GameObject newTarget = (GameObject)posAcheteur[i-1];
                acheteur.setTargetA(newTarget.transform.position);
            }
        }
    }
    
    public void setPremier()
    {
        first.GetComponent<Acheteur>().setEstPremierTrue();
       
    }
}

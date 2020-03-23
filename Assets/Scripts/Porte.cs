using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : Interactable
{
    Animator anim;
    public GameObject destination;

    public override void interagir(GameObject serviteur)
    {

        serviteur.GetComponent<Serviteur>().agir_Porte();
        StartCoroutine("teleporte", serviteur);
    }

    public IEnumerator ouvrirPorteCoroutine()
    {
        anim.SetBool("ouvert", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("ouvert", false);

    }
    public void ouvrirPorte()
    {
        StartCoroutine("ouvrirPorteCoroutine");
    }

    public IEnumerator teleporte(GameObject serviteur)
    {
        anim.SetBool("ouvert", true);
        yield return new WaitForSeconds(2);
        anim.SetBool("ouvert", false);
        serviteur.GetComponent<Serviteur>().teleporter(destination);
        destination.GetComponent<Porte>().ouvrirPorte();
    }
    public void teleporter(GameObject serviteur)
    {
        StartCoroutine("teleporte", serviteur);

    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

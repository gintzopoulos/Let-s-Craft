using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Chambre : Interactable
{
    public Text text_nb_serviteurs;
    public Text text_max_serviteurs;
    public Button bouton_acheter;
    public SpriteRenderer[] serviteurs;
    int serviteurArrivee = 0;
   
    public override void interagir(GameObject serviteur)
    {
        StartCoroutine("interagir_Coroutine", serviteur);

    }

    IEnumerator interagir_Coroutine(GameObject serviteur)
    {
        serviteur.GetComponent<Serviteur>().disparaitre();
        yield return new WaitForSeconds(2f);
        Destroy(serviteur);
        serviteurArrivee += 1;
    }
    public void supp_serviteur()
    {
        serviteurArrivee -= 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        bouton_acheter.onClick.AddListener(Acheter_Serviteur);
        StartCoroutine("FlipX");
        serviteurArrivee = RessourceManager.Instance.get_Nb_Serviteurs_restants();
    }

    public void Acheter_Serviteur()
    {
        RessourceManager.Instance.acheter_serviteur(500);
    }

    void Update()
    {
        int nb_serviteurs_restants = RessourceManager.Instance.get_Nb_Serviteurs_restants();
        int nb_serviteur_max = RessourceManager.Instance.get_Max_Serviteurs();
        text_max_serviteurs.text = "Serviteurs total : " + nb_serviteur_max.ToString();
        text_nb_serviteurs.text = "Serviteurs libres : " + nb_serviteurs_restants.ToString();
        bouton_acheter.interactable = RessourceManager.Instance.get_bourse() >= 500;
        update_Lits(nb_serviteurs_restants,nb_serviteur_max);

    }

    void update_Lits(int nb_serviteurs_restants, int nb_serviteur_max)
    {
       
        int nb_to_affiche = serviteurArrivee / (nb_serviteur_max / 3);
        if (nb_serviteurs_restants > 0)
        {
            for (int i = 0; i <= nb_to_affiche; i++)
            {
                serviteurs[i].gameObject.SetActive(true);
            }
            for (int i = serviteurs.Length - 1; i > nb_to_affiche; i--)
            {
                serviteurs[i].gameObject.SetActive(false);
            }
        }
        else if (nb_serviteurs_restants < 1)
        {
            serviteurArrivee = 0;
            foreach (SpriteRenderer s in serviteurs)
            {
                s.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator FlipX()
    {
        int i = Random.Range(0, 4);
        serviteurs[i].flipX = Random.Range(0, 2) > 0;

        yield return new WaitForSeconds(Random.Range(0f, 4f));

        StartCoroutine("FlipX");
    }
}

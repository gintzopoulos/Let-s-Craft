using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Serviteur : MonoBehaviour
{
    Canvas Warning_Bubble_Arme;
    Canvas Warning_Bubble_Ressources;
    float speed;
    public GameObject target;
    Animator animator;
    public GameObject serviteur;
    public GameObject origin;
    public GameObject target_intermediaire;
    public Animator animator_state;
    RessourceManager.MaterialRessourceType type;
    bool Isobjectif_atteint = false;
    private bool estAssigne = false;
    public Button b;
    private int sens = 1;
    public AudioSource audioSourceVoix;
    public int nbCoupDeFouet = 0;

    public Serviteur()
    {

    }


    public void set_assigne(bool est) { this.estAssigne = est; }
    public bool get_Est_assigne() { return this.estAssigne; }

    public void init(GameObject t, GameObject origin, GameObject serviteur, Canvas res, Canvas arme, AudioSource audioSourceVoix, GameObject tI = null)
    {
        target = t;
        this.serviteur = serviteur;
        this.origin = origin;
        target_intermediaire = tI;
        this.Warning_Bubble_Ressources = res;
        this.Warning_Bubble_Arme = arme;
        this.audioSourceVoix = audioSourceVoix;

    }
    public void accelerer()
    {
        if (this.speed < 3)
        {
            this.speed = 0;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(200 * sens, 150));
            this.speed = 5;
            animator.runtimeAnimatorController = RessourceManager.Instance.get_Animator(speed);
            animator.speed = speed;
            audioSourceVoix.Play();
            nbCoupDeFouet++;
        }

    }
    public void retour(GameObject intermediaire)
    {
        GameObject tmp = origin;
        origin = target;
        target = tmp;
        Isobjectif_atteint = false;
        this.target_intermediaire = intermediaire;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        this.speed = Random.Range(1, 5);
        animator.runtimeAnimatorController = RessourceManager.Instance.get_Animator(speed);
        animator.speed = speed;
        b.onClick.AddListener(accelerer);

    }
    void Update()
    {
        if (!Isobjectif_atteint)
        {

            GameObject the_target_transform = null;
            if (target_intermediaire == null) { the_target_transform = target; }
            else if (target_intermediaire != null) { the_target_transform = target_intermediaire; }
            objectif_atteint(the_target_transform);
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(the_target_transform.transform.position.x, transform.position.y), step);
            Update_animation(the_target_transform);

        }
    }

    public void Agir_Stock(RessourceManager.MaterialRessourceType type)
    {
        this.type = type;
        StartCoroutine("Ranger_Stock", type);
    }
    public void Agir_Stock(RessourceManager.WeaponRessourceType type)
    {
        //this.type = type;
        StartCoroutine("Ranger_Stock", type);
    }

    public void objectif_atteint(GameObject the_target_transform)
    {

        if (!Isobjectif_atteint)
        {
            float step = speed * Time.deltaTime;

            if (transform.position.x == the_target_transform.transform.position.x)
            {

                if (target_intermediaire == null)
                {
                    target.GetComponent<Interactable>().interagir(serviteur);
                    Isobjectif_atteint = true;
                }
                else if (target_intermediaire != null)
                {
                    target_intermediaire.GetComponent<Interactable>().interagir(serviteur);
                    target_intermediaire = null;
                    Isobjectif_atteint = true;
                }

            }

        }

    }


    //Pour anmations
    void Update_animation(GameObject thetarget)
    {

        if (transform.position.x < thetarget.transform.position.x)
        {
            animator.SetBool("VersGauche", false);
            animator.SetBool("VersDroite", true);
            sens = -1;
        }
        if (transform.position.x > thetarget.transform.position.x)
        {
            animator.SetBool("VersGauche", true);
            animator.SetBool("VersDroite", false);
            sens = 1;
        }
    }
    public void agir_Porte()
    {
        StartCoroutine("ouvrirPorte_Coroutine");
    }
    public void teleporter(GameObject destination)
    {
        StartCoroutine("teleporte_Coroutine", destination.transform);
    }
    public void apparaitre()
    {
        StartCoroutine("apparaitre_Coroutine");
    }
    public void disparaitre()
    {
        StartCoroutine("disparaitre_Coroutine");

    }
    IEnumerator ouvrirPorte_Coroutine()
    {
        disparaitre();
        yield return new WaitForSeconds(2f);

    }
    IEnumerator disparaitre_Coroutine()
    {
        animator.SetBool("Disparaitre", true);
        for (int i = 0; i <= 10; i++)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);

        }
        animator.SetBool("Disparaitre", false);
        //Isobjectif_atteint = true;
    }
    IEnumerator apparaitre_Coroutine()
    {
        // Isobjectif_atteint = false;
        animator.SetBool("Apparaitre", true);
        for (int i = 0; i <= 10; i++)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, color.a + 0.1f);
            yield return new WaitForSeconds(0.1f);

        }
        animator.SetBool("Apparaitre", false);


    }
    IEnumerator teleporte_Coroutine(Transform destination)
    {
        transform.position = destination.transform.position;
        apparaitre();
        yield return new WaitForSeconds(1f);
        Isobjectif_atteint = false;
    }
    IEnumerator Ranger_Stock(RessourceManager.MaterialRessourceType type)
    {
        animator.SetBool("Range_Stock", true);
        yield return new WaitForSeconds(2);
        RessourceManager.Instance.Ajouter(type);
        //if (RessourceManager.Instance.Ajouter(type) == false)
        //{
        //    Warning_Bubble_Ressources.GetComponent<Warning_Bubble>().setDisplay(true);
        //}
        //else
        //{
        //    Warning_Bubble_Ressources.GetComponent<Warning_Bubble>().setDisplay(false);
        //}
        serviteur.GetComponentInChildren<Image>().color = new Color(0f, 0f, 0f, 0f);
        retour(null);
        animator.SetBool("Range_Stock", false);
    }
    IEnumerator Ranger_Stock(RessourceManager.WeaponRessourceType type)
    {
        animator.SetBool("Range_Stock", true);
        yield return new WaitForSeconds(2);
        RessourceManager.Instance.Ajouter(type);
        //if (RessourceManager.Instance.Ajouter(type) == false)
        //{
        //    Warning_Bubble_Ressources.GetComponent<Warning_Bubble>().setDisplay(true);
        //}
        //else
        //{
        //    Warning_Bubble_Ressources.GetComponent<Warning_Bubble>().setDisplay(false);
        //}
        serviteur.GetComponentInChildren<Image>().color = new Color(0f, 0f, 0f, 0f);
        retour(RessourceManager.Instance.get_target(RessourceManager.Target.porteBas));
        animator.SetBool("Range_Stock", false);
    }

}

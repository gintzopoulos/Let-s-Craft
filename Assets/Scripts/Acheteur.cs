using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Acheteur : MonoBehaviour
{
    private Contrat contrat;
    Animator animator;
    float speed = 2;
    private Vector2 targetA;
    private Vector2 targetF;
    private bool estArrive = false;
    public bool aTerminer = false;
    private bool displayInfo = false;
    private bool afficheBulle = false;
    private bool estPremier = false;
    float timerContrat = 300.0f;
    int secondes;
    int minutes;

    public Canvas UIContrat;
    public Canvas BulleContrat;
    public TextMeshProUGUI QuantiteOr;
    public TextMeshProUGUI QuantiteArme;
    public Button Accept;
    public Button Reject;
    public GameObject ArmyContrat;
    public GameObject ArmeContrat;
    public Sprite ImageArmy1;
    public Sprite ImageArmy2;
    public TextMeshProUGUI Timer;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        contrat = new Contrat();
        animator.runtimeAnimatorController = RessourceManager.Instance.get_Animator(contrat.getArmy());
        SetUiContrat();
        Reject.onClick.AddListener(rejeter);
        Accept.onClick.AddListener(AccepterContrat);
       // Debug.Log(contrat.getTypeArme());
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        //Déplacement sprite
        if (aTerminer==false)
        {
           
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(targetA.x+1.5f, transform.position.y), step);

                if (transform.position.x == targetA.x + 1.5f)
                {
                    animator.SetBool("estArriver", true);
                    animator.SetBool("aTerminer", false);
                    estArrive = true;
                }
     
        }
        else
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(targetF.x, transform.position.y), step);
            
            animator.SetBool("estArriver", false);
            animator.SetBool("aTerminer", true);
            
            if (transform.position.x == targetF.x)
            {
                //estRevenu = true;
                Destroy(gameObject);
                
            }
        }

        FadeCanvas();
        FadeBulle();
        GetTimerContrat();
    }

    public void setTargetA(Vector2 t)
    {
        this.targetA = t;
    }

    public void setTargetF(Vector2 t)
    {
        this.targetF = t;
    }

    public bool getBool_estArrive() {
        return estArrive;
    }

    public bool getBool_aTermine()
    {
        return aTerminer;
    }

    public void setCanvaActive()
    {
        UIContrat.gameObject.SetActive(true);
    }

    public void setCanvaInactive()
    {
        UIContrat.gameObject.SetActive(false);
    }

    public void setBulleActive()
    {
        BulleContrat.gameObject.SetActive(true);
        afficheBulle = true;
    }

    public void setBulleInactive()
    {
        BulleContrat.gameObject.SetActive(false);
        afficheBulle = false;
        //Debug.Log("setBulleInactive");
    }

    void FadeCanvas()
    {
        if (displayInfo)
        {
            setCanvaActive();
        }

        else
        {
            setCanvaInactive();         
        }
    }

    void FadeBulle()
    {
        if (estArrive && estPremier && !aTerminer)
        {
            setBulleActive();
        }
        else
        {
            setBulleInactive();
        }

    }
    private void OnMouseOver()
    {
        if (estArrive && estPremier)
        {
            displayInfo = true;
            //afficheBulle = false;
        }
        
    }

    private void OnMouseExit()
    {
        displayInfo = false;
        //afficheBulle = true;
    }

    public void setEstPremierTrue()
    {
        estPremier = true;
    }

    public void SetUiContrat()
    {
        uint Qty = contrat.getQuantity();
        RessourceManager.WeaponRessourceType typeArme = contrat.getTypeArme();
        Sprite imageArme = RessourceManager.Instance.get_Arme(typeArme).image;
                
        setSpriteArmy();
        ArmeContrat.GetComponent<SpriteRenderer>().sprite = imageArme; 
        QuantiteArme.text = contrat.getQuantity().ToString();
        QuantiteOr.text = (RessourceManager.Instance.get_Arme(typeArme).prix * Qty).ToString();

    }

    public void rejeter()
    {
        aTerminer = true;
        RessourceManager.Instance.refuserContrat(contrat.getQuantity() * RessourceManager.Instance.get_Arme(contrat.getTypeArme()).prix);
    }

    public void setSpriteArmy()
    {
        //army1 = badbees / army2 = badskulls
        if (contrat.getArmy() == 1)
        {
            ArmyContrat.GetComponent<SpriteRenderer>().sprite = ImageArmy1;
        }
        else
        {
            ArmyContrat.GetComponent<SpriteRenderer>().sprite = ImageArmy2;
        }
    }

    public void GetTimerContrat()
    {
        if (estArrive && estPremier)
        {
            timerContrat -= Time.deltaTime;
            secondes = (int)(timerContrat % 60);
            minutes = (int)(timerContrat / 60);
            Timer.text = minutes.ToString() + "," + secondes.ToString();

            if (timerContrat <= 0.0f)
            {
                rejeter();
            }
        }



    }

    public void AccepterContrat()
    {
        if (contrat.getQuantity() > RessourceManager.Instance.get_Arme(contrat.getTypeArme()).nb) { return; }
        RessourceManager.Instance.Supprimer(contrat.getTypeArme(), contrat.getQuantity());

        WarSystem.Instance.SellWeapon(contrat.getArmy(), contrat.getTypeArme(), contrat.getQuantity());


        Debug.Log("ContratAccepter");
        aTerminer = true;
    }
}

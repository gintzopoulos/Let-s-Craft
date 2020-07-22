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

    public Canvas UIContrat;
    public Canvas BulleContrat;
    public TextMeshProUGUI QuantiteOr;
    public TextMeshProUGUI QuantiteArme;
    public Button Accept;
    public Button Reject;
    public Image army;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        contrat = new Contrat();
        animator.runtimeAnimatorController = RessourceManager.Instance.get_Animator(contrat.getArmy());
        SetUiContrat();
        Reject.onClick.AddListener(rejeter);
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
        Debug.Log("setBulleInactive");
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
        int Qty = contrat.getQuantity();
        RessourceManager.WeaponRessourceType typeArme = contrat.getTypeArme();

        QuantiteArme.text = contrat.getQuantity().ToString();
        QuantiteOr.text = (RessourceManager.Instance.get_Arme(typeArme).prix * Qty).ToString();

    }

    public void rejeter()
    {
        aTerminer = true;
    }
}

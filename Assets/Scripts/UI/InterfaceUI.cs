using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InterfaceUI : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    public Canvas myCanvas;
    public string myString;
    public Text myText;
    public float fadeTime;
    public bool displayInfo;

    public Button buttonBulleAchatServ;
    public SpriteRenderer bulleAchatServ;
    bool bulleAchatServestActive = true;

    public Button buttonBullePlusMoins;
    public SpriteRenderer bullePlusMoins;
    bool bulleAjoutestActive = true;

    public Button buttonBulleCraft;
    public SpriteRenderer bulleCraft;
    bool bulleCraftestActive = true;


    // Start is called before the first frame update
    void Start()
    {
        //myCanvas = GameObject.Find("CanvasMenu_Bois").GetComponent<Canvas>();
        //myText = GameObject.Find("TextCanvas").GetComponent<Text>();
        myText.color = Color.clear;
        Cursor.visible = true;

        buttonBulleCraft.onClick.AddListener(delegate { FadeBulle(bulleCraft); } );
        buttonBullePlusMoins.onClick.AddListener(delegate { FadeBulle(bullePlusMoins); } );
        buttonBulleAchatServ.onClick.AddListener(delegate { FadeBulle(bulleAchatServ); } );

    }

    // Update is called once per frame
    void Update()
    {
        FadeText();
        FadeCanvas();
         
    }

    private void OnMouseOver()
    {
       displayInfo = !RessourceManager.Instance.get_Is_UI_Craft_Active();
    }

    private void OnMouseExit()
    {
        displayInfo = false;

    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    displayInfo = true;
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    displayInfo = false;
    //}

    void FadeText()
    {
        if(displayInfo)
        {
            myText.text = myString;
            myText.color = Color.Lerp(myText.color, Color.white, fadeTime * Time.deltaTime);
        }

        else
        {
            myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }

    void FadeCanvas()
    {
        if (displayInfo)
        {
            myCanvas.gameObject.SetActive(true);
        }

        else
        {
            myCanvas.gameObject.SetActive(false);
        }

    }

    void FadeBulle(SpriteRenderer sr)
    {
        if (sr == bulleCraft)
        {
            if (bulleCraftestActive)
            {
                sr.gameObject.SetActive(false);
                bulleCraftestActive = false;
            }
            else
            {
                sr.gameObject.SetActive(true);
                bulleCraftestActive = true;

            }

        }
          
        if (sr == bulleAchatServ)
        {
            if (bulleAchatServestActive)
            {
                sr.gameObject.SetActive(false);
                bulleAchatServestActive = false;
            }
            else
            {
                sr.gameObject.SetActive(true);
                bulleAchatServestActive = true;

            }
        }

        if (sr == bullePlusMoins)
        {
            if (bulleAjoutestActive)
            {
                sr.gameObject.SetActive(false);
                bulleAjoutestActive = false;
            }
            else
            {
                sr.gameObject.SetActive(true);
                bulleAjoutestActive = true;

            }
        }

    }
}

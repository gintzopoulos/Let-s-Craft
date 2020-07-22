using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DisplayRessource : Interactable
{
    protected string nb;
    //public RessourceManager.MaterialRessourceType type;
    public Text myText;

    
    // Start is called before the first frame update
    void Start()
    {
       
        Cursor.visible = true;
    }

    // Update is called once per frame
    protected abstract void Update();

    protected void DisplayInfo()
    {
        myText.text = nb;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBourse : MonoBehaviour
{
    public Text text_bourse;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text_bourse.text = RessourceManager.Instance.get_bourse().ToString();
        
    }
}

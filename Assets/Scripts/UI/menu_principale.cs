using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu_principale : MonoBehaviour
{
    public Button a;
    public Button b;
    public Button c;

    // Start is called before the first frame update
    void Start()
    {
        a.onClick.AddListener(Play);
        b.onClick.AddListener(Quit);
        c.onClick.AddListener(Menu);
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

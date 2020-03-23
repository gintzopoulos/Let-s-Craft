using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour

{
    public Button btnQuit;
    public Button btnPlay;
    //public Canvas Chargement;

    // Start is called before the first frame update
    void Start()
    {
       // Chargement.gameObject.SetActive(false);
        btnPlay.onClick.AddListener(Play);
        btnQuit.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Play()
    {
        //Chargement.gameObject.SetActive(true);
        StartCoroutine("playCoroutine");

    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator playCoroutine()
    {
        SceneManager.LoadScene(1);
        yield return null;
    }
}

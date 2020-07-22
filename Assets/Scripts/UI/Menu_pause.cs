using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_pause : Singleton<Menu_pause>
{
    public bool gameIsPause = false;
    public GameObject CanvasPause;
    public Button btnPause;
    public Button btnMenu;
    public Button btnContinue;
    //public Canvas scene;

    void Start()
    {
        resume();
        btnPause.onClick.AddListener(updateState);
        btnMenu.onClick.AddListener(getBackMenu);
        btnContinue.onClick.AddListener(resume);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            updateState();
        }
    }

    void resume()
    {
        CanvasPause.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
        RessourceManager.Instance.set_UI_Craft_Active(false);
        
    }

    void pause()
    {
        CanvasPause.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
        RessourceManager.Instance.set_UI_Craft_Active(true);
        
    }

    void updateState()
    {
        
        if (gameIsPause == true)
        {
            resume();

        }
        else
        {
            pause();
        }
    }

    void getBackMenu()
    {
        //StartCoroutine("getBackMenu_Coroutine");
        SceneManager.LoadScene(0,LoadSceneMode.Single);
        //Destroy(this);
        ////Application.LoadLevel("Menu_Principale");
        //resume();
        ////SceneManager.UnloadSceneAsync(1);
    }

    IEnumerator getBackMenu_Coroutine()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        yield return load;
        resume();
        SceneManager.UnloadSceneAsync(1);
    }

}

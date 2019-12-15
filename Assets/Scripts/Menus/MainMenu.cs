using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.SceneManagement;
=======
>>>>>>> 62859b665c78ef6c2ba7f74430f53c43196c27f5

public class MainMenu : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject MainMenu_UI;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen.SetActive(true);
        MainMenu_UI.SetActive(false);
    }

    public void EnableTitle()
    {
        titleScreen.SetActive(true);
    }

    public void DisableTitle()
    {
        titleScreen.SetActive(false);
    }

    public void EnableMainMenuUI()
    {
        MainMenu_UI.SetActive(true);
    }

    public void DisableMainMenuUI()
    {
        MainMenu_UI.SetActive(false);
    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void onClickStart()
    {
        SceneManager.LoadScene("MainScenev2");
    }

    public void onClickExit()
    {
        Application.Quit();
    }
}

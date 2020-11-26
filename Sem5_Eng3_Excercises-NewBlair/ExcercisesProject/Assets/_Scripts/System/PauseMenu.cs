using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject oPauseMenu;
    [SerializeField]
    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if(isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }


    void ActivateMenu()
    {
        Time.timeScale = 0;
        oPauseMenu.SetActive(true);
    }

    void DeactivateMenu()
    {
        Time.timeScale = 1;
        oPauseMenu.SetActive(false);
    }

    void PauseGame()
    {
        isPaused = !isPaused;
    }

    void ExitGame()
    {
        Application.Quit();
    }
}

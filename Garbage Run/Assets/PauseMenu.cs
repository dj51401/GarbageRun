using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    void OnDisable(){  }
    void OnEnable(){  }
    void Awake(){  }

    public void GoBackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    public void CloseGame()
    {
        Application.Quit();

    }

}

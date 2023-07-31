using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManagement.Load("GameScene");
    }

    public void Credits()
    {
        SceneManagement.Load("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

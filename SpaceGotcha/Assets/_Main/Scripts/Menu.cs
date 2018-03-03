using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Play()
    {
        Fader.FadeTo(SceneEnum.Game, Color.black, 2);
        //SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

}

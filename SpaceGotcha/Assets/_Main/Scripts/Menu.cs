using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public void Play()
    {
        Fader.FadeTo(SceneEnum.Game, Color.black, 2);
    }

    public void Quit()
    {
        Application.Quit();
    }

}

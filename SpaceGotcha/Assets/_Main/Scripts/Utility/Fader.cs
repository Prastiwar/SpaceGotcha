using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneEnum
{
    Menu,
    Game
}
public class Fader : MonoBehaviour
{
    static bool isFading = false;
    Image layer;
    Color fadeCol;
    bool isFadeIn;
    float fadeSpeed;
    string fadeScene;
    WaitForSeconds update = new WaitForSeconds(0.05f);
    WaitForEndOfFrame waitForEnd = new WaitForEndOfFrame();

    public static void FadeTo(SceneEnum _scene, Color _col, float _fadeSpeed)
    {
        if (isFading)
            return;

        Time.timeScale = 1;
        isFading = true;

        GameObject init = new GameObject();
        init.name = "Fader";
        init.AddComponent<Fader>();
        Fader fader = init.GetComponent<Fader>();
        fader.fadeSpeed = _fadeSpeed;
        fader.fadeScene = _scene.ToString();
        fader.fadeCol = _col;
    }

    void Awake()
    {
        DontDestroyOnLoad(this);

        GameObject gm = new GameObject("FadeLayer");
        gm.transform.SetParent(this.transform);
        Image imageGM = gm.AddComponent<Image>();
        imageGM.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        fadeCol.a = 0;
        imageGM.color = fadeCol;
        Canvas canvasGM = gm.AddComponent<Canvas>();
        canvasGM.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGM.sortingOrder = 10;

        layer = imageGM;
    }

    void OnEnable()
    {
        // Start fading out
        isFadeIn = false;
        StartCoroutine(FadeCor());
    }

    void OnDestroy()
    {
        isFading = false;
    }

    IEnumerator FadeCor()
    {
        yield return waitForEnd; // make sure every variable is loaded correctly
        float fadeDamp = 0.1f;
        float minValue = -0.1f;
        float maxValue = 1.1f;
        float b;
        float condValue;

        fadeSpeed *= fadeDamp;
        if (isFadeIn)
        {
            b = minValue;
            condValue = 0;
            fadeCol.a = maxValue;
            fadeSpeed /= 2;
        }
        else
        {
            b = maxValue;
            condValue = 1;
            fadeCol.a = minValue;
        }
        
        while (isFadeIn ? (fadeCol.a > condValue) : (fadeCol.a < condValue))
        {
            fadeCol.a = Mathf.Lerp(fadeCol.a, b, fadeSpeed);
            if (fadeCol.a < 0.5f)
                fadeSpeed *= 2;
            layer.color = fadeCol;
            yield return update;
        }

        if (isFadeIn)
        {
            Destroy(gameObject);
        }
        else
        {
            SceneManager.LoadScene(fadeScene);
            yield return new WaitWhile(() => SceneManager.GetActiveScene().ToString() == fadeScene);
            StartCoroutine(FadeCor());
        }
        isFadeIn = !isFadeIn;
    }

}

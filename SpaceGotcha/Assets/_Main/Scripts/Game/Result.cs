using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject resultCanvas;
    [SerializeField] TextMeshProUGUI resultComment;
    [SerializeField] ResultData data;
    [SerializeField] Button playButton;

    public void GameOver(bool hasWin)
    {
        resultCanvas.SetActive(true);
        int randIndex = Random.Range(0, (hasWin) ? data.WinTexts.Length : data.LoseTexts.Length);
        string randText = (hasWin) ? data.WinTexts[randIndex] : data.LoseTexts[randIndex];
        TextMeshProUGUI playText = playButton.GetComponentInChildren<TextMeshProUGUI>();

        resultComment.text = randText;
        playText.text = (hasWin) ? "It was easy" : "Try again";
        playButton.onClick.AddListener(() => Play(hasWin));
    }

    void Play(bool hasWin)
    {
        if (hasWin)
        {
            Game.Instance.SettingsIndex++;

            if (Game.Instance.SettingsIndex >= Game.Instance.Settings.Count)
            {
                GameSettings newSettings = Instantiate(Game.Instance.Settings[Game.Instance.SettingsIndex - 1]);
                
                newSettings.FinalScore += newSettings.FinalScoreMultiplier;
                newSettings.PlayerSpeed += newSettings.PlayerSpeedMultiplier;
                newSettings.ObstacleSpeed += newSettings.ObstacleSpeedMultiplier;
                newSettings.StartCount += newSettings.StartCountMultiplier;
                newSettings.AddAmount += newSettings.AddAmountMultiplier;
                newSettings.TakeAmount -= newSettings.TakeAmountMultiplier;

                Game.Instance.Settings.Add(newSettings);
            }
        }
        SceneManager.LoadScene("Game");
    }

}

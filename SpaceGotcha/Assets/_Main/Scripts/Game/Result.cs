using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        playButton.onClick.AddListener(Play);

        if (hasWin)
        {
            Game.Instance.Data.SettingsIndex++;

            // If there is no more settings in list
            if (Game.Instance.Data.SettingsIndex >= Game.Instance.Data.Settings.Count)
            {
                CreateNewSettings();
            }
        }

        Game.Instance.Data.Save();
    }

    void CreateNewSettings()
    {
        GameSettings newSettings = Instantiate(Game.Instance.Data.Settings[Game.Instance.Data.SettingsIndex - 1]);

        newSettings.FinalScore += newSettings.FinalScoreMultiplier;
        newSettings.PlayerSpeed += newSettings.PlayerSpeedMultiplier;
        newSettings.ObstacleSpeed += newSettings.ObstacleSpeedMultiplier;
        newSettings.StartCount += newSettings.StartCountMultiplier;
        newSettings.AddAmount += newSettings.AddAmountMultiplier;
        newSettings.TakeAmount -= newSettings.TakeAmountMultiplier;
        newSettings.Save(Game.Instance.Data.SettingsIndex);

        Game.Instance.Data.Settings.Add(newSettings);
    }

    void Play()
    {
        Fader.FadeTo(SceneEnum.Game, Color.black, 1.5f);
    }

}

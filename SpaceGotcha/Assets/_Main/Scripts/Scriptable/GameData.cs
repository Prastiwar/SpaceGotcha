using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameData : ScriptableObject
{
    public int SettingsIndex;
    public List<GameSettings> Settings = new List<GameSettings>();

    public void Save()
    {
        Persistance.SaveObject("SettingsIndex", SettingsIndex);
    }

    public void Load()
    {
        SettingsIndex = (int)Persistance.LoadObject("SettingsIndex", 0);

        ListNullCheck();
        LoadAllSettings();
    }

    void ListNullCheck()
    {
        int length = Settings.Count;

        for (int i = 0; i < length; i++)
        {
            if (Settings[i] == null)
            {
                GameSettings settings = Settings[0];
                Settings.Clear();
                Settings.Add(settings);
                break;
            }
        }
    }

    void LoadAllSettings()
    {
        for (int i = 1; Settings.Count <= SettingsIndex; i++)
        {
            GameSettings newSettings = Instantiate(Settings[i - 1]);
            newSettings.Load(i, Settings[i - 1]);
            Settings.Add(newSettings);
        }
    }

}

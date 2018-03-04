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
        
        for (int i = 1; Settings.Count <= SettingsIndex || ( Settings.Count > i && Settings[i] == null); i++)
        {
            GameSettings newSettings = Instantiate(Settings[0]);
            newSettings.Load(i, Settings[0]);

            if (i < Settings.Count)
            {
                Settings[i] = newSettings;
            }
            else
            {
                Settings.Add(newSettings);
            }
        }
    }
}

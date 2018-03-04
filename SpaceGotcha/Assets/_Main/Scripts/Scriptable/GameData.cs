using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameData : ScriptableObject
{
    public int SettingsIndex;
    public List<GameSettings> Settings = new List<GameSettings>();
}

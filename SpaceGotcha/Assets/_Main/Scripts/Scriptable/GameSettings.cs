using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameSettings : ScriptableObject
{
    [Header("Player Settings")]
    public int PlayerSpeed;

    [Header("Game Settings")]
    public int FinalScore;

    [Header("Game Multipliers")]
    public int FinalScoreMultiplier;
    public int PlayerSpeedMultiplier;
    public float ObstacleSpeedMultiplier;
    public int StartCountMultiplier;
    public int AddAmountMultiplier;
    public int TakeAmountMultiplier;

    [Header("Obstacles Settings")]
    public GameObject ObstaclePrefab;
    public float ObstacleSpeed;
    public int Radius;
    public int HoleRadius;
    public int StartCount;
    public int AddAmount;
    public int TakeAmount;
    public GameObject ParticlesPrefab;
    public GameObject GameOverParticlesPrefab;

    public void Save(int i)
    {
        Persistance.SaveObject("PlayerSpeed" + i, PlayerSpeed);
        Persistance.SaveObject("FinalScore" + i, FinalScore);
        Persistance.SaveObject("ObstacleSpeed" + i, ObstacleSpeed);
        Persistance.SaveObject("StartCount" + i, StartCount);
        Persistance.SaveObject("AddAmount" + i, AddAmount);
        Persistance.SaveObject("TakeAmount" + i, TakeAmount);
    }

    public void Load(int i, GameSettings defaultSettings)
    {
        PlayerSpeed = (int)Persistance.LoadObject("PlayerSpeed" + i, defaultSettings.PlayerSpeed);
        FinalScore = (int)Persistance.LoadObject("FinalScore" + i, defaultSettings.FinalScore);
        ObstacleSpeed = (float)Persistance.LoadObject("ObstacleSpeed" + i, defaultSettings.ObstacleSpeed);
        StartCount = (int)Persistance.LoadObject("StartCount" + i, defaultSettings.StartCount);
        AddAmount = (int)Persistance.LoadObject("AddAmount" + i, defaultSettings.AddAmount);
        TakeAmount = (int)Persistance.LoadObject("TakeAmount" + i, defaultSettings.TakeAmount);
    }
}

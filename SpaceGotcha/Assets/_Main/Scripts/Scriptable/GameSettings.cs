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
}

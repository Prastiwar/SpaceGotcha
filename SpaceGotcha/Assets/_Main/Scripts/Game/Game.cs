using System.Collections;
using System.Collections.Generic;
using TMPro;
using TP.Utilities;
using Unity.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] TextMeshProUGUI ScoreText;
    public GameData Data;

    [HideInInspector] public GameObject Player;
    [HideInInspector] public GameObject PlayerBase;
    Transform playerBaseTrans;
    Result result;

    public NativeCustom<int> _score;
    public NativeCustom<int> Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    void Awake()
    {
        if (ReferenceEquals(Instance, null))
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance.gameObject.SetActive(true);
            Destroy(gameObject);
            return;
        }

        Time.timeScale = 1;
        Cache();
    }

    void Cache()
    {
        result = GetComponent<Result>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerBase = Player.transform.parent.gameObject;
        playerBaseTrans = PlayerBase.transform;
        Score = new NativeCustom<int>(1, Allocator.Persistent);
        ScoreText.text = Score.Value.ToString();

        TPObjectPool.AddToPool(Data.Settings[Data.SettingsIndex].ParticlesPrefab, 4);
    }
    
    void Update()
    {
        if (Time.timeScale < 1)
        {
            return;
        }
        
        if (_score.Value <= 0)
        {
            GameOver(false);
            _score.Value = 0;
        }
        else if (_score.Value >= Data.Settings[Data.SettingsIndex].FinalScore)
        {
            GameOver(true);
            _score.Value = Data.Settings[Data.SettingsIndex].FinalScore;
        }
        else if (ScoreText.text != _score.Value.ToString())
        {
            GameObject obj = TPObjectPool.GetFreeObjOf(Data.Settings[Data.SettingsIndex].ParticlesPrefab, true);
            TPObjectPool.ActiveObj(obj, playerBaseTrans.position);
            StartCoroutine(TPObjectPool.DeactiveObj(obj, 1));
        }
        ScoreText.text = _score.Value.ToString();
    }

    void GameOver(bool hasWin)
    {
        Time.timeScale = 0;
        GameObject particles = Instantiate(Data.Settings[Data.SettingsIndex].GameOverParticlesPrefab);
        ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.startColor = hasWin ? Color.green : Color.red;
        StartCoroutine(PauseParticles(particleSystem, hasWin));
        GetComponent<ObstacleManager>().GameOver();
    }

    IEnumerator PauseParticles(ParticleSystem particleSystem, bool hasWin)
    {
        yield return new WaitForSecondsRealtime(3);
        particleSystem.Pause();
        result.GameOver(hasWin);
    }

    void OnDisable()
    {
        Instance = null;
        Score.Dispose();
        TPObjectPool.Dispose();
    }

}

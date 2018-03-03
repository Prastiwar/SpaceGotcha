using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class ObstacleManager : MonoBehaviour
{
    Transform baseTrans;
    Transform playerTrans;
    GameSettings settings;

    NativeArray<Vector3> nativeWaypoints;
    TransformAccessArray transformAccessArray;

    JPosition jPosition;
    JobHandle jPositionHandle;

    void Awake()
    {
        Game game = GetComponent<Game>();
        settings = game.Settings[game.SettingsIndex];
        playerTrans = Game.Instance.Player.transform;
        baseTrans = playerTrans.parent;
        nativeWaypoints = new NativeArray<Vector3>(settings.StartCount, Allocator.Persistent);
        transformAccessArray = new TransformAccessArray(settings.StartCount);

        Spawn(settings.StartCount);
    }

    Vector2 DonutRandomVector2(Vector2 holePos, int holeRadius, int radius)
    {
        Vector2 pos = new Vector2();

        while (true)
        {
            pos = Random.insideUnitCircle * radius;

            if (Vector3.Distance(pos, holePos) > holeRadius)
            {
                return pos;
            }
        }
    }

    void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randVec = DonutRandomVector2(playerTrans.position, settings.HoleRadius, settings.Radius);
            nativeWaypoints[i] = randVec;

            GameObject clone = Instantiate(settings.ObstaclePrefab, randVec, Quaternion.identity);
            transformAccessArray.Add(clone.transform);
        }
    }

    void Update()
    {
        if (Time.timeScale < 1)
        {
            return;
        }
        if(Time.frameCount % 2 == 0)
        {
            jPosition = new JPosition()
            {
                target = baseTrans.position,
                speed = settings.ObstacleSpeed,
                randomVector = nativeWaypoints,
                points = Game.Instance.Score,
                player = playerTrans.position
            };
            jPositionHandle = jPosition.Schedule(transformAccessArray);
        }
    }

    void LateUpdate()
    {
        if (Time.timeScale < 1)
        {
            return;
        }
        if (Time.frameCount % 2 == 0)
        {
            jPositionHandle.Complete();
        }
    }

    void OnDisable()
    {
        nativeWaypoints.Dispose();
        transformAccessArray.Dispose();
    }

    public void GameOver()
    {
        jPositionHandle.Complete();
    }

    struct JPosition : IJobParallelForTransform
    {
        [ReadOnly]
        public Vector3 target;
        [ReadOnly]
        public Vector3 player;
        [ReadOnly]
        public NativeArray<Vector3> randomVector;
        [ReadOnly]
        public float speed;
        
        [WriteOnly]
        public NativeCustom<int> points;

        public void Execute(int index, TransformAccess transform)
        {
            float distancePlanet = Vector3.Distance(transform.position, target);
            float distancePlayer = Vector3.Distance(transform.position, player);

            if (distancePlayer < 0.85f)
            {
                transform.position = randomVector[GetRandomIndex()];
                points.Value += Game.Instance.Settings[Game.Instance.SettingsIndex].AddAmount;
            }
            else if (distancePlanet < 1)
            {
                transform.position = randomVector[GetRandomIndex()];
                points.Value -= Game.Instance.Settings[Game.Instance.SettingsIndex].TakeAmount;
                if (points.Value <= 0)
                {
                    points.Value = 0;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed);
            }
        }

        int GetRandomIndex()
        {
            System.Random random = new System.Random(System.DateTime.Now.Millisecond);
            return random.Next(0, randomVector.Length);
        }
    }
}

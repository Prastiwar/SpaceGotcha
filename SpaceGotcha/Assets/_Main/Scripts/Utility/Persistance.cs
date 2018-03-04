using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Persistance : MonoBehaviour
{
    public static Persistance Instance;
    public static Dictionary<string, object> PersistanceObjects = new Dictionary<string, object>();

    static string fileName = "SaveFile";
    static string fileExtention = "sgsav";

    void Awake()
    {
        if (ReferenceEquals(Instance, null))
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadGame();
    }

    static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, Path.ChangeExtension(fileName, fileExtention));
    }

    public static void SaveObject(string key, object value)
    {
        if (PersistanceObjects.ContainsKey(key))
        {
            PersistanceObjects[key] = value;
        }
        else
        {
            PersistanceObjects.Add(key, value);
        }
    }

    public static object LoadObject(string key, object defaultValue)
    {
        if (PersistanceObjects.ContainsKey(key))
        {
            return PersistanceObjects[key];
        }

        return defaultValue;
    }

    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetPath());
        bf.Serialize(file, PersistanceObjects);
        file.Close();
    }

    public static void LoadGame()
    {
        if (!File.Exists(GetPath()))
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(GetPath(), FileMode.Open);
        object serializedObject = bf.Deserialize(file) as object;
        Dictionary<string, object> objects = serializedObject as Dictionary<string, object>;
        PersistanceObjects = objects;
        file.Close();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

}

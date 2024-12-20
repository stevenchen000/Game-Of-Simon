﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityUtilities
{
    

    public static void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// Loads a scene onto the scene
    /// </summary>
    /// <param name="levelName"></param>
    public static void LoadLevelAdditive(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Loads a scene and applies an offset too all objects in it
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="offset"></param>
    public static void LoadSceneAdditive(string levelName, Vector3 offset)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        GameObject[] objs = scene.GetRootGameObjects();

        foreach(GameObject go in objs)
        {
            go.transform.position = go.transform.position + offset;
        }
    }

    /// <summary>
    /// Deletes a scene that has been loaded
    /// </summary>
    /// <param name="levelName"></param>
    public static void UnloadLevel(string levelName)
    {
        SceneManager.UnloadSceneAsync(levelName);
    }

    public static void RemoveChildren(Transform obj)
    {
        Transform child = GetFirstChild(obj);

        while (child != null)
        {
            child.transform.SetParent(null);
            child.gameObject.SetActive(false);
            child = GetFirstChild(obj);
        }
    }

    public static Transform GetFirstChild(Transform obj)
    {
        Transform child = null;
        try
        {
            child = obj.GetChild(0);
        }
        catch (Exception e)
        {

        }

        return child;
    }


    public static string ReadFile(string filename)
    {
        string result = "";

        if (File.Exists(filename))
        {
            FileStream file = File.OpenRead(filename);
            StreamReader reader = new StreamReader(file);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                result += $"{line}\n";
            }

            reader.Close();
            file.Close();
        }

        return result.Trim();
    }

    public static FileStream GetFileStream(string filename)
    {
        FileStream file = null;

        if (File.Exists(filename))
        {
            file = File.OpenWrite(filename);
        }

        return file;
    }

    public static T GetDeserializedObject<T>(string filename)
    {
        T result = default(T);

        BinaryFormatter bf = new BinaryFormatter();
        string saveLocation = GetSavePath(filename);


        if (File.Exists(saveLocation))
        {
            FileStream file = GetFileStream(saveLocation);
            result = (T)bf.Deserialize(file);

            file.Close();
        }

        return result;
    }

    public static void SerializeObject<T>(string filename, T obj)
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(filename))
        {
            string path = GetSavePath(filename);
            FileStream file = GetFileStream(path);

            bf.Serialize(file, obj);
            file.Close();
        }
    }



    public static string GetSavePath(string filename)
    {
        string path = Application.persistentDataPath;

        return $"{path}\\{filename}";
    }


    public static Vector3 GetVectorBetween(Vector3 from, Vector3 to){
        return to - from;
    }

    public static Vector2 GetVectorBetween(Vector2 from, Vector2 to)
    {
        return to - from;
    }
}

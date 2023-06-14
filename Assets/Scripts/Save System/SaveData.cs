using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private Dictionary<string, string> stringDict = new Dictionary<string, string>();
    private Dictionary<string, int> intDict = new Dictionary<string, int>();




    /****************
     * String values
     * *************/

    public void SetString(string name, string value)
    {
        stringDict[name] = value;
    }
    public bool HasString(string name) { return stringDict.ContainsKey(name); }
    public string GetString(string name) { return stringDict[name]; }
    public void DeleteString(string name)
    {
        stringDict.Remove(name);
    }

    public Dictionary<string, string> GetStringDict() { return stringDict; }


    /**************
     * Int values
     * ***********/

    public void SetInt(string name, int value)
    {
        intDict[name] = value;
    }
    public bool HasInt(string name) { return intDict.ContainsKey(name); }
    public int GetInt(string name) { return intDict[name]; }
    /// <summary>
    /// Increments the int by amount
    /// Accepts negative numbers
    /// If value does not exist, will set to amount
    /// </summary>
    /// <param name="name"></param>
    /// <param name="amount"></param>
    public void IncrementInt(string name, int amount)
    {
        if (HasInt(name))
        {
            int value = GetInt(name) + amount;
            SetInt(name, value);
        }
        else
        {
            SetInt(name, amount);
        }
    }
    public void DeleteInt(string name)
    {
        intDict.Remove(name);
    }


    public Dictionary<string, int> GetIntDict() { return intDict; }



    /******************
     * Load Values
     * ****************/

    public void LoadValues(Dictionary<string, string> strings, Dictionary<string, int> integers)
    {
        stringDict = strings;
        intDict = integers;
    }

}

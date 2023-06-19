using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameVarString
{
    private SaveData data;
    private string varName;


    public GameVarString(SaveData data, string varName) {
        this.data = data;
        this.varName = varName;
    }

    public bool Exists()
    {
        return data.HasString(varName);
    }

    public void SetValue(string value)
    {
        data.SetString(varName, value);
    }

    public string GetValue()
    {
        string result = null;
        if (Exists())
        {
            result = data.GetString(varName);
        }

        return result;
    }


}
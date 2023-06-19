using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameVarInt
{
    private SaveData data;
    private string varName;


    public GameVarInt(SaveData data, string varName) {
        this.data = data;
        this.varName = varName;
    }

    public bool Exists()
    {
        return data.HasInt(varName);
    }

    public void SetValue(int value)
    {
        data.SetInt(varName, value);
    }

    public int GetValue()
    {
        int result = 0;
        if (Exists())
        {
            result = data.GetInt(varName);
        }

        return result;
    }


}
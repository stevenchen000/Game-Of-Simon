using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;


public class GameStateManager : MonoBehaviour
{
	private static GameStateManager instance;
	[SerializeField] private LoadScreen loadScreen;

	private GameVarInt totalCoins;


	private const string filename = "data.sav";
	private SaveData saveData;


	/******************
	 * Unity
	 * Functions
	 * *****************/

	void Start()
	{
		if(instance == null)
        {
			instance = this;
			DontDestroyOnLoad(this);
			LoadData();
			totalCoins = new GameVarInt(saveData, VarNames.coins);
        }
        else
        {
			Destroy(this);
        }
	}

	void Update()
	{
		
	}

    public void OnApplicationQuit()
    {
		SaveData();
    }

    private void OnApplicationPause(bool pause)
    {
        if(Application.platform == RuntimePlatform.Android ||
			Application.platform == RuntimePlatform.IPhonePlayer)
        {
			SaveData();
        }
    }


    /****************
	 * Variable
	 * Functions
	 * **************/

    public static string GetSimonName() { return GetString(VarNames.simonName); }
	public static void SetSimonName(string newName) 
	{
		SetString(VarNames.simonName, newName);
	}


	public static int GetCoins() { return GetInt(VarNames.coins); }
	public static bool HasEnoughCoins(int amount) { return GetInt(VarNames.coins) >= amount; }
	/// <summary>
	/// Attempts to spend coins
	/// Returns false if not enough coins
	/// </summary>
	/// <param name="amount"></param>
	/// <returns></returns>
	public static bool SpendCoins(int amount) 
	{
		bool coinsSpent = false;

        if (HasEnoughCoins(amount))
        {
			IncrementInt(VarNames.coins, -amount);
			coinsSpent = true;
        }

		return coinsSpent;
	}

	public static void AddCoins(int amount)
    {
		IncrementInt(VarNames.coins, amount);
    }



	public static void SetString(string name, string value)
    {
		instance.saveData.SetString(name, value);
    }
	public static bool HasString(string name)
    {
		return instance.saveData.HasString(name);
    }
	public static string GetString(string name)
    {
		return instance.saveData.GetString(name);
    }

	public static void ChangeInt(string name, int value)
    {
		instance.saveData.SetInt(name, value);
    }
	public static bool HasInt(string name)
    {
		return instance.saveData.HasInt(name);
    }
	public static int GetInt(string name)
    {
		return instance.saveData.GetInt(name);
    }
	public static void IncrementInt(string name, int amount)
    {
		instance.saveData.IncrementInt(name, amount);
    }




	//Save and Load


	public void LoadData()
    {
		saveData = UnityUtilities.GetDeserializedObject<SaveData>(filename);
		if(saveData == null)
        {
			saveData = new SaveData();
			Debug.Log("No save data found, reset data");
        }
		Debug.Log(saveData.GetAllDataAsText());
    }

	public void SaveData()
    {
		UnityUtilities.SerializeObject(filename, saveData);
		Debug.Log("Game saved");
		Debug.Log(saveData.GetAllDataAsText());
    }



	/**************
	 * Load Screen
	 * *************/

	public static void LoadOut(string levelName)
    {
		instance.loadScreen.LoadOut(levelName);
    }

}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;


public class GameData
{
	public string playerName;
	public int coins;

}

public class GameStateManager : MonoBehaviour
{
	private static GameStateManager instance;
	[SerializeField] private LoadScreen loadScreen;

	[SerializeField] private string simonName = "Simon";
	[SerializeField] private List<string> previousNames = new List<string>();
	[SerializeField] private int coins;

	private const string filename = "player.sim";


	void Start()
	{
		if(instance == null)
        {
			instance = this;
			DontDestroyOnLoad(this);
        }
        else
        {
			Destroy(this);
        }
	}

	void Update()
	{
		
	}


	public static string GetSimonName() { return instance.simonName; }
	public static void SetSimonName(string newName) 
	{
		instance.previousNames.Add(GetSimonName());
		instance.simonName = newName;
	}
	public static int GetNumberOfOldNames() { return instance.previousNames.Count; }
	public static string GetOldName(int index) { return instance.previousNames[index]; }


	public static int GetCoins() { return instance.coins; }
	public static bool HasEnoughCoins(int amount) { return instance.coins >= amount; }
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
			instance.coins -= amount;
			coinsSpent = true;
        }

		return coinsSpent;
	}

	public static void AddCoins(int amount)
    {
		instance.coins += amount;
    }




	//Save and Load


	public void LoadData()
    {
		GameData data = UnityUtilities.GetDeserializedObject<GameData>(filename);
		UnloadGameData(data);
    }

	public void SaveData()
    {
		GameData data = SaveToGameData();
		UnityUtilities.SerializeObject(filename, data);
    }


	private void UnloadGameData(GameData data)
    {

    }

	private GameData SaveToGameData()
    {
		GameData data = new GameData();

		return data;
    }

	/**************
	 * Load Screen
	 * *************/

	public static void LoadOut(string levelName)
    {
		instance.loadScreen.LoadOut(levelName);
    }

}

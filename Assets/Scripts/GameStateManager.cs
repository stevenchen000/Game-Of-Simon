using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;


public class GameStateManager : MonoBehaviour
{
	private static GameStateManager gameState;

	[SerializeField] private string simonName = "Simon";
	[SerializeField] private List<string> previousNames = new List<string>();
	[SerializeField] private int coins;



	void Start()
	{
		if(gameState == null)
        {
			gameState = this;
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


	public static string GetSimonName() { return gameState.simonName; }
	public static void SetSimonName(string newName) 
	{
		gameState.previousNames.Add(GetSimonName());
		gameState.simonName = newName;
	}
	public static int GetNumberOfOldNames() { return gameState.previousNames.Count; }
	public static string GetOldName(int index) { return gameState.previousNames[index]; }


	public static int GetCoins() { return gameState.coins; }
	public static bool HasEnoughCoins(int amount) { return gameState.coins >= amount; }
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
			gameState.coins -= amount;
			coinsSpent = true;
        }

		return coinsSpent;
	}
}

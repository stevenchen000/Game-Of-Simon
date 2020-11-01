using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSystem{
	public class SimonActionManager : MonoBehaviour
	{
		[SerializeField] private List<SimonAction> actions = new List<SimonAction>();

		public int GetNumberOfActions() { return actions.Count; }
		public SimonAction GetActionAtIndex(int index) 
		{
			SimonAction action = null;
			if (actions.Count > index) action = actions[index];
			return action;
		}
		public SimonAction GetRandomAction()
        {
			int rand = Random.Range(0, GetNumberOfActions());
			return actions[rand];
        }
	}
}
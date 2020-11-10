using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSystem{
	public class SimonActionSelector : MonoBehaviour
	{
		private SimonActionManager manager;
		private List<SimonAction> allActions = new List<SimonAction>();

        private void Start()
        {
			manager = FindObjectOfType<SimonActionManager>();
        }


        public void AddAction()
		{
			SimonAction action = manager.GetRandomAction();
			allActions.Add(action);
		}

		public void ResetActions()
        {
			allActions = new List<SimonAction>();
        }

		//Getters & Setters

		public int GetNumberOfActions() { return allActions.Count; }
		public SimonAction GetActionAtIndex(int index) { return allActions[index]; }
		public List<SimonAction> GetAllActions() { return allActions; }
	}
}
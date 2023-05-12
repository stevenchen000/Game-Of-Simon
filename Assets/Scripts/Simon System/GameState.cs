using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSystem
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] private int score = 0;
        [SerializeField] private float timeRemaining = 0;
        [SerializeField] private List<SimonAction> actions = new List<SimonAction>();
        [SerializeField] private int currActionIndex = 0;
        [SerializeField] private int mistakes = 0;
        [SerializeField] private SimonState gameState = SimonState.GameOver;

        [Space]
        [SerializeField] private VarEventSO<int> onScoreChange;
        [SerializeField] private VarEventSO<float> onTimeChange;
        [SerializeField] private VarEventSO<float> onTimeDifference;

        //Score Functions
        public int GetScore() { return score; }
        public void AddScore(int amount) { 
            score += amount;
            onScoreChange.CallEvent(score); 
        }
        public void RemoveScore(int amount) { 
            score -= amount;
            onScoreChange.CallEvent(score);
        }
        public void ResetScore() { score = 0; }

        //Time Functions
        public float GetTimeRemaining() { return timeRemaining; }
        public void AddTime(float amount) { 
            timeRemaining += amount;
            onTimeChange.CallEvent(timeRemaining);
            onTimeDifference.CallEvent(timeRemaining);
        }
        /// <summary>
        /// Remove time and calls onTimeChange and onTimeDifference
        /// </summary>
        /// <param name="amount"></param>
        public void RemoveTime(float amount) {
            timeRemaining = Mathf.Max(0, timeRemaining - amount);
            onTimeChange.CallEvent(timeRemaining);
            onTimeDifference.CallEvent(-amount);
        }
        /// <summary>
        /// Time counts down
        /// Calls OnTimeChange, but does not call OnTimeDifference
        /// </summary>
        public void TimeTick()
        {
            timeRemaining = Mathf.Max(0, timeRemaining - Time.deltaTime);
            onTimeChange.CallEvent(timeRemaining);
        }
        public void SetTime(float newTime) { timeRemaining = newTime;
            onTimeChange?.CallEvent(timeRemaining);
        }

        //Mistakes
        public int GetNumberOfMistakes() { return mistakes;  }
        public void AddMistake() { mistakes++; }
        public void ResetMistakes() { mistakes = 0; }

        //Actions
        public SimonAction GetAction(int index)
        {
            SimonAction result = null;

            if (index >= 0 && index < actions.Count)
            {
                result = actions[index];
            }

            return result;
        }
        /// <summary>
        /// Returns a copy of the list of actions
        /// </summary>
        /// <returns></returns>
        public List<SimonAction> GetActions()
        {
            var result = new List<SimonAction>();
            result.AddRange(actions);
            return result;
        }
        public int GetNumberOfActions() { return actions.Count; }
        public bool CompareAction(SimonAction action)
        {
            var compAction = GetAction(currActionIndex);
            return compAction == action;
        }

        public void AddAction(SimonAction action)
        {
            actions.Add(action);
        }

        public void SetActions(List<SimonAction> newActions)
        {
            actions = newActions;
        }

        public void ResetActions()
        {
            actions = new List<SimonAction>();
        }

        public int GetActionIndex() { return currActionIndex; }
        public void IncrementActionIndex() { currActionIndex++; }
        public void ResetActionIndex() { currActionIndex = 0; }
        public bool ActionsDone() { return currActionIndex >= actions.Count; }


        public SimonState GetGameState() { return gameState; }
        public void SetGameState(SimonState newState) { gameState = newState; }
    }
}
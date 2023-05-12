using SOEventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSystem
{
	public class SimonTimer : MonoBehaviour
	{
		public float maxWaitTime { get { return _maxWaitTime; } set { _maxWaitTime = value; } }
		[SerializeField] private float _maxWaitTime = 5;

		public float bonusTimePerAction { get { return _bonusTimePerAction; } set { _bonusTimePerAction = value; } }
		[Space(20)] 
		[SerializeField] private float _bonusTimePerAction = 0.2f;
		public float bonusTimePerRound { get { return _bonusTimePerRound; } set { _bonusTimePerRound = value; } }
		[SerializeField] private float _bonusTimePerRound = 0.5f;
		public float penaltyTime { get { return _penaltyTime; } set { _penaltyTime = value; } }
		[SerializeField] private float _penaltyTime = 1;

		[Space]
		[SerializeField] private VarEventSO<float> onBonusTime;
		[SerializeField] private VarEventSO<float> onPenaltyTime;


		private GameState state;

		void Start()
		{
			state = FindObjectOfType<GameState>();
			ResetRemainingTime();
		}

		void Update()
		{
			if (IsRunning())
			{
				state.TimeTick();
				if (GetRemainingTime() <= 0)
				{
					SimonManager.GameOver();
				}
			}

		}



		public void AddBonusTime()
		{
			state.AddTime(bonusTimePerAction);
			onBonusTime?.CallEvent(bonusTimePerAction);
		}

		public void AddRoundBonusTime()
		{
			state.AddTime(bonusTimePerRound);
			onBonusTime?.CallEvent(bonusTimePerRound);
		}

		public void ApplyPenaltyTime()
		{
			state.RemoveTime(penaltyTime);
			onPenaltyTime?.CallEvent(penaltyTime);
		}

		private void ResetRemainingTime()
        {
			state.SetTime(maxWaitTime);
        }


		public float GetRemainingTime() { return state.GetTimeRemaining(); }
		public float GetMaxTime() { return maxWaitTime; }

		/***************
		 * Helpers
		 * **************/

		private bool IsRunning()
        {
			return state.GetGameState() == SimonState.PlayerTurn;
        }

	}
}
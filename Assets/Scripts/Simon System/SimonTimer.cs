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
		public float timeRemaining { get { return _timeRemaining; } set { _timeRemaining = Mathf.Max(value, 0); } }
		[SerializeField] private float _timeRemaining;

		
		public float bonusTimePerAction { get { return _bonusTimePerAction; } set { _bonusTimePerAction = value; } }
		[Space(20)] 
		[SerializeField] private float _bonusTimePerAction = 0.2f;
		public float bonusTimePerRound { get { return _bonusTimePerRound; } set { _bonusTimePerRound = value; } }
		[SerializeField] private float _bonusTimePerRound = 0.5f;
		public float penaltyTime { get { return _penaltyTime; } set { _penaltyTime = value; } }
		[SerializeField] private float _penaltyTime = 1;

		[Space(20)]
		[SerializeField] private EventSO onTimerStart;
		[SerializeField] private EventSO onTimerStop;
		[SerializeField] private EventSO onGameOver;

		private bool isRunning = false;

		void Start()
		{
			timeRemaining = maxWaitTime;
		}

		void Update()
		{
			if (isRunning)
			{
				timeRemaining -= Time.deltaTime;
				if (timeRemaining <= 0)
				{
					onGameOver?.CallEvent();
					isRunning = false;
				}
			}

		}



		public void AddBonusTime()
		{
			timeRemaining += bonusTimePerAction;
		}

		public void CalculateNewMaxTime()
		{
			maxWaitTime = timeRemaining + bonusTimePerRound;
		}

		public void RemovePenaltyTime()
		{
			timeRemaining -= penaltyTime;
		}

		public void StopTimer()
		{
			onTimerStop?.CallEvent();
			isRunning = false;
			CalculateNewMaxTime();
			ResetRemainingTime();
		}

		public void StartTimer()
		{
			onTimerStart?.CallEvent();
			isRunning = true;
		}


		private void ResetRemainingTime()
        {
			timeRemaining = maxWaitTime;
        }
	}
}
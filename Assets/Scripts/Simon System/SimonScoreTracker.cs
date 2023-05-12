using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSystem{
	public class SimonScoreTracker : MonoBehaviour
	{
		[SerializeField] private float scorePerAction = 100;
		[SerializeField] private float comboMultiplier = 0.5f;
		public int comboCount { get; set; }
		public int totalScore { get; set; }
		[SerializeField] private IntEventSO onScoreChanged;
		[SerializeField] private IntEventSO onAddScore;
		[SerializeField] private IntEventSO onPenaltyScore;

		void Start()
		{
			
		}

		void Update()
		{
			
		}

		public void AddToScore()
        {
			int addedScore = CalculateScoreEarned();
			totalScore += addedScore;
			onAddScore?.CallEvent(addedScore);
			onScoreChanged?.CallEvent(totalScore);
			comboCount++;
        }

		public void ResetCombo()
        {
			comboCount = 0;
        }

		private int CalculateScoreEarned()
        {
			float scoreEarned = scorePerAction;

			if (comboCount > 1)
			{
				float multiplier = 1 + comboMultiplier * (comboCount - 1);
				scoreEarned *= multiplier;
			}

			return (int)scoreEarned;
        }



	}
}
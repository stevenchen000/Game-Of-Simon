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
		public float totalScore { get; set; }
		[SerializeField] private FloatEventSO onScoreChanged;

		void Start()
		{
			
		}

		void Update()
		{
			
		}

		public void AddToScore()
        {
			totalScore += CalculateScoreEarned();
			onScoreChanged?.CallEvent(totalScore);
			comboCount++;
        }

		public void ResetCombo()
        {
			comboCount = 0;
        }

		private float CalculateScoreEarned()
        {
			float scoreEarned = scorePerAction;

			if (comboCount > 1)
			{
				float multiplier = 1 + comboMultiplier * (comboCount - 1);
				scoreEarned *= multiplier;
			}

			return scoreEarned;
        }



	}
}
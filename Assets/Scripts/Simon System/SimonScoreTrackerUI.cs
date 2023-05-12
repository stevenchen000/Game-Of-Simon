using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SimonSystem{
	public class SimonScoreTrackerUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text textbox;

		void Start()
		{
			if (textbox == null) textbox = transform.GetComponentInChildren<TMP_Text>();
		}

		void Update()
		{
			
		}

		public void UpdateScoreText(int newScore)
        {
			textbox.text = $"Score: {newScore}";
        }
	}
}
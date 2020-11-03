using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SimonSystem{
	public class SimonScoreTrackerUI : MonoBehaviour
	{
		[SerializeField] private SimonScoreTracker tracker;
		[SerializeField] private TMP_Text textbox;

		void Start()
		{
			if(tracker == null) tracker = FindObjectOfType<SimonScoreTracker>();
			if (textbox == null) textbox = transform.GetComponentInChildren<TMP_Text>();
		}

		void Update()
		{
			
		}

		public void UpdateScoreText(float newScore)
        {
			textbox.text = $"Score: {newScore}";
        }
	}
}
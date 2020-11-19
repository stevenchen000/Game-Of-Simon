using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeatManager : MonoBehaviour
{
	[SerializeField] private float beatsPerMinute = 120;
	[SerializeField] private AudioClip beatSound;
	[Range(0,2)]
	[SerializeField] private float beatMultiplier = 1f;
	private bool isRunning = false;
	private AudioSource audio;

	private float timer = 0;
	private float period;

	void Start()
	{
		audio = transform.GetComponent<AudioSource>();
		period = 60 / beatsPerMinute;
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (isRunning) RunBeat();
	}


	public void StartBeat()
    {
		isRunning = true;
    }



	private void RunBeat()
    {
		
		if (timer > period)
		{
			audio.PlayOneShot(beatSound, beatMultiplier);
			timer -= period;
		}
    }
}

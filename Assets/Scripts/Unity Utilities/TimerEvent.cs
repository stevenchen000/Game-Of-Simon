using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerEvent : MonoBehaviour
{
	[SerializeField] private float startTime;
	[SerializeField] private UnityEvent timerEvent;
	[SerializeField] private bool startOnAwake;
	[SerializeField] private Timer timer;


    void Start()
	{
		if (startOnAwake) StartTimer();
	}

	void Update()
	{
		if(timer != null)
        {
			timer.Tick();
			if (timer.AtTime(startTime))
			{
				timerEvent?.Invoke();
				timer = null;
			}
        }
	}

	public void StartTimer()
    {
		timer = new Timer();
    }


}

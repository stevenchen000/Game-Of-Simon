using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonTimerUI : MonoBehaviour
{
    private Slider slider;
    private CanvasGroup cgroup;
    private SimonManager simon;
    private bool enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        cgroup = transform.GetComponent<CanvasGroup>();
        slider = transform.GetComponentInChildren<Slider>();
        simon = FindObjectOfType<SimonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            UpdateValue();
        }
    }

    private void UpdateValue()
    {
        float maxTime = simon.GetMaxWaitTime();
        float currTime = simon.GetCurrentTime();

        float percentage = (maxTime - currTime) / maxTime;
        slider.value = percentage;
    }


    public void EnableUI()
    {
        cgroup.alpha = 1;
        enabled = true;
    }

    public void DisableUI()
    {
        cgroup.alpha = 0;
        enabled = false;
    }
}

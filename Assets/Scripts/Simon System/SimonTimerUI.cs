using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace SimonSystem
{
    public class SimonTimerUI : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text textbox;
        private CanvasGroup cgroup;
        private SimonTimer timer;
        private bool uiEnabled = false;

        private float percentage = 1;

        [SerializeField] private float fadeInTime = 1;
        [SerializeField] private float fadeOutTime = 1;

        // Start is called before the first frame update
        void Start()
        {
            cgroup = transform.GetComponent<CanvasGroup>();
            timer = FindObjectOfType<SimonTimer>();
            //cgroup.alpha = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (timer.GetMaxTime() > 0)
            {
                UpdateSlider();
                UpdateText();
            }
        }

        private void UpdateSlider()
        {
            float maxTime = timer.GetMaxTime();
            float currTime = timer.GetRemainingTime();

            float tempPercentage = currTime / maxTime;
            tempPercentage = Mathf.Max(0, tempPercentage);

            slider.value = tempPercentage;
        }

        private void UpdateText()
        {
            float currTime = timer.GetRemainingTime();

            textbox.text = $"{currTime.ToString("0.00")}s";
        }

        private void FadeTimer(float fadeValue, float fadeTime)
        {
            var tweener = DOTween.To(() => cgroup.alpha, x => cgroup.alpha = x, fadeValue, fadeTime);
        }


        public void EnableUI()
        {
            uiEnabled = true;
            percentage = 1;

            //FadeTimer(1, fadeInTime);
        }

        public void DisableUI()
        {
            uiEnabled = false;

            //FadeTimer(0, fadeOutTime);
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace SimonSystem
{
    public class SimonButtonUI : MonoBehaviour
    {
        private CanvasGroup cgroup;
        private List<SimonButton> buttons = new List<SimonButton>();
        private SimonActionManager actionManager;
        private SimonManager simon;

        // Use this for initialization
        void Start()
        {
            cgroup = transform.GetComponent<CanvasGroup>();
            actionManager = FindObjectOfType<SimonActionManager>();
            simon = FindObjectOfType<SimonManager>();
            SetupButtons();
        }

        // Update is called once per frame
        void Update()
        {

        }




        private void SetupButtons()
        {
            var newButtons = transform.GetComponentsInChildren<SimonButton>();
            buttons = new List<SimonButton>();
            buttons.AddRange(newButtons);
            

            for (int i = 0; i < buttons.Count; i++)
            {
                SimonAction action = actionManager.GetActionAtIndex(i);
                var button = buttons[i];

                SetupButton(button, action);
            }
        }

        private void SetupButton(SimonButton button, SimonAction action)
        {
            button.UpdateButton(action);
        }

        public void DisableUI()
        {
            cgroup.interactable = false;
        }

        public void EnableUI()
        {
            cgroup.interactable = true;
        }
    }
}
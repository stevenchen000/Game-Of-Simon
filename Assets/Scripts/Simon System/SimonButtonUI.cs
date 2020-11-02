﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace SimonSystem
{
    public class SimonButtonUI : MonoBehaviour
    {
        private CanvasGroup cgroup;
        private List<Button> buttons = new List<Button>();
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
            var newButtons = transform.GetComponentsInChildren<Button>();
            buttons.AddRange(newButtons);

            for (int i = 0; i < buttons.Count; i++)
            {
                SimonAction action = actionManager.GetActionAtIndex(i);
                Button button = buttons[i];

                SetupButton(button, action);
            }
        }

        private void SetupButton(Button button, SimonAction action)
        {
            Sprite newSprite = action.GetSprite();
            Transform imageTrans = button.transform.GetChild(0);
            Image buttonImage = imageTrans.GetComponent<Image>();

            buttonImage.sprite = newSprite;
            button.onClick.AddListener(() => simon.SelectAction(action));
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimonSystem;
using SOEventSystem;

public class SimonButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;
    [SerializeField] private SimonAction action;
    [SerializeField] private SimonActionEventSO simonEvent;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ButtonAction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateButton(SimonAction action)
    {
        this.action = action;
        string name = action.GetActionName();
        var sprite = action.GetSprite();
        icon.sprite = sprite;
        text.text = name;
    }

    private void ButtonAction()
    {
        simonEvent.CallEvent(action);
    }
}

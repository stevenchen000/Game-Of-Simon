using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimonState
{
    PlayingActions,
    AwaitingPlayer,
    GameOver
}

public class SimonManager : MonoBehaviour
{
    public Character player;
    public List<SimonAction> actions = new List<SimonAction>();

    private SimonState state = SimonState.PlayingActions;

    private List<SimonAction> allActions = new List<SimonAction>();
    private int actionIndex = 0;

    [SerializeField]
    private float timeBetweenActions = 0.3f;
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private float maxWaitTime = 5f;

    [SerializeField]
    private EventSO onAwaitTurn;
    [SerializeField]
    private EventSO onPlayerStart;
    [SerializeField]
    private EventSO onGameOver;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();
        ChangeState(SimonState.PlayingActions);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick();
        RunState();
    }

    //Important Public Functions

    public void SelectAction(SimonAction action)
    {
        if (state == SimonState.AwaitingPlayer)
        {
            bool correct = CompareAction(action);

            if (correct)
            {
                action.RunAction(player);
                actionIndex++;
                if(actionIndex < allActions.Count)
                {
                    timer = new Timer();
                }
                else
                {
                    ChangeState(SimonState.PlayingActions);
                }
            }
            else
            {
                ChangeState(SimonState.GameOver);
            }
        }
    }








    //State Functions


    private void RunState()
    {
        switch (state)
        {
            case SimonState.PlayingActions:
                PlayActions();
                break;
            case SimonState.AwaitingPlayer:
                if (timer.AtTime(maxWaitTime)) ChangeState(SimonState.GameOver);
                break;
            case SimonState.GameOver:
                break;
        }
    }

    private void PlayActions()
    {
        if (timer.AtTime(timeBetweenActions))
        {
            if (actionIndex < allActions.Count)
            {
                allActions[actionIndex].RunAction(player);
                actionIndex++;
            }
            else
            {
                ChangeState(SimonState.AwaitingPlayer);
            }

            timer = new Timer();
        }
    }


    private void ChangeState(SimonState newState)
    {
        switch (newState)
        {
            case SimonState.PlayingActions:
                allActions.Add(GetRandomAction());
                actionIndex = 0;
                onAwaitTurn?.CallEvent();
                break;
            case SimonState.AwaitingPlayer:
                actionIndex = 0;
                onPlayerStart?.CallEvent();
                break;
            case SimonState.GameOver:
                onGameOver?.CallEvent();
                break;
        }
        state = newState;
        timer = new Timer();
    }





    //Helper Functions

    private SimonAction GetRandomAction()
    {
        int rand = Random.Range(0, actions.Count);
        return actions[rand];
    }

    private bool CompareAction(SimonAction action)
    {
        return allActions[actionIndex] == action;
    }




    ///Getters and Setters

    public SimonAction GetAction(int index) { return actions[index]; }

    public float GetMaxWaitTime() { return maxWaitTime; }
    public float GetCurrentTime() { return timer.GetCurrentTime(); }
}

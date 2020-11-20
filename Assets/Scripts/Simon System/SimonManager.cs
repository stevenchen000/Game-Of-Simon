using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimonState
{
    PlayingSequence,
    AwaitingPlayer,
    AwaitingFinalAnimation,
    VictoryAnimation,
    GameOver
}

namespace SimonSystem
{
    public class SimonManager : MonoBehaviour
    {
        private Character player;
        private SimonActionSelector selector;
        private SimonSequencePlayer sequence;
        private SimonState state = SimonState.PlayingSequence;

        private int actionIndex = 0;

        [SerializeField] private EventSO onPlayerTurnStart;
        [SerializeField] private EventSO onPlayerTurnEnd;
        [SerializeField] private EventSO onCorrectAction;
        [SerializeField] private EventSO onWrongAction;

        [SerializeField] private VarEventSO<SimonAction> onSimonAction;

        private bool isActive = false;

        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Character>();
            selector = GetComponentInChildren<SimonActionSelector>();
            sequence = GetComponentInChildren<SimonSequencePlayer>();
        }

        // Update is called once per frame
        void Update()
        {
            if(state == SimonState.AwaitingFinalAnimation)
            {
                if (player.IsIdle()) ChangeState(SimonState.PlayingSequence);
            }
        }

        //Important Public Functions

        public void StartGame()
        {
            if (!isActive)
            {
                isActive = true;
                selector.ResetActions();
                ChangeState(SimonState.PlayingSequence);
            }
        }

        public void SelectAction(SimonAction action)
        {
            if (isActive && state == SimonState.AwaitingPlayer)
            {
                bool correct = CompareAction(action);

                if (correct)
                {
                    //action.RunAction(player);
                    actionIndex++;
                    onCorrectAction?.CallEvent();
                    onSimonAction?.CallEvent(action);

                    if (actionIndex == selector.GetNumberOfActions())
                    {
                        ChangeState(SimonState.AwaitingFinalAnimation);
                    }
                }
                else
                {
                    onWrongAction?.CallEvent();
                }
            }
        }


        public void WaitForPlayer()
        {
            if (isActive) ChangeState(SimonState.AwaitingPlayer);
        }

        public void GameOver()
        {
            ChangeState(SimonState.GameOver);
        }




        //State Functions




        private void ChangeState(SimonState newState)
        {
            if (state == SimonState.AwaitingPlayer) onPlayerTurnEnd?.CallEvent();

            switch (newState)
            {
                case SimonState.PlayingSequence:
                    selector.AddAction();
                    sequence.Play();
                    break;
                case SimonState.AwaitingPlayer:
                    actionIndex = 0;
                    onPlayerTurnStart?.CallEvent();
                    break;
                case SimonState.AwaitingFinalAnimation:

                    break;
                case SimonState.VictoryAnimation:
                    
                    break;
                case SimonState.GameOver:
                    isActive = false;
                    break;
            }
            state = newState;
        }





        //Helper Functions

        private bool CompareAction(SimonAction action)
        {
            return selector.GetActionAtIndex(actionIndex) == action;
        }



    }
}
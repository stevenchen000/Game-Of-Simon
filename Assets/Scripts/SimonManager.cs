using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimonState
{
    PlayingSequence,
    AwaitingPlayer,
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



        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Character>();
            selector = FindObjectOfType<SimonActionSelector>();
            sequence = FindObjectOfType<SimonSequencePlayer>();
            ChangeState(SimonState.PlayingSequence);
        }

        // Update is called once per frame
        void Update()
        {
            //RunState();
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
                    onCorrectAction?.CallEvent();

                    if (actionIndex == selector.GetNumberOfActions())
                    {
                        ChangeState(SimonState.PlayingSequence);
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
            ChangeState(SimonState.AwaitingPlayer);
        }






        //State Functions


        private void RunState()
        {
            switch (state)
            {
                case SimonState.PlayingSequence:
                    
                    break;
                case SimonState.AwaitingPlayer:
                    
                    break;
                case SimonState.GameOver:
                    break;
            }
        }



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
                case SimonState.GameOver:

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
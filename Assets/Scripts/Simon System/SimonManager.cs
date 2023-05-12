using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimonState
{
    StartingGame,
    CountingDownComputer,
    PlayingSequence,
    CountingDownPlayer,
    PlayerTurn,
    AwaitingFinalAnimation,
    VictoryAnimation,
    GameOver
}

namespace SimonSystem
{
    public class SimonManager : MonoBehaviour
    {
        private static SimonManager instance;


        private Character player;
        [SerializeField] private SimonActionManager actionManager;
        [SerializeField] private SimonSequencePlayer sequence;
        [SerializeField] private GameState gameState;

        [Space]
        [SerializeField] private EventSO onGameStart;
        [SerializeField] private EventSO onSequenceStart;
        [SerializeField] private EventSO onPlayerTurnStart;
        [SerializeField] private EventSO onPlayerTurnEnd;
        [SerializeField] private EventSO onGameOver;

        [Space(10)]
        [SerializeField] private VarEventSO<SimonAction> onCorrectAction;
        [SerializeField] private VarEventSO<SimonAction> onWrongAction;

        [SerializeField] private VarEventSO<SimonAction> onSimonAction;

        private bool isActive = false;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Character>();
        }

        // Update is called once per frame
        void Update()
        {
            if(gameState.GetGameState() == SimonState.AwaitingFinalAnimation)
            {
                if (player.IsIdle()) ChangeState(SimonState.PlayingSequence);
            }
        }

        //Important Public Functions

        public void StartGame()
        {
            ChangeState(SimonState.StartingGame);
        }

        public void WaitForPlayer()
        {
            ChangeState(SimonState.PlayerTurn);
        }

        public void StartSequence() { ChangeState(SimonState.PlayingSequence); }

        public static void GameOver()
        {
            instance.ChangeState(SimonState.GameOver);
        }


        public void ProcessAction(SimonAction action)
        {
            if (gameState.GetGameState() == SimonState.PlayerTurn)
            {
                bool correct = gameState.CompareAction(action);

                if (correct)
                {
                    gameState.IncrementActionIndex();
                    onCorrectAction.CallEvent(action);

                    if (gameState.ActionsDone())
                    {
                        ChangeState(SimonState.AwaitingFinalAnimation);
                    }
                }
                else
                {
                    onWrongAction.CallEvent(action);
                }
            }
        }

        public static void AddAction(SimonAction action)
        {
            GetState().AddAction(action);
        }




        //State Functions




        private void ChangeState(SimonState newState)
        {
            Debug.Log(newState);
            switch (newState)
            {
                case SimonState.StartingGame:
                    onGameStart.CallEvent();
                    //hide menus
                    break;
                case SimonState.PlayingSequence:
                    AddRandomAction();
                    sequence.Play();
                    onSequenceStart.CallEvent();
                    break;
                case SimonState.PlayerTurn:
                    onPlayerTurnStart.CallEvent();
                    break;
                case SimonState.AwaitingFinalAnimation:
                    gameState.ResetActionIndex();
                    onPlayerTurnEnd.CallEvent();
                    break;
                case SimonState.VictoryAnimation:

                    break;
                case SimonState.GameOver:
                    gameState.ResetActions();
                    onGameOver.CallEvent();
                    break;
            }
            gameState.SetGameState(newState);
        }




        //Getters
        public static GameState GetState()
        {
            return instance.gameState;
        }

        public static SimonState GetCurrentState() { return GetState().GetGameState(); }

        public static List<SimonAction> GetAllActions() { return GetState().GetActions(); }


        //Helper Functions

        private void AddRandomAction()
        {
            var action = actionManager.GetRandomAction();
            AddAction(action);
        }

        /*private bool CompareAction(SimonAction action)
        {
            return selector.GetActionAtIndex(actionIndex) == action;
        }*/



    }
}
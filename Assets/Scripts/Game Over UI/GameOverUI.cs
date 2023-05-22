using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameOverUIState
{
    Appearing,
    Active,
    Disappearing,
    Inactive
}

public class GameOverUI : MonoBehaviour
{

    private GameOverUIState uiState = GameOverUIState.Inactive;
    [SerializeField] private RectTransform gameOverUI;
    [SerializeField] private CanvasGroup background;

    [Space]
    [Range(1, 5)]
    [SerializeField] private float gameOverStartTime = 2f;
    [Range(1,5)]
    [SerializeField] private float transitionTime = 1f;

    [SerializeField] private float activeY = 245;
    [SerializeField] private float inactiveY = -131;

    [SerializeField] private EventSO onGameRestart;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 currPos = gameOverUI.transform.position;
        gameOverUI.transform.position = new Vector3(currPos.x, inactiveY, currPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /****************
     * Public
     * ***************/

    public void StartUIAppear()
    {
        StartChangeStateAfterTime(gameOverStartTime, GameOverUIState.Appearing);
    }

    public void StartUIDisappear()
    {
        ChangeState(GameOverUIState.Disappearing);
    }

    /********************
     * Helpers
     * ****************/

    private void UIAppear()
    {
        gameOverUI.transform.LeanMoveY(activeY, transitionTime).setEaseInBounce();
        background.LeanAlpha(1, transitionTime);
        background.blocksRaycasts = true;
        StartChangeStateAfterTime(transitionTime, GameOverUIState.Active);
    }

    private void UIDisappear()
    {
        gameOverUI.transform.LeanMoveY(inactiveY, transitionTime).setEaseOutBack();
        background.LeanAlpha(0, transitionTime);
        background.blocksRaycasts = false;
        StartChangeStateAfterTime(transitionTime, GameOverUIState.Inactive);

        onGameRestart.CallEvent();
    }

    private void ChangeState(GameOverUIState newState)
    {
        switch (newState)
        {
            case GameOverUIState.Appearing:
                UIAppear();
                break;
            case GameOverUIState.Disappearing:
                UIDisappear();
                break;
        }


    }

    private void StartChangeStateAfterTime(float time, GameOverUIState newState)
    {
        StartCoroutine(ChangeStateAfterTime(time, newState));
    }

    private IEnumerator ChangeStateAfterTime(float time, GameOverUIState newState)
    {
        yield return new WaitForSecondsRealtime(time);
        ChangeState(newState);
    }

}

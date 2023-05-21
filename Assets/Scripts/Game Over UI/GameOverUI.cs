using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Vector2 hiddenOffset;

    // Start is called before the first frame update
    void Start()
    {
        uiState = GameOverUIState.Appearing;
        gameOverUI.anchoredPosition = hiddenOffset;
    }

    // Update is called once per frame
    void Update()
    {
        switch (uiState)
        {
            case GameOverUIState.Appearing:
                UIAppear();
                break;
            case GameOverUIState.Active:
                break;
            case GameOverUIState.Disappearing:
                UIDisappear();
                break;
            case GameOverUIState.Inactive:
                break;
        }
    }

    private void UIAppear()
    {
        float currY = gameOverUI.anchoredPosition.y;
        float goalY = 0;
        float newValue = LeanTween.easeInCubic(currY, goalY, 0.2f);
        gameOverUI.anchoredPosition = new Vector2(0, newValue);
    }

    private void UIDisappear()
    {

    }

    private bool TweenUI(float stop, float leeway)
    {
        float currY = gameOverUI.anchoredPosition.y;
        
        return false;
    }

    private void ChangeState(GameOverUIState newState)
    {
        switch (uiState)
        {

        }


    }

}

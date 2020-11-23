using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelectionUI : MonoBehaviour
{
	[SerializeField] private ScrollRect scrollView;
	[SerializeField] private RectTransform content;
	[SerializeField] private LoadScreen loadScreen;
	[SerializeField] private List<LevelData> levels = new List<LevelData>();

	[Space(20)]
	[SerializeField] private Vector2 backdropSize;
	[SerializeField] private float stopTime = 0.2f;

	private LevelData currentLevel;

	void Start()
	{
		SetupBackdrops();
		currentLevel = levels[0];
		loadScreen = FindObjectOfType<LoadScreen>();
	}

	void Update()
	{
			
	}

	public void LoadCurrentLevel()
    {
		string levelName = currentLevel.levelName;

		loadScreen.LoadOut(levelName);
    }

	public void SetContentPosition()
	{
		scrollView.StopMovement();

		Debug.Log("Content position set");
		float x = -content.anchoredPosition.x;
		Debug.Log(x);
		int index = GetNearestIndexFromPosition(x);
		Debug.Log(index);
		float newX = -index * backdropSize.x;
		Debug.Log(newX);

		Vector2 newPosition = new Vector2(newX, 0);

		UpdateCurrentLevel(index);
		DOTween.To(() => content.anchoredPosition, z => content.anchoredPosition = z, newPosition, stopTime);
	}

	private void UpdateCurrentLevel(int index)
    {
		currentLevel = levels[index];
    }

	private void SetupBackdrops()
    {
		UnityUtilities.RemoveChildren(content.transform);

		GameObject obj = new GameObject();
		Image img = obj.AddComponent<Image>();

		for (int i = 0; i < levels.Count; i++)
        {
			Image backdrop = Instantiate(img);
			backdrop.rectTransform.sizeDelta = backdropSize;
			backdrop.sprite = levels[i].background;

			backdrop.transform.SetParent(content.transform);
        }
    }


	private int GetNearestIndexFromPosition(float x)
    {
		int index = Mathf.RoundToInt(x / backdropSize.x);
		index = Mathf.Min(levels.Count - 1, index);
		index = Mathf.Max(0, index);

		return index;
    }
	
}

using DG.Tweening;
using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
	private CanvasGroup cgroup;
	[SerializeField] private Image loadBackground;

	[Space(20)]
	[SerializeField] private Color loadScreenColor;
	[SerializeField] private float loadDuration;

	[Space(20)]
	[SerializeField] private EventSO OnLoadInStart;
	[SerializeField] private EventSO OnLoadInFinish;
	[SerializeField] private EventSO OnLoadOutStart;
	[SerializeField] private EventSO OnLoadOutFinish;

	private bool isRunning = false;
	void Start()
	{
		cgroup = transform.GetComponent<CanvasGroup>();
		SetLoadScreenColor(loadScreenColor);

		//DontDestroyOnLoad(this);
		SceneManager.sceneLoaded += (x, y) => LoadIn();
		cgroup.alpha = 1;
		
		LoadIn();
	}

	private void SetLoadScreenColor(Color color)
    {
		if(loadBackground != null)
        {
			loadBackground.color = color;
        }
    }



	
	public void LoadIn(float loadDuration)
    {
		if (!isRunning)
		{
			ActivateLoad();
			OnLoadInStart?.CallEvent();
			var tweener = DOTween.To(() => cgroup.alpha, x => cgroup.alpha = x, 0, loadDuration);
			tweener.OnComplete(() =>
								{
									OnLoadInFinish?.CallEvent();
									isRunning = false;
									cgroup.blocksRaycasts = false;
								});
		}
    }

	public void LoadIn()
    {
		LoadIn(loadDuration);
    }


	public void LoadOut(string sceneName)
	{
		if (!isRunning)
		{
			ActivateLoad();
			OnLoadOutStart?.CallEvent();
			var tweener = DOTween.To(() => cgroup.alpha, x => cgroup.alpha = x, 1, loadDuration);
			tweener.OnComplete(() =>
								{
									OnLoadOutFinish?.CallEvent();
									isRunning = false;
									UnityUtilities.LoadLevel(sceneName);
								});
		}
	}

	private void ActivateLoad()
    {
		isRunning = true;
		cgroup.blocksRaycasts = true;
    }
}

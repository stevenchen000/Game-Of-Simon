using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum AnimationParameter
{
	Position,
	Rotation,
	Scale
}


[Serializable]
public class UIAnimation : ISerializationCallbackReceiver
{
	[TextArea()]
	public string description;

	[Space(20)]
	public float startTime;
	public float duration;
	public bool animationLoops = false;
	public Ease tweenEasing = Ease.OutBack;

	[Space(20)]
	public bool animLoops;
	public LoopType loopType = LoopType.Yoyo;

	[Space(20)]
	public AnimationParameter paramType;
	public RectTransform trans;
	public Vector2 toPos;


	[Range(0,1)]
	[Space(20)]
	public float percentage = 0;
	private Tween tweener;


    public void StartAnimation()
    {
        switch (paramType)
        {
            case AnimationParameter.Position:
				tweener = trans.DOAnchorPos(toPos, duration);
				break;
            case AnimationParameter.Rotation:
				
                break;
            case AnimationParameter.Scale:
				tweener = trans.DOScale(toPos, duration);
                break;
        }

		tweener.SetEase(tweenEasing);
		if (animLoops) tweener.SetLoops(-1, loopType);
		tweener.OnUpdate(() => percentage = tweener.ElapsedDirectionalPercentage());
    }

    public void StopAnimation()
    {
		if(tweener != null)
        {
			tweener.Kill(true);
			tweener = null;
        }
    }



	public void OnAfterDeserialize()
	{
        
    }

    public void OnBeforeSerialize(){}
}

public class UIAnimator : MonoBehaviour
{
	[SerializeField] private bool startOnAwake;
	[SerializeField] private float delay;
	[SerializeField] private List<UIAnimation> animations = new List<UIAnimation>();
	
	private Timer timer;
	private bool isRunning;

	void Start()
	{
		if (startOnAwake) StartAnimation();
	}

	void Update()
	{
		if(timer != null)
        {
			timer.Tick();
			RunAnimations();
        }
	}



	//public functions

	public void StartAnimation()
    {
		timer = new Timer();
		KillAllAnimations();
    }

	public void StopAnimation()
    {

    }






	//Helper Funcs

	private void RunAnimations()
    {
		bool finished = true;
		Debug.Log("Animation running");
		Debug.Log(timer.GetCurrentTime());
		for(int i = 0; i < animations.Count; i++)
        {
            if (TimeForAnimationToStart(animations[i]))
            {
				Debug.Log("Animation reached");
				animations[i].StartAnimation();
            }
            else if (AnimationHasStarted(animations[i]))
            {
				
            }
            else
            {
				finished = false;
            }
        }

		if (finished)
		{
			timer = null;
			Debug.Log("Animation finished");
		}
    }

	private void KillAllAnimations()
    {
		for(int i = 0; i < animations.Count; i++)
        {
			animations[i].StopAnimation();
        }
    }

	private bool TimeForAnimationToStart(UIAnimation anim)
    {
		return timer.AtTime(anim.startTime + delay);
    }

	private bool AnimationHasStarted(UIAnimation anim)
    {
		float startTime = anim.startTime + delay;
		return timer.PassedTime(startTime);
    }
}

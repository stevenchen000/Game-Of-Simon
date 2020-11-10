using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSystem
{
	public enum SequenceType
    {
		AllActions,
		LastAction
    }


	public class SimonSequencePlayer : MonoBehaviour
	{
		private SimonActionSelector selector;
		private Character player;
		private AudioSource audio;

		[SerializeField] private SequenceType sequence;

		[Space(20)]
		[SerializeField] private float startDelayTime = 1f;
		[SerializeField] private float endDelayTime = 1f;
		[Range(0,1)]
		[SerializeField] private float timeBetweenActions;

		[Space(20)]
		[SerializeField] private AudioClip clapSound;

		[Space(20)]
		[Range(1,10)]
		[SerializeField] private int groupSize = 3;
		[Range(0,1)]
		[SerializeField] private float groupWaitTime = 0.2f;

		[Space(20)]
		[SerializeField] private EventSO onSequenceFinish;
		[SerializeField] private VarEventSO<SimonAction> onActionRun;
		void Start()
		{
			selector = FindObjectOfType<SimonActionSelector>();
			player = FindObjectOfType<Character>();
			audio = transform.GetComponent<AudioSource>();
		}

		public void Play()
        {
			StartCoroutine(PlayClaps());
        }

		private IEnumerator PlayClaps()
        {
			for(int i = 0; i < 3; i++)
            {
				audio.PlayOneShot(clapSound);
				yield return new WaitForSeconds(timeBetweenActions);
            }

			StartCoroutine(PlayAllAnimations());
        }

		private IEnumerator PlayAllAnimations()
        {
			var actionList = GetListOfActions();
			int numOfActions = actionList.Count;

			yield return new WaitForSeconds(startDelayTime);

			for(int i = 0; i < numOfActions; i++)
			{
				if (StartOfNewGroup(numOfActions, i)) yield return new WaitForSeconds(groupWaitTime);
				else if(i != 0) yield return new WaitForSeconds(timeBetweenActions);

				SimonAction action = actionList[i];
				action.RunAction(player);
				onActionRun?.CallEvent(action);
			}

			yield return new WaitForSeconds(endDelayTime);

			onSequenceFinish?.CallEvent();
        }

		private bool StartOfNewGroup(int numOfActions, int index)
        {
			bool result = index % groupSize == 0;
			
			if(index == 0)
            {
				result = false;
            }
			/*else if(index % groupSize == 0)
			{
				int remainingActions = numOfActions - index;

				if (remainingActions <= groupSize/2) result = false;
            }
            else if(index % groupSize != 0)
            {
				result = false;
            }
			*/
			return result;
        }





		private List<SimonAction> GetListOfActions()
        {
			List<SimonAction> result = new List<SimonAction>();

            switch (sequence)
            {
                case SequenceType.AllActions:
					result.AddRange(selector.GetAllActions());
					break;
                case SequenceType.LastAction:
					int numOfActions = selector.GetNumberOfActions();
					result.Add(selector.GetActionAtIndex(numOfActions - 1));
                    break;
            }

			return result;
        }
    }
}
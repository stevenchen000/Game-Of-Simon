using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSystem{
	public class SimonActionPlayer : MonoBehaviour
	{
		private Character character;
		private Animator anim;
		private SimonAction currentAction;
		private Timer timer;
		private Vector3 defaultPosition;
		private Quaternion defaultRotation;
		private Vector3 defaultScale;

		void Start()
		{
			character = transform.GetComponent<Character>();
			anim = transform.GetComponentInChildren<Animator>();
			timer = new Timer();
		}

		void Update()
		{
			
		}

		public void PlayAction(SimonAction action)
        {
			action.RunAction(character);
        }



		private void SetupDefaultTransforms()
        {
			defaultPosition = transform.position;
			defaultRotation = transform.rotation;
			defaultScale = transform.localScale;
		}
	}
}
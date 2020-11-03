using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSystem{
	public class SimonActionPlayer : MonoBehaviour
	{
		private Animator anim;
		private SimonAction currentAction;
		private Timer timer;
		private Vector3 defaultPosition;
		private Quaternion defaultRotation;
		private Vector3 defaultScale;

		void Start()
		{
			anim = transform.GetComponentInChildren<Animator>();
			timer = new Timer();
		}

		void Update()
		{
			
		}

		private void SetupDefaultTransforms()
        {
			defaultPosition = transform.position;
			defaultRotation = transform.rotation;
			defaultScale = transform.localScale;
		}
	}
}
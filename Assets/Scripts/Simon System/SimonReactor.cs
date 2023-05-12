using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SimonSystem
{
    [Serializable]
    public class SimonReactionContainer
    {
        public SimonAction action;
        public SimonReaction reaction;
    }

    public class SimonReactor : MonoBehaviour
    {
        [SerializeField] private List<SimonReactionContainer> reactions = new List<SimonReactionContainer>();
        [SerializeField] private float crossfadeTime = 0.1f;
        private Dictionary<SimonAction, SimonReaction> _reactions = new Dictionary<SimonAction, SimonReaction> ();
        
        private Animator anim;
        private Vector3 defaultPosition;
        private Vector3 defaultScale;

        // Start is called before the first frame update
        void Start()
        {
            anim = transform.GetComponent<Animator>();
            defaultPosition = transform.position;
            defaultScale = transform.localScale;
            SetupDictionary();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void React(SimonAction action)
        {
            if (_reactions.ContainsKey(action))
            {
                _reactions[action].PlayReaction(this);
            }
        }



        public void PlayAnimation(string animation)
        {
            anim.CrossFade(animation, crossfadeTime, 0, 0);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }




        private void SetupDictionary()
        {
            for(int i = 0; i < reactions.Count; i++)
            {
                SimonAction action = reactions[i].action;
                SimonReaction reaction = reactions[i].reaction;

                _reactions[action] = reaction;
            }
        }
    }
}
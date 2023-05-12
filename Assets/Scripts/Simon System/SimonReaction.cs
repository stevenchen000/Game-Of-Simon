using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace SimonSystem
{
    [System.Serializable]
    public class SimonReaction
    {
        [SerializeField] private string animationName;
        [SerializeField] private bool changePosition;
        [SerializeField] private Vector3 newPosition;
        [SerializeField] private Vector3 newScale = new Vector3(1,1,1);

        public void PlayReaction(SimonReactor reactor)
        {
            reactor.PlayAnimation(animationName);
            if (changePosition)
            {
                
            }
        }
    }
}
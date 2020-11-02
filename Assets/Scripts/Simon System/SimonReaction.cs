using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace SimonSystem
{
    [CreateAssetMenu(menuName = "Simon Action/Reaction")]
    public class SimonReaction : ScriptableObject
    {
        [SerializeField] private string animationName;

        public void PlayReaction(SimonReactor reactor)
        {
            reactor.PlayAnimation(animationName);
        }
    }
}
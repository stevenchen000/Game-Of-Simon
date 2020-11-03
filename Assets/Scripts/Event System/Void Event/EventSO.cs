using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    [CreateAssetMenu(menuName = "Events/Void Event")]
    public class EventSO : ScriptableObject, ISerializationCallbackReceiver
    {
        [TextArea(4,10)]
        [SerializeField] private string description;
        public delegate void VoidEvent();
        private event VoidEvent _event;

        [Space(20)]
        [SerializeField] private bool showDebugText;

        public void SubscribeToEvent(VoidEvent func)
        {
            _event += func;
        }

        public void UnsubscribeFromEvent(VoidEvent func)
        {
            _event -= func;
        }

        public void CallEvent()
        {
            if(showDebugText) Debug.Log($"Called event {name}");
            _event?.Invoke();
        }

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
        }
    }
}
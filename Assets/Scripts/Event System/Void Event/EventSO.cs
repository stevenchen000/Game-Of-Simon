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
        private static bool _showDebugText;
        private bool previousValue;

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
            if(_showDebugText) Debug.Log($"Called event {name}");
            _event?.Invoke();
        }

        public void OnBeforeSerialize()
        {
            if(showDebugText != previousValue)
            {
                previousValue = showDebugText;
                _showDebugText = showDebugText;
            }else if(_showDebugText != showDebugText)
            {
                showDebugText = _showDebugText;
                previousValue = showDebugText;
            }
        }

        public void OnAfterDeserialize()
        {
        }
    }
}
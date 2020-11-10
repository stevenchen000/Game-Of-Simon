using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    [Serializable]
    public class EventListenObject : ISerializationCallbackReceiver
    {
        [TextArea()]
        public string description;
        public EventSO eventSO;
        public UnityEvent unityEvent;

        public void SetupEvent()
        {
            eventSO?.SubscribeToEvent(unityEvent.Invoke);
        }

        public void RemoveEvent()
        {
            eventSO?.UnsubscribeFromEvent(unityEvent.Invoke);
        }



        #region Serialization

        public void OnBeforeSerialize() 
        {
            string eventName = eventSO == null ? "" : eventSO.name;
            int numOfEvents = unityEvent.GetPersistentEventCount();
            string methodNames = GetAllMethodNames();
            
            description = $"{eventName} - {numOfEvents} action(s)\n{methodNames}";
        }

        private string GetAllMethodNames()
        {
            string methods = "";

            for(int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            {
                var target = unityEvent.GetPersistentTarget(i);
                if (target != null)
                {
                    string targetName = target.name;
                    string methodName = unityEvent.GetPersistentMethodName(i);
                    
                    methods += $"{i + 1}) {methodName} ({targetName})\n";
                }
            }

            return methods.Trim();
        }

        public void OnAfterDeserialize() { }

        #endregion
    }

    public class EventListener : MonoBehaviour, ISerializationCallbackReceiver
    {
        [TextArea(2,10)]
        [SerializeField] private string description;
        public List<EventListenObject> eventObjects = new List<EventListenObject>();

        // Start is called before the first frame update
        void Start()
        {
            SetupEventObjects();
        }

        private void OnDestroy()
        {
            CancelEventObjects();
        }


        private void SetupEventObjects()
        {
            for(int i = 0; i < eventObjects.Count; i++)
            {
                eventObjects[i].SetupEvent();
            }
        }

        private void CancelEventObjects()
        {
            for(int i = 0; i < eventObjects.Count; i++)
            {
                eventObjects[i].RemoveEvent();
            }
        }

        public void OnBeforeSerialize()
        {
            description = "";
            for(int i = 0; i < eventObjects.Count; i++)
            {
                description += $"{eventObjects[i].description}";
                if (i != eventObjects.Count - 1) description += "\n\n";
            }
        }

        public void OnAfterDeserialize() { }
    }
}
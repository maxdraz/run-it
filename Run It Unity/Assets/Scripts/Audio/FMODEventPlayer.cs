using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace RunIt.Audio
{
    public class FMODEventPlayer : MonoBehaviour
    {
        
        [EventRef]
        public string Event = "";
        private List<EventInstance> events;
        //private bool playOnTriggerEnter;
        [SerializeField] private bool playOnce;
        //private bool hasPlayed;

        private void Awake()
        {
            events = new List<EventInstance>();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                events[i].release();
            }
        }

        public void Play()
        {
            var eventInstance = CreateEvent();
            eventInstance.start();
            events.Add(eventInstance);
        }
        public void PlayWithParameter(string param, float value)
        {
            var eventInstance = CreateEvent();
            eventInstance.setParameterByName(param, value);
            eventInstance.start();
            events.Add(eventInstance);
        }
        private EventInstance CreateEvent()
        {
            return  RuntimeManager.CreateInstance(Event);
            
        }
    }
}
using System;
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
        [SerializeField] private bool playOnAwake;
        //private bool playOnTriggerEnter;
        [SerializeField] private bool oneInstance;
        private static EventInstance instance;
        //private bool hasPlayed;

        private void Awake()
        {
            events = new List<EventInstance>();
        }

        private void Start()
        {
            if (playOnAwake)
            {
                Play();
            }
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
            if (oneInstance)
            {
                instance = CreateEvent();
                instance.start();
                events.Add(instance);
            }
            else
            {
                var eventInstance = CreateEvent();
                eventInstance.start();
                events.Add(eventInstance);
            }
            
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

        public void Stop()
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                events[i].release();
                events.RemoveAt(i);
            }
        }
    }
}
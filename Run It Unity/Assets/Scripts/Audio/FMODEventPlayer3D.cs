using System;
using UnityEngine;

namespace RunIt.Audio
{
    public class FMODEventPlayer3D : MonoBehaviour
    {
        [FMODUnity.EventRef] public string sound;
        private FMOD.Studio.EventInstance soundEvent;
        private Rigidbody rb;

        private void Start()
        {
            soundEvent = FMODUnity.RuntimeManager.CreateInstance(sound);
            rb = GetComponent<Rigidbody>();
        }

        public void Play()
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, transform, rb);
            soundEvent.start();
        }
    }
}
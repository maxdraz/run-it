using System;
using RunIt.Audio;
using UnityEngine;

namespace RunIt.Collectibles
{
    public class CryptoKey : Collectible
    {
        [SerializeField] private FMODEventPlayer audio;

        private void Awake()
        {
            if (!audio)
            {
                audio = GetComponent<FMODEventPlayer>();
            }
        }

        private void OnDisable()
        {
            audio.Play();
        }
    }
}
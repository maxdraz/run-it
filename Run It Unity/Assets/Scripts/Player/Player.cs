using System;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using RunIt.Spawning;
using UnityEngine;
using RunIt.Utilities;

namespace RunIt.Player
{
    public class Player : MonoBehaviour
    {
        private Health health;
        private Rigidbody rb;
        private void Awake()
        {
            health = GetComponent<Health>();
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            health.PlayerDied += OnRespawn;
        }

        private void OnDisable()
        {
            health.PlayerDied -= OnRespawn;
        }

        private void OnRespawn()
        {
            //reset position
            var checkpoint = CheckpointManager.Instance.CurrentCheckpoint;

            if (!checkpoint) return;
            
            var respawnTransform = checkpoint.RespawnTransform;
            transform.position = respawnTransform.position;
            transform.rotation = respawnTransform.rotation;
            
            //reset health
            health.ResetHealth();
        }

       
    }
}
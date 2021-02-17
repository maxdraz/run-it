using System;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Spawning
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Transform respawnTransform;
        
        public Transform RespawnTransform => respawnTransform;

       private void SetSpawn( )
        {
            CheckpointManager.Instance.SetCheckpoint(this);
        }

       private void OnTriggerEnter(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               SetSpawn();
           }
       }
    }
}
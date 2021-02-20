using System;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Spawning
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Transform respawnTransform;
        [SerializeField] private Detector playerDetector;
        
        public Transform RespawnTransform => respawnTransform;

        private void OnEnable()
        {
            playerDetector.Enter += SetSpawn;
        }

        private void OnDisable()
        {
            playerDetector.Enter -= SetSpawn;
        }

        private void SetSpawn(Collider other)
        {
            CheckpointManager.Instance.SetCheckpoint(this);
           
        }
       
    }
}
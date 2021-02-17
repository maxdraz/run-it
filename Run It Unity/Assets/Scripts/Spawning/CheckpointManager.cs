using System;
using RunIt.Player;
using UnityEngine;

namespace RunIt.Spawning
{
    public class CheckpointManager : MonoBehaviour
    {
        public static CheckpointManager Instance;
        [SerializeField] private Checkpoint currentCheckpoint;

        public Checkpoint CurrentCheckpoint => currentCheckpoint;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

       public void SetCheckpoint(Checkpoint cp)
        {
            currentCheckpoint = cp;
        }
    }
}
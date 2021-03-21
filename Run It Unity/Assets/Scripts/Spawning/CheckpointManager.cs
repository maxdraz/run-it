using System;
using RunIt.Player;
using RunIt.Settings;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Spawning
{
    public class CheckpointManager : MonoBehaviour
    {
        public static CheckpointManager Instance;
        [SerializeField] private Checkpoint currentCheckpoint;
        private GameObject playerGO;
        private Health health;
        [SerializeField] private GameObject DeathUI;
        private BasicUIDisplayer deathTextDisplayer;
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
            
            playerGO = GameObject.FindWithTag("Player");
            health = playerGO.GetComponent<Health>();
            deathTextDisplayer = DeathUI.GetComponentInChildren<BasicUIDisplayer>();
        }

        private void OnEnable()
        {
            if (health)
            {
                health.PlayerDied += OnPlayerDeath;
            }
        }

        public void SetCheckpoint(Checkpoint cp)
        {
            currentCheckpoint = cp;
        }

       public void Respawn()
       {
           var rb = playerGO.GetComponent<Rigidbody>();
           rb.velocity = Vector3.zero;
           
           playerGO.transform.position = currentCheckpoint.RespawnTransform.position;
           playerGO.transform.rotation = currentCheckpoint.RespawnTransform.rotation;
            
           //reset health
           playerGO.GetComponent<Health>().ResetHealth();
           Time.timeScale = 1f;
           CursorSettings.Instance.SetCursorMode(CursorLockMode.Locked);
       }

       private void OnPlayerDeath(DamageInfo damageInfo)
       {
           DeathUI.SetActive(true);
           Time.timeScale = 0;
           CursorSettings.Instance.SetCursorMode(CursorLockMode.None);

           string txt;
           switch (damageInfo.damageType)
           {
                case DamageInfo.DamageType.Enemy:
                    txt = "Killed by Enemy Turret";
                    break;
                case DamageInfo.DamageType.Fall:
                    txt = "Fall damage";
                    break;
                case DamageInfo.DamageType.Hazard:
                    txt = "Killed by lava";
                    break;
                default:
                    txt = "Unknown causes";
                    break;
           }
           deathTextDisplayer.SetText(txt);
       }
    }
}
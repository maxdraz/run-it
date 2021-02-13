using System;
using RunIt.Collectibles;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Airlock
{
    public class AirlockController : MonoBehaviour
    {
        [SerializeField] private int airlockIndex;
        [SerializeField] private DoorController entrance;
        [SerializeField] private DoorController exit;
        private int total; 
        private int collected;
        [SerializeField] private Detector closeEntranceDetector;
        [SerializeField] private Detector openExitDetector;
        [SerializeField] private Detector closeExitDetector;

        private void OnEnable()
        {
            closeEntranceDetector.Enter += CloseEntrance;
            openExitDetector.Enter += OpenExit;
            closeExitDetector.Enter += CloseExit;
        }

        private void Start()
        {
            total = CollectibleManager.Instance.GetCollectibleCount(airlockIndex);
            CollectibleManager.Instance.Collected += OnCollect;
        }
        private void OnDisable()
        {
            CollectibleManager.Instance.Collected -= OnCollect;
            
            closeEntranceDetector.Enter -= CloseEntrance;
            openExitDetector.Enter -= OpenExit;
            closeExitDetector.Enter -= CloseExit;
        }


        void OnCollect(Collectible collectible)
        {
            if (airlockIndex != collectible.AirlockIndex) return;

            collected++;

            if (collected >= total)
            {
                OpenEntrance();
            }
        }

        private void OpenEntrance()
        {
            entrance.Open();
        }

        private void CloseEntrance(Collider other)
        {
            entrance.Close();
        }
        private void CloseExit(Collider other)
        {
            exit.Close();
        }
        private void OpenExit(Collider other)
        {
            exit.Open();
        }
    }
}
using System;
using System.Globalization;
using RunIt.Detection;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Global
{
    public class Timer : MonoBehaviour, ITextDisplayable
    {
        private float elapsed;
        private bool startTimer;
        [SerializeField] private Detector startTimerDetector;
        [SerializeField] private Detector stopTimerDetector;
        

        private void OnEnable()
        {
            startTimerDetector.Enter += OnStartTimer;
            stopTimerDetector.Enter += OnStopTimer;
        }

        private void OnDisable()
        {
            stopTimerDetector.Enter -= OnStopTimer;
        }

        private void Update()
        {
            if (!startTimer) return;
            
            elapsed += Time.deltaTime;
        }

        public string GetDisplayText()
        {
            return Mathf.Round(elapsed).ToString();
        }

        
        
        private void OnStartTimer(Collider other)
        {
            startTimer = true;
        }

        private void OnStopTimer(Collider other)
        {
            startTimer = false;
        }

        public event ITextDisplayable.ValueChangedHandler ValueChanged;
    }
}
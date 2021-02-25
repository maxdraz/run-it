﻿using System;
using System.Globalization;
using RunIt.Detection;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Global
{
    public class Timer : MonoBehaviour, ITextDisplayable
    {
        public static Timer Instance;
        private float elapsed;

        public float Elapsed => elapsed;

        private bool startTimer;
        [SerializeField] private Detector startTimerDetector;
        [SerializeField] private Detector stopTimerDetector;

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

        private void OnEnable()
        {
            if (startTimerDetector)
            {
                startTimerDetector.Enter += OnStartTimer;
            }
            if (stopTimerDetector)
            {
                stopTimerDetector.Enter += OnStopTimer;
            }
        }

        private void OnDisable()
        {
            if (startTimerDetector)
            {
                startTimerDetector.Enter -= OnStartTimer;
            }
            if (stopTimerDetector)
            {
                stopTimerDetector.Enter -= OnStopTimer;
            }
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
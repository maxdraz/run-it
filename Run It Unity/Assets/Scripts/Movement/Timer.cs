using UnityEngine;

namespace RunIt.Movement
{
    public class Timer
    {
        private float maxTime;
        private float time;
        public bool isRunning;
        
        public Timer(float t)
        {
            maxTime = t;
            time = maxTime;
        }
        public void Update()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                isRunning = false;
            }
        }

        public void Start()
        {
            isRunning = true;
            time = maxTime;
        }
    }
}
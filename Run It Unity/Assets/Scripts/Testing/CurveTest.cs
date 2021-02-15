using System;
using UnityEngine;

namespace RunIt.Testing
{
    public class CurveTest : MonoBehaviour
    {
        public AnimationCurve myCurve;
        public bool start;
        public float scale;
        public float t;
        private Vector3 startPos;
        public float acceleration;

        private void Start()
        {
            startPos = transform.position;
        }

        private void Update()
        {
            if (start && t <=1)
            {
                t += Time.deltaTime;
                if (t >= 0.99)
                {
                    t = 1;
                }
            }
            else if(!start && t > 0)
            {
                t -= Time.deltaTime;
                if (t <= 0)
                {
                    t = 0;
                }
            }

            acceleration = myCurve.Evaluate(t);

            var y = myCurve.Evaluate(t);
            var posY = startPos.y;
            var newY = posY + (y * scale);
            var newPos = new Vector3(transform.position.x, newY, transform.position.z);
                
            transform.localPosition = newPos; 
            
           
           
        }
    }
}
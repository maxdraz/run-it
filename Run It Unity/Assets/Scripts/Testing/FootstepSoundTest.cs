using System;
using System.Collections;
using RunIt.Audio;
using UnityEngine;

namespace RunIt.Testing
{
    public class FootstepSoundTest : MonoBehaviour
    {
        [Range(0,12)]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float strideLength;
        [SerializeField] private FMODEventPlayer footstepSound;
        private float time;
        private void Start()
        {
            time += Time.deltaTime;

            var offset = CalculateTime(strideLength, movementSpeed);

            print(offset);
            
        }

        private void Update()
        {
            time += Time.deltaTime;
            var t = CalculateTime(strideLength, movementSpeed);
            print(t);
            if (t - time <= 0)
            {
                footstepSound.Play();
                time = 0;
            }
          
        }

        IEnumerator PlayFootstepSound(float time)
        {
            yield return new WaitForSeconds(time);
            footstepSound.Play();
        }

        private float CalculateTime(float strideL, float speed)
        {
            return strideL / speed;
        }
    }
}
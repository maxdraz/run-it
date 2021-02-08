using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunIt
{
    public class GravitySettings : MonoBehaviour
    {
        [SerializeField] private float gravity = 9.81f;
        // Start is called before the first frame update
        private void OnValidate()
        {
            Physics.gravity = new Vector3(0, -gravity, 0);
        }

        private void Awake()
        {
            Physics.gravity = new Vector3(0, -gravity, 0);
        }
    }
}

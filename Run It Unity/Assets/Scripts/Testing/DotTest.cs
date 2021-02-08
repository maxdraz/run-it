using System;
using UnityEngine;

namespace RunIt.Testing
{
    public class DotTest : MonoBehaviour
    {
        public Transform other;

        private void Update()
        {
            var dot = Vector3.Dot(transform.forward, other.forward);
            print(dot);
        }
    }
}
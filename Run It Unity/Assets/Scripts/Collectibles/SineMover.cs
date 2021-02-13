using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunIt.Collectibles
{
    public class SineMover : MonoBehaviour
    {
        [SerializeField] private Transform toMove;
        private float theta;
        [SerializeField] private float frequency;
        [SerializeField] private float distance;
        private Vector3 startPos;
       
        private void Start()
        {
            if (!toMove) toMove = this.gameObject.transform;
            startPos = transform.position;
            theta = Random.Range(0, 360f);
        }

        private void Update()
        {
         
            theta += (360 * frequency) * Time.deltaTime;
            theta %= 360f;
            var sin = Mathf.Sin(Mathf.Deg2Rad * theta);
            var currentPos = transform.position;
            var newPos = new  Vector3(0, sin * distance, 0);
            newPos = transform.TransformPoint(newPos);
            toMove.position = newPos;
        }
    }
}
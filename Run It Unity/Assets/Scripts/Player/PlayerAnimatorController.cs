using System;
using RunIt.Detection;
using RunIt.Testing;
using UnityEngine;

namespace RunIt.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField] private Animator anim;
        private static readonly int forward = Animator.StringToHash("Forward");
        [SerializeField] private Detector groundDetector;
        private static readonly int grounded = Animator.StringToHash("OnGround");

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            anim.SetFloat(forward, rb.velocity.magnitude);
            anim.SetBool(grounded, groundDetector.detected);
        }
    }
}
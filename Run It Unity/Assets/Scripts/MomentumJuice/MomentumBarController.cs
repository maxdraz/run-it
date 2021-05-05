using System;
using System.Collections;
using System.Collections.Generic;
using RunIt.Input;
using RunIt.Player;
using RunIt.Testing;
using UnityEngine;
using UnityEngine.InputSystem;

public class MomentumBarController : MonoBehaviour
{
    [SerializeField] private UIBar slider;
    [SerializeField] private UIBar image;
    private float percentage;

    [SerializeField] private GroundMover playerMover;

    private InputAction jumpAction;
  //  [SerializeField] private 
  private void Awake()
  {
      playerMover = GameObject.FindWithTag("Player").GetComponent<GroundMover>();
      slider = GetComponent<UIBar>();
  }

  private void Start()
  {
      jumpAction = InputManager.Instance.GetAction("Jump");
  }

  private void Update()
  {
      percentage = playerMover.Velocity.magnitude / playerMover.MaxForwardSpeed;
      if(percentage <=0 || !playerMover.IsGrounded || jumpAction.ReadValue<float>() > 0) return;
      
      slider.Scale(percentage);
      image.Scale(percentage);
  }
}

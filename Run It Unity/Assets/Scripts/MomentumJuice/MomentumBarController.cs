using System;
using System.Collections;
using System.Collections.Generic;
using RunIt.Player;
using RunIt.Testing;
using UnityEngine;

public class MomentumBarController : MonoBehaviour
{
    [SerializeField] private UIBar slider;
    [SerializeField] private UIBar image;
    private float percentage;

    [SerializeField] private GroundMover playerMover;
  //  [SerializeField] private 
  private void Awake()
  {
      playerMover = GameObject.FindWithTag("Player").GetComponent<GroundMover>();
      slider = GetComponent<UIBar>();
  }

  private void Update()
  {
      percentage = playerMover.Velocity.magnitude / playerMover.MaxForwardSpeed;
      if(percentage <=0) return;
      
      slider.Scale(percentage);
      image.Scale(percentage);
  }
}

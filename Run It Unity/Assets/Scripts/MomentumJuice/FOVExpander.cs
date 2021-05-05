using System.Collections;
using System.Collections.Generic;
using RunIt.Testing;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem.Interactions;

public class FOVExpander : MonoBehaviour
{
    private GroundMover playerMover;

    [SerializeField] private float startFOV;

    [SerializeField] private float toAdd = 10f;
    private Camera cam;
    private float targetFOV;
    [SerializeField] private float lerpTime = 0.1f;

    private float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerMover = GameObject.FindWithTag("Player").GetComponent<GroundMover>();
        cam = Camera.main.GetComponent<Camera>();
        startFOV = cam.fieldOfView;
        maxSpeed = playerMover.MaxForwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
       // targetFOV = startFOV + (toAdd * (playerMover.Velocity.magnitude / maxSpeed));
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, lerpTime);
        var dot = Vector3.Dot(playerMover.InputDir, playerMover.Velocity);
        
        if (dot > 0)
        {
            var percentage = playerMover.Velocity.magnitude / maxSpeed;
            cam.fieldOfView = Mathf.Lerp(startFOV, startFOV + toAdd, percentage);

            if (percentage <= 0.7f)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, startFOV, lerpTime);
            }
         //   print(toAdd * (playerMover.Velocity.magnitude / maxSpeed));
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, startFOV, lerpTime);
        }
    }

    private float InterpolateBetween(float start, float target, float percentage)
    {
        return start + target;
    }
}

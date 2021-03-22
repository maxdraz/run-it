using RunIt.Detection;
using RunIt.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class LedgeGrabber : MonoBehaviour
    {
        [SerializeField] private SphereCaster ledgeDetector;
        [SerializeField] private Transform lookTransform;
        private bool grabLedge;
        private InputAction grabAction;
        [SerializeField] private string grabActionName;
        private RaycastHit hitInfo;
        [SerializeField] private Vector3 ledgeOffset;
        [SerializeField] private Vector3 climbUpPos;
        [SerializeField] private float grabSpeed;
        [SerializeField] private float climbSpeed = 4f;
        [SerializeField] private float jumpOffStrength;
        [SerializeField] private float upperJumpSpeed;
        [SerializeField] private float lowerJumpSpeed;
        private Rigidbody rb;
        private Jump jumper;
        private Vector3 hangPos;
        [SerializeField] private float distToTopOfWall;
        //private Mover mover;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            jumper = GetComponent<Jump>();
            //    mover = GetComponent<Mover>();
        }

        private void Start()
        {
            grabAction = InputManager.Instance.GetAction(grabActionName);
        
        }

        private void OnEnable()
        {
            ledgeDetector.Stay += OnLedgeStay;
        }

        private void OnDisable()
        {
            ledgeDetector.Stay -= OnLedgeStay;
        }

        private void Update()
        {
            if (!grabLedge) return;
            
            rb.isKinematic = true;

            // check whether to climb, or jump off
            var input = grabAction.ReadValue<float>();

            if (input <= 0)
            {
                
                //jump
                //get dot with wall
                var dot = Vector3.Dot(hitInfo.normal, transform.forward);

                if (dot <= 0.1f) //facing towards wall
                {
                    //climb on top of wall
                    var climbPos = hitInfo.point + (-hitInfo.normal  * 0.3f) + climbUpPos;
                    var toClimbPos = climbPos - transform.position;
                    transform.Translate(toClimbPos.normalized * (climbSpeed * Time.deltaTime), Space.World);
                    
                    if (toClimbPos.magnitude <= 0.1f)
                    {
                        //transform.position = climbPos;
                        rb.isKinematic = false;
                        rb.velocity = Vector3.zero;
                        FinishGrab();
                   
                    }
                }
                else
                {
                    //jump off
                    rb.isKinematic = false;

                    var dotLookNormal = Vector3.Dot(lookTransform.forward, Vector3.up);

                    if (dotLookNormal > -0.3f) // jump upwards
                    {
                        var jumpVelocity = (transform.forward + transform.up).normalized * upperJumpSpeed;
                        jumper.ExecuteJump(jumpVelocity, false);

                    }
                    else //jump downwards
                    {
                        jumper.ExecuteJump(transform.forward * lowerJumpSpeed, false);
                        print("jumped down " + dotLookNormal);
                    }
                
                    FinishGrab();
                }
            }
            else
            {
                MoveToGrabPoint();
            }
        }


        private void OnLedgeStay(RaycastHit hit)
        {
            var input = grabAction.ReadValue<float>();
            var topOfWallPos = hit.point;
            topOfWallPos.y = hit.collider.bounds.max.y;
            var dist = topOfWallPos - hit.point;
            if (input <= 0 || grabLedge || dist.magnitude > distToTopOfWall) return; //return if no input, already grabbing, wall too high

            hitInfo = hit;
            grabLedge = true;
            rb.velocity = Vector3.zero;
            //  mover.Speed = 0f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(hangPos, 0.5f);
        }

        private void FinishGrab()
        {
            grabLedge = false;
        }

        private void MoveToGrabPoint()
        {
            var newY = hitInfo.collider.bounds.max.y + ledgeOffset.y;
            //hangPos = hitInfo.point; //+ new Vector3(0, newY, 0);

            //hangPos = new Vector3(hitInfo.normal.x, hitInfo.collider.bounds.max.y, hitInfo.normal.z);
            hangPos =hitInfo.point + (hitInfo.normal * ledgeOffset.z);
            hangPos.y = newY;
            var toTarget = hangPos - transform.position;
            transform.Translate(toTarget.normalized * (grabSpeed * Time.deltaTime), Space.World);
            if (toTarget.magnitude <= 0.1f)
            {
                transform.position = hangPos;
            }
        }
    }
}
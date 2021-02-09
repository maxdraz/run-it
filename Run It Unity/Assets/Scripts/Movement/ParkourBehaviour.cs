using System.Collections;
using RunIt.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class ParkourBehaviour : MonoBehaviour
    {
        protected Rigidbody rb;
        [SerializeField] protected string inputName;
        protected InputAction action;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            action = InputManager.Instance.GetAction(inputName);
        }

        protected virtual IEnumerator SubscribeToInputCoroutine()
        {
            yield return new WaitForEndOfFrame();
        }
        
        protected virtual void OnActionStart(InputAction.CallbackContext ctx)
        {
        }
        
        protected virtual void OnActionPerformed(InputAction.CallbackContext ctx)
        {
        }

        protected virtual void OnActionCanceled(InputAction.CallbackContext ctx)
        {
            
        }
    }
}
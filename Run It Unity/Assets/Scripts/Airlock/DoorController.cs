using System;
using UnityEngine;

namespace RunIt.Airlock
{
    public class DoorController : MonoBehaviour
    {
        private Animator anim;
        [SerializeField] private bool opened;

        private void OnValidate()
        {
            if (!Application.isPlaying) return;
            if(!anim) return;
            
            if (opened)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void Open()
        {
           anim.CrossFade("DoorOpen", 0,0);
        }
        
        public void Close()
        {
            anim.CrossFade("DoorClose", 0,0);
        }
    }
}
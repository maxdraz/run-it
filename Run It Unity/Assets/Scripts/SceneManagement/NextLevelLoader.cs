using System;
using RunIt.Managers;
using UnityEngine;

namespace RunIt.SceneManagement
{
    public class NextLevelLoader : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManger.Instance.LoadNextScene();
            }
            
        }
    }
}
using System;
using RunIt.Settings;
using TMPro;
using UnityEngine;

namespace RunIt.UI
{
    public class SetDefaultName : MonoBehaviour
    {
        private TMP_InputField tmp;

        private void Awake()
        {
            tmp = GetComponent<TMP_InputField>();
        }

        private void Start()
        {
            if (GameSettings.Instance)
            {
                tmp.text = GameSettings.Instance.PlayerSettings.playerName;
            }
        }
    }
}
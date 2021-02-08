using System;
using TMPro;
using UnityEngine;

namespace RunIt.UI
{
    public class TMPDisplayer : UIDisplayer
    {
        [SerializeField] protected string text;
        [SerializeField] protected TextMeshProUGUI textMesh;

        private void Awake()
        {
            if (textMesh == null)
            {
                textMesh = GetComponent<TextMeshProUGUI>();
            }
        }
    }
}
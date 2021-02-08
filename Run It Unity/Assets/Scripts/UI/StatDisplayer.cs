using System;
using UnityEngine;

namespace RunIt.UI
{
    public class StatDisplayer : TMPDisplayer
    {
        [SerializeField] private Stat stat;

        private void OnEnable()
        {
            stat.StatChanged += OnStatChanged;
        }

        private void Start()
        {
            var initialValue = stat.GetValue();
            Display(initialValue);
        }

        private void OnDisable()
        {
            stat.StatChanged -= OnStatChanged;
        }

        private void OnStatChanged(object value)
        {
            Display(value);
        }

        protected override void Display(object value)
        {
            textMesh.text = text + value;
        }
    }
}
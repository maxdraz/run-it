using System;
using RunIt.Collectibles;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Player
{
    public class Inventory : MonoBehaviour, ITextDisplayable
    {
        private int cryptokeyCount;
        private void Start()
        {
            ValueChanged?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            var collectible = other.GetComponentInParent<Collectible>();
            if (collectible == null) return;
            AddToInventory(collectible);
        }

        private void AddToInventory(Collectible collectible)
        {
            cryptokeyCount++;
            CollectibleManager.Instance.OnCollect(collectible);
            ValueChanged?.Invoke();
        }
        
        public string GetDisplayText()
        {
            return  cryptokeyCount.ToString() + " / "+ CollectibleManager.Instance.GetCollectibleCount(0);
        }

        public event ITextDisplayable.ValueChangedHandler ValueChanged;
    }
}
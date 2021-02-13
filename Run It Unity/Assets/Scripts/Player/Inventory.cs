using System;
using RunIt.Collectibles;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Player
{
    public class Inventory : Displayable
    {
        private int cryptokeyCount;
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
            InvokeOnValueChanged(collectible.Name, cryptokeyCount);
        }


        public object GetValue()
        {
            return cryptokeyCount;
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Collectibles
{
    public class CollectibleManager : MonoBehaviour
    {
        public static CollectibleManager Instance;
        [SerializeField] private List<Collectible> collectibles;
        
        public delegate void CollectionHandler(Collectible collectible);

        public CollectionHandler Collected;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            collectibles = FindObjectsOfType<Collectible>().ToList();
            collectibles.Sort();
        }

        public void OnCollect(Collectible collectible)
        {
            collectible.OnCollect();
            Collected?.Invoke(collectible);
        }

        public int GetCollectibleCount(int airlockIndex)
        {
            var total = 0;
            for (int i = 0; i < collectibles.Count; i++)
            {
                if (collectibles[i].AirlockIndex > airlockIndex) break;
                else
                {
                    total++;
                }
            }
            
            return total;
        }
    }
}
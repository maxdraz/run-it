using System;
using UnityEngine;

namespace RunIt.Collectibles
{
    public abstract class Collectible : MonoBehaviour, IComparable<Collectible>
    {
        [SerializeField] protected string name;
        [SerializeField] protected int airlockIndex;

        public string Name
        {
            get => name;
        }
        public int AirlockIndex => airlockIndex;

        public virtual void OnCollect()
        {
            gameObject.SetActive(false);
        }

        public int CompareTo(Collectible other)
        {
            if (other == null)
            {
                return 1;
            }

            return AirlockIndex - other.AirlockIndex;

        }
    }
}
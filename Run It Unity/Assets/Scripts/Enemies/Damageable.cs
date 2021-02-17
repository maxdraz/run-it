using UnityEngine;

namespace RunIt.Enemies
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField] protected int damage;

        public int Damage => damage;
    }
}
using System;
using RunIt.Player;
using UnityEngine;

namespace RunIt.Enemies
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField] protected DamageInfo damageInfo;
        public DamageInfo DamageInfo => damageInfo;
    }
}
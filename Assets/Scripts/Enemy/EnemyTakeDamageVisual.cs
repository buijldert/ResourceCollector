/*
	EnemyTakeDamageVisual.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using Interfaces;
using Utility;

namespace Enemy
{
    public class EnemyTakeDamageVisual : TakeDamageVisual, IDamageable
    {
        /// <summary>
        /// Changes the visuals of the enemy when it takes damage.
        /// </summary>
        /// <param name="damage">The damages that the enemy takes.</param>
        public void TakeDamage(float damage)
        {
            if(gameObject.activeSelf)
                ChangeColor(damage);
        }
    }
}

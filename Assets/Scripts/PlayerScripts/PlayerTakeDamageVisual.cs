/*
	PlayerTakeDamageVisual.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using Assets;
using Enemy;
using Utility;

namespace PlayerScripts
{
    /// <summary>
    /// Makes sure the player gets feedback when they're hit.
    /// </summary>
    public class PlayerTakeDamageVisual : TakeDamageVisual
    {
        private void OnEnable()
        {
            PlayerFallDamage.TakeFallDamage += ChangeColor;
            EnemyAttack.OnEnemyAttack += ChangeColor;
            BombExplosion.OnDetonation += ChangeColor;
        }

        private void OnDisable()
        {
            PlayerFallDamage.TakeFallDamage -= ChangeColor;
            EnemyAttack.OnEnemyAttack -= ChangeColor;
            BombExplosion.OnDetonation -= ChangeColor;
        }
    }
}
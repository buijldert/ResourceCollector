/*
	IDamageable.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine.EventSystems;

namespace Interfaces
{
    /// <summary>
    /// Interface for usage when damaging invdividual enemies.
    /// </summary>
    public interface IDamageable : IEventSystemHandler
    {
        void TakeDamage(float damage);
    }
}
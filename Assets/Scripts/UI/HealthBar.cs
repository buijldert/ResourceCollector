using UnityEngine;
using UnityEngine.UI;
using Data;
using Enemy;
using Utility;
using Assets;
using System.Collections;
using PlayerScripts;

public class HealthBar : MonoBehaviour
{
    public delegate void HealthDepeletedAction();
    public static event HealthDepeletedAction OnHealthDepleted;

    //The resource bar image.
    [SerializeField] private Image _resourceBar;
    [SerializeField] private Text _resourceText;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    private void OnEnable()
    {
        PlayerFallDamage.TakeFallDamage += MutateResource;
        PlayerStats.MaxHealth = PlayerStats.Health;
        EnemyAttack.OnEnemyAttack += MutateResource;
        BombExplosion.OnDetonation += MutateResource;
    }

    private void Start()
    {
        _resourceBar.fillAmount = PlayerStats.Health / PlayerStats.MaxHealth;
    }

    /// <summary>
    /// Use this for de - initialization.
    /// </summary>
    private void OnDisable()
    {
        PlayerFallDamage.TakeFallDamage -= MutateResource;
        EnemyAttack.OnEnemyAttack -= MutateResource;
        BombExplosion.OnDetonation -= MutateResource;
    }

    /// <summary>
    /// Mutates the resource by the parameters amount ex: MutateResource(20) = _resource + 20.
    /// MutateResource(-20) = _resource - 20.
    /// </summary>
    /// <param name="resourceMutation">The amount of resources with which the resource will change (-/+).</param>
    public void MutateResource(float resourceMutation)
    {
        PlayerStats.Health += resourceMutation;

        if (PlayerStats.Health <= 0)
            PlayerStats.Health = 0;

        _resourceBar.fillAmount = PlayerStats.Health / PlayerStats.MaxHealth;
        _resourceText.text = PlayerStats.Health + "/" + PlayerStats.MaxHealth;

        if (resourceMutation < 0)
            ShakeEvent.SendShakeEvent();

        if (PlayerStats.Health <= 0)
        {
            GameState.CGameState = CurrentGameState.Paused;
            if (OnHealthDepleted != null)
                OnHealthDepleted();
        }
    }
}

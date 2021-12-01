using System;
using UnityEngine;

public class Hero : MonoBehaviour, IDamagable
{
    #region Variables

        [SerializeField] private int startingHealth;
        [SerializeField] private int health;

        [SerializeField] private DamageDealer takesDamageFrom = DamageDealer.Enemy;

        [SerializeField] private AudioSource audioSource;

        public int StartingHealth { get => startingHealth; }
        public int Health { get => health; }

        public static event Action<int> HealthUpdate;

        public static event Action IsHurt;


	#endregion

    private void Awake()
    {
        health = startingHealth;
        GetComponentInChildren<HitBox>().AttachCharacter(this, takesDamageFrom);

        if(audioSource == null)
			audioSource = GetComponentInChildren<AudioSource>();
    }

	public int GetHealth()
	{
		return health;
	}

    public void Heal(int healAmount)
    {
        var newHealth = health + healAmount;

        if(newHealth >= startingHealth)
            newHealth = startingHealth;

        health = newHealth;

        HealthUpdate?.Invoke(health);
    }

    public void TakeDamage(int damage, AudioClip audioClip)
    {
        TakeDamage(damage);
        audioSource.PlayOneShot(audioClip);
    }

    public void TakeDamage(int damage)
    {
        IsHurt?.Invoke();
        var newHealth = health - damage;

        if(newHealth <= 0)
            Die();

        health = newHealth;
        HealthUpdate?.Invoke(health);
    }

    private void Die()
    {
        GameObject.Destroy(this.gameObject);
        Debug.Log($"<size=14><color=yellow>Yellowjacket</color> <color=red>died.</color></size>", this);
    }


}


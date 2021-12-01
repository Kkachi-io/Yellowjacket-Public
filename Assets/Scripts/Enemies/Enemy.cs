using System;
using CMF;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
	#region Variables

		[Header("Components")]
		[SerializeField] private Animator animator;

		[SerializeField] private AdvancedWalkerController controller;

		[SerializeField] private AudioSource audioSource;

		[SerializeField] private AudioClip walkSound;

		[SerializeField] private AudioClip runSound;

		[SerializeField] private float runPitch = 1.1f;

		[Header("Enemy Info")]
		[SerializeField] private EnemySO baseEnemy;

		[SerializeField] private int health;
		
		[SerializeField] private Vector3 hitImpact = new Vector3();

		private string anim_Hit_string = "Hit";

		private int anim_Hit;

		private string anim_Die_string = "Die";

		private int anim_Die;

		public event Action EnemyDied;

	#endregion

	private void Awake()
	{
		health = baseEnemy.StartingHealth;

		GetComponentInChildren<HitBox>(true).AttachCharacter(this, baseEnemy.TakesDamageFrom);
		
		if(animator == null)
			animator = GetComponentInChildren<Animator>();

		if(controller == null)
			controller = GetComponentInChildren<AdvancedWalkerController>();

		if(audioSource == null)
			audioSource = GetComponentInChildren<AudioSource>();
		
		if(!walkSound)
			audioSource.clip = walkSound;

		anim_Hit = Animator.StringToHash(anim_Hit_string);
		anim_Die = Animator.StringToHash(anim_Die_string);
	}

	public int GetHealth()
	{
		return health;
	}
	
    public void TakeDamage(int damage, AudioClip audioClip)
    {
        TakeDamage(damage);
		audioSource.PlayOneShot(audioClip);
    }

	public void TakeDamage(int damage)
	{
		var newHealth = health - damage;

		if(newHealth <= 0)
		{
			Die();
			return;
		}

		health = newHealth;
		animator.SetTrigger(anim_Hit);
		controller.AddMomentum(hitImpact);
	}

	private void Die()
	{
		health = 0;
		animator.SetTrigger(anim_Die);
		controller.AddMomentum(hitImpact);
		GetComponentInChildren<HitBox>().gameObject.SetActive(false);
		GetComponentInChildren<Weapon>().gameObject.SetActive(false);
		EnemyDied?.Invoke();
	}

	public void PlayMovementAudio()
	{
		if(audioSource.enabled && walkSound)
			audioSource.PlayOneShot(walkSound);
	}

	public void PlayRunAudio()
	{
		if(audioSource.enabled && !audioSource.isPlaying && runSound)
		{
			audioSource.clip = runSound;
			audioSource.loop = true;
			audioSource.pitch = runPitch;
			audioSource.Play();
		}
	}

	public void StopRunAudio()
	{
		if(audioSource.enabled && audioSource.isPlaying)
		{
			audioSource.Stop();
			audioSource.pitch = 1;
		}
	}
}

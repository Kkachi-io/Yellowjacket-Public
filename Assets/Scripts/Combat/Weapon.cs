using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

namespace UnityEngine
{

public class Weapon : MonoBehaviour
{
    #region Variables

        [SerializeField] private WeaponSO weaponSO;

        [SerializeField] private Animator animator;

        [SerializeField] private AudioSource audioSource;

        private int attackAnim;

    #endregion

    private void Awake()
    {
        attackAnim = Animator.StringToHash(weaponSO.AttackAnim);

        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if(this.transform.root.GetComponentInChildren<Hero>())
        {
            CharacterNewInput.Attacked += Attack;
            Slide.IsSliding += Attack;
        }
    }

    private void OnDisable()
    {
        if(this.transform.root.GetComponentInChildren<Hero>())
        {
            CharacterNewInput.Attacked -= Attack;
            Slide.IsSliding -= Attack;
        }
    }

    public int GetWeaponDamage()
    {
        return weaponSO.Damage;
    }

    public AudioClip GetWeaponAudioClip()
    {
        return weaponSO.HitSoundClip;
    }

    public void Attack()
    {
        if(weaponSO.AttackAnimParamType == AttackAnimParamType.Trigger)
        {
            AttackTrigger();
        }
        else
        {
            Debug.LogError($"Weapon.cs does not have an attack method for this type of attack: {weaponSO.AttackAnimParamType.ToString()}", this);
        }

        PlayAttackSound();
    }

	private void AttackTrigger()
	{
		animator.SetTrigger(attackAnim);
	}

    private void PlayAttackSound()
    {
        if(weaponSO.AttackSoundClip)
        {
            audioSource.PlayOneShot(weaponSO.AttackSoundClip);
        }
    }
}

}

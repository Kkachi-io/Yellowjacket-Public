using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageDealer
{
    Hero,
    Enemy,
    Both
}

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour
{
    #region Variables

        [SerializeField] private IDamagable attachedCharacter;

        [HideInInspector] public DamageDealer TakesDamageFrom;

	public void AttachCharacter(IDamagable characterToAttach, DamageDealer takesDamageFrom)
	{
		attachedCharacter = characterToAttach;
		TakesDamageFrom = takesDamageFrom;
	}

	#endregion

	private void OnTriggerEnter(Collider other)
	{
		var weapon = other.GetComponent<Weapon>();

		if(weapon)
		{
			if(weapon.GetWeaponAudioClip())
			{
				attachedCharacter.TakeDamage(weapon.GetWeaponDamage(), weapon.GetWeaponAudioClip());
				return;
			}

			attachedCharacter.TakeDamage(weapon.GetWeaponDamage());
		}
	}

}

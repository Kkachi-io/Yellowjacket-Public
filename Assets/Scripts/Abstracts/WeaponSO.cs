using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackAnimParamType
{
    Trigger,
    Bool
}

[CreateAssetMenu(menuName = "Kkachi/Weapon", fileName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    #region Variables

        public string Name;

        public int Damage;

        public string AttackAnim = "Attack";

        public AttackAnimParamType AttackAnimParamType = AttackAnimParamType.Trigger;

        public AudioClip AttackSoundClip;

        public AudioClip HitSoundClip;

    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Kkachi/EnemySO", fileName = "Enemy")]
public class EnemySO : ScriptableObject
{
    #region Variables

        public int StartingHealth;
        public DamageDealer TakesDamageFrom;

    #endregion


}

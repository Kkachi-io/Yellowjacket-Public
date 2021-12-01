// This is just a quick and dirty way to time the animation with the weapon collider.

using UnityEngine;

public class EnemyAnimationListener_LittleWeaver : MonoBehaviour
{
    #region Variables

        [SerializeField] private Weapon weapon;

    #endregion

    public void OnAttack()
    {
        weapon.Attack();
    }
}

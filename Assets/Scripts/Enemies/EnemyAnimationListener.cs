// This is just a quick and dirty way to time the animation with the weapon collider.

using UnityEngine;

public class EnemyAnimationListener : MonoBehaviour
{
    #region Variables

        [SerializeField] private Weapon weapon_r;

        [SerializeField] private Weapon weapon_l;

        [SerializeField] private SpriteRenderer spriteRenderer;



    #endregion

    public void OnAttack()
    {
        if(spriteRenderer.flipX)
            weapon_l.Attack();
        else
            weapon_r.Attack();
    }
}

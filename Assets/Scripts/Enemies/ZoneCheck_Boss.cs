using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCheck_Boss : ZoneCheck
{
    #region Variables

        [SerializeField] private ColliderListener attackRangeLeft;

        [SerializeField] private ColliderListener attackRangeRight;

        [SerializeField] private bool heroInCloseAttackZone = false;

        public bool HeroInCloseAttackZone { get => heroInCloseAttackZone; }


	#endregion


	private new void OnEnable()
    {
        base.OnEnable();

        attackRangeLeft.OnTriggerEntered += CheckZone;

        attackRangeRight.OnTriggerEntered += CheckZone;

        attackRangeLeft.OnTriggerExited += CheckZoneExit;

        attackRangeRight.OnTriggerExited += CheckZoneExit;

    }

    private void CheckZone(Collider other)
    {
        if(other.GetComponent<Hero>())
            heroInCloseAttackZone = true;
    }

    private void CheckZoneExit(Collider other)
    {
        if(other.GetComponent<Hero>())
            heroInCloseAttackZone = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCheck : MonoBehaviour
{
    #region Variables

        [SerializeField] private ColliderListener patrolZone;

        [SerializeField] private ColliderListener attackZone;

        [SerializeField] private Hero hero;

        [SerializeField] private bool hasLeftPatrolZone;

        public bool HasHero { get => hero != null; }

        public Transform HeroTransform { 
            get 
            {
                if(hero != null)
                { return hero.transform; }

                return null;
            }
        }

        public bool HasLeftPatrolZone { get => hasLeftPatrolZone; }

	#endregion

	public void OnEnable()
    {
        attackZone.OnTriggerEntered += AttackZoneTriggerEntered;
        attackZone.OnTriggerExited += AttackZoneTriggerExited;

        patrolZone.OnTriggerEntered += PatrolZoneTriggerEntered;
        patrolZone.OnTriggerExited += PatrolZoneTriggerExited;
    }

	private void OnDisable()
    {
        attackZone.OnTriggerEntered -= AttackZoneTriggerEntered;
        attackZone.OnTriggerExited -= AttackZoneTriggerExited;

        patrolZone.OnTriggerEntered -= PatrolZoneTriggerEntered;
        patrolZone.OnTriggerExited -= PatrolZoneTriggerExited;
    }


	private void AttackZoneTriggerEntered(Collider other)
	{
		var heroEnter = other.GetComponent<Hero>();

        if(heroEnter)
            hero = heroEnter;
	}

	private void AttackZoneTriggerExited(Collider other)
	{
        var heroExit = other.GetComponent<Hero>();

        if(heroExit != null && heroExit == hero)
            hero = null; 
	}

	private void PatrolZoneTriggerEntered(Collider other)
	{
		var collidingObj = other.GetComponent<ZoneCheck>();

        if(collidingObj == this)
            hasLeftPatrolZone = false;
	}
    
    private void PatrolZoneTriggerExited(Collider other)
    {
		var collidingObj = other.GetComponent<ZoneCheck>();

        if(collidingObj == this)
            hasLeftPatrolZone = true;
    }
}

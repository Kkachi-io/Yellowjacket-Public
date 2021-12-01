using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaiMangou.ProRadarBuilder;

public class Health : MonoBehaviour
{
    #region Variables

        [Header("Components")]
        [SerializeField] private Collider col;

        [SerializeField] private GameObject displayObjs;

        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip audioClip;

        [SerializeField] private _2DRadar radar;

        [Header("Stats")]
        [SerializeField] private int healAmount;

        [SerializeField] private float respawnCooldown = 15f;

        private float timer = 0;

        private bool healthIsHiding = false;

        private const string UPGRADE_TAG = "Upgrade";

        private const string DEFAULT_TAG = "Untagged";
        

    #endregion

    private void Update()
    {
        if(!healthIsHiding) return;

        timer += Time.deltaTime;

        if(timer >= respawnCooldown)
            DisplayActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var hero = other.GetComponent<Hero>();

        if(hero)
        {
            hero.Heal(healAmount);
            audioSource.PlayOneShot(audioClip);
            DisplayActive(false);
        }
    }

    private void DisplayActive(bool isVisible)
    {
        radar.Blips.Find(x => x.Tag == tag).IsActive = isVisible;
        col.enabled = isVisible;
        displayObjs.SetActive(isVisible);
        healthIsHiding = !isVisible;
        timer = 0;
    }


}

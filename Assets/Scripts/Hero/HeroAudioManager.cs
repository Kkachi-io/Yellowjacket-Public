using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kkachi;
using System;
using CMF;
using System.Linq;

public class HeroAudioManager : MonoBehaviour
{
    #region Variables

        [SerializeField] private List<AudioSource> audioSources;

        [SerializeField] private List<AudioClip> footsteps;

        [SerializeField] private List<AudioClip> landings;

        [SerializeField] private List<AudioClip> jumpSounds;
        
        public static event Action IsAttacking;

        public static event Action FinishedAttacking;

    #endregion

    // private void Awake()
    // {
    //     if(audioSources.Count == 0)
    //         audioSources = GetComponents<AudioSource>().ToList();
    //         string breakpointCatcher = null;
    // }

    private void OnEnable()
    {
        SidescrollerController.Jump += PlayJumpAudio;
    }

    private void OnDisable()
    {
        SidescrollerController.Jump += PlayJumpAudio;
    }

    public void PlayFootstep()
    {
        audioSources.FirstFromPool().PlayOneShot(footsteps[UnityEngine.Random.Range(0,footsteps.Count)]);
    }

    public void PlayLandingAudio()
    {
        audioSources.FirstFromPool().PlayOneShot(landings[UnityEngine.Random.Range(0,landings.Count)]);
    }

    public void PlayJumpAudio()
    {
        var audioS = audioSources.FirstFromPool();
        if(audioS)
            audioS.PlayOneShot(jumpSounds[UnityEngine.Random.Range(0,jumpSounds.Count)]);
    }

    public void Attack()
    {
        IsAttacking?.Invoke();
    }

    public void FinishAttack()
    {
        FinishedAttacking?.Invoke();
    }

}

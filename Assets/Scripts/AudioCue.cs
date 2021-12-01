using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class AudioCue : MonoBehaviour
{
    #region Variables

        [SerializeField] private Collider col;

        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip audioClip;

    #endregion

    private void Awake()
    {
        if(!col)
            col = GetComponent<Collider>();
        if(!audioSource)
            audioSource = GetComponent<AudioSource>();

        if(!audioClip)
            Debug.LogError($"{name} in {transform.parent.name} does not have an audio clip.", this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Hero>())
        {
            // audioSource.PlayOneShot(audioClip);
            audioSource.Play();
            col.enabled = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Kkachi;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings_GUI : MonoBehaviour
{
    #region Variables

        [SerializeField] private int logMultiplier = 20;

        [Header("Components")]
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private AudioMixer mixer;

        [SerializeField] private Slider masterSlider;

        [SerializeField] private Slider musicSlider;

        [SerializeField] private Slider sfxSlider;

        [SerializeField] private Transform tabHighlighter;

        [SerializeField] private Tab firstSelectedTab;

        private Tab currentTab = null;
        
        [Header("Animation Settings")]
        [SerializeField] private float fadeInTime = 0.5f;

        [SerializeField] private float fadeOutTime = 0.333f;

        [SerializeField] private float tabMoveTime = 0.25f;
        
        private string mainSequenceID = "Settings Fade";

        private object tabSequenceID = "Switching Settings Tabs";
        
        private const string master_Volume_Param = "MasterVolume";

        private const string music_Volume_Param = "MusicVolume";

        private const string sfx_Volume_Param = "SFXVolume";

    #endregion

    private void Awake()
    {
        LoadAudioSettings();

        var tabs = GetComponentsInChildren<Tab>();
        foreach(var tab in tabs)
        {
            tab.IsSelected += SwitchTab;
        }

        currentTab = firstSelectedTab;

        Reset();
    }

    private void OnEnable()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }

    public void On()
    {
        if(DOTween.IsTweening(mainSequenceID))
            DOTween.Kill(mainSequenceID);
        
        DOTween.CompleteAll();
        
        Sequence Sequence = DOTween.Sequence().SetId(mainSequenceID);
        Sequence.Append(canvasGroup.DOFade(1, fadeInTime))
                .InsertCallback(0, delegate{ canvasGroup.blocksRaycasts = true;
                                            canvasGroup.interactable = true;})
                .Insert(0, currentTab.CanvasGroup.DOFade(1, fadeInTime))
                .InsertCallback(0, delegate{ToggleCanvasGroupInteractivity(currentTab.CanvasGroup, true);
                                                                            currentTab.TabScreen.Activate();})
                .Insert(0, tabHighlighter.DOMoveX(currentTab.TabPosition.x, tabMoveTime));

        currentTab.GetComponent<Button>().Select();
    }

    public void Off()
    {
        SaveAudioSettings();

        if(DOTween.IsTweening(mainSequenceID))
            DOTween.Kill(mainSequenceID);
        
        Sequence Sequence = DOTween.Sequence().SetId(mainSequenceID);
        Sequence.Append(canvasGroup.DOFade(0, fadeOutTime))
                .AppendCallback(Reset);
    }

    private void Reset()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        currentTab.CanvasGroup.alpha = 0;
        currentTab.CanvasGroup.blocksRaycasts = false;
        currentTab.CanvasGroup.interactable = false;
    }

    private void SwitchTab(Tab newTab)
    {
        if(currentTab == newTab) return;

        SaveAudioSettings();

        if(DOTween.IsTweening(tabSequenceID))
            DOTween.Kill(tabSequenceID);
        
        Sequence Sequence = DOTween.Sequence().SetId(tabSequenceID);
        
        // if(currentTab != newTab)
        // {
            Sequence.Append(currentTab.CanvasGroup.DOFade(0, fadeOutTime))
                    .InsertCallback(0, delegate{ToggleCanvasGroupInteractivity(currentTab.CanvasGroup, false);
                                                currentTab.TabScreen.Deactivate();});
        // }

        Sequence.Insert(0, newTab.CanvasGroup.DOFade(1, fadeInTime))
                .InsertCallback(fadeInTime, delegate{ToggleCanvasGroupInteractivity(newTab.CanvasGroup, true);
                                                     newTab.TabScreen.Activate();})
                .Insert(0, tabHighlighter.DOMoveX(newTab.TabPosition.x, tabMoveTime));


        currentTab = newTab;
    }

    private void ToggleCanvasGroupInteractivity(CanvasGroup canvasGroup, bool value)
    {
        canvasGroup.blocksRaycasts = value;
        canvasGroup.interactable = value;
    }

    #region Audio

        private void SetMasterVolume(float value)
        {
            mixer.SetFloat(master_Volume_Param, Mathf.Log10(value) * logMultiplier);
        }

        private void SetMusicVolume(float value)
        {
            mixer.SetFloat(music_Volume_Param, Mathf.Log10(value) * logMultiplier);
        }

        private void SetSFXVolume(float value)
        {
            mixer.SetFloat(sfx_Volume_Param, Mathf.Log10(value) * logMultiplier);
        }

        [ContextMenu("Save")]
        private void SaveAudioSettings()
        {
            PlayerPrefs.SetFloat(master_Volume_Param, masterSlider.value);

            PlayerPrefs.SetFloat(music_Volume_Param, musicSlider.value);

            PlayerPrefs.SetFloat(sfx_Volume_Param, sfxSlider.value);
        }

        [ContextMenu("Load")]
        private void LoadAudioSettings()
        {
            var masterVol = PlayerPrefs.GetFloat(master_Volume_Param, 0.5f);
            SetMasterVolume(masterVol);
            masterSlider.value = masterVol;

            var musicVol = PlayerPrefs.GetFloat(music_Volume_Param, 0.5f);
            SetMusicVolume(musicVol);
            musicSlider.value = musicVol;

            var sfxVol = PlayerPrefs.GetFloat(sfx_Volume_Param, 0.5f);
            SetSFXVolume(sfxVol);
            sfxSlider.value = sfxVol;

        }

        [ContextMenu("Clear Audio Settings")]
        private void ClearAudioSettings()
        {
            PlayerPrefs.DeleteKey(master_Volume_Param);
            PlayerPrefs.DeleteKey(music_Volume_Param);
            PlayerPrefs.DeleteKey(sfx_Volume_Param);
        }

    #endregion


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class DeathGUIController : MonoBehaviour
{
    #region Variables

        [Header("Components")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private QuitConfirmController quitModal;
        [SerializeField] private Button quitCancelButton;
        
        [Header("Canvas Group")]
        [SerializeField] private float canvasDuration = 2.5f;

        [Header("Background")]
        [SerializeField] private Color backgroundStartingColor = new Color(0,0,0,0);
        [SerializeField] private float backgroundAlpha = 0.85f;
        [SerializeField] private float backgroundDuration = 5;

        [Header("Frame Components")]
        [SerializeField] private RawImage frame;
        [SerializeField] private RawImage frameBackground;
        [SerializeField] private RawImage background;

        [Header("Frame")]
        [SerializeField] private float frameBGColorStartTime = 2.5f;
        [SerializeField] private float frameBGColorDuration = 5f;
        [SerializeField] private Color frameBGStartingColor = Color.yellow;
        [SerializeField] private Color frameBGEndColor = Color.red;

        [Header("UI")]
        [SerializeField] private CanvasGroup uiCanvasGroup;
        [SerializeField] private Button firstSelectable;
        [SerializeField] private float uiFadeStartTime = 2.5f;
        [SerializeField] private float uiFadeDuration = 1.5f;
        
        private string sequenceName = "Death Screen Animation";

        private int loadingSceneIndex = 1;
        private string deathUISequenceID = "Death UI Buttons Animation";

	#endregion

	private void Awake()
    {
        ResetDeathScreen();
    }

    [ContextMenu("Death On")]
    public void DeathOn()
    {
        InputManager.SwitchToUIInputs();

        ResetDeathScreen();

        if(DOTween.IsTweening(sequenceName))
            return;

        Sequence deathSequence = DOTween.Sequence().SetId(sequenceName);
        deathSequence.Append(canvasGroup.DOFade(1, canvasDuration))
                        .Insert(0, background.DOFade(backgroundAlpha, backgroundDuration))
                        .Insert(frameBGColorStartTime, frameBackground.DOColor(frameBGEndColor, frameBGColorDuration))
                        .InsertCallback(uiFadeStartTime, delegate{ ToggleUIButtons(true); });
    }

    [ContextMenu("Reset Death Screen")]
    private void ResetDeathScreen()
    {
        if(DOTween.IsTweening(sequenceName))
            DOTween.Kill(sequenceName);

        canvasGroup.alpha = 0;
        frameBackground.color = frameBGStartingColor;
        background.color = backgroundStartingColor;
        uiCanvasGroup.alpha = 0;
        ToggleUIButtonsInteractivity(false);
    }
    
    private void ToggleUIButtons(bool value)
    {
        if(DOTween.IsTweening(deathUISequenceID))
            DOTween.Kill(deathUISequenceID);

        var fadeTime = value ? uiFadeDuration : uiFadeDuration / 2;
        
        Sequence uiSequence = DOTween.Sequence().SetId(deathUISequenceID);
        uiSequence.Append(uiCanvasGroup.DOFade(Convert.ToInt32(value), fadeTime))
                    .InsertCallback(0, delegate{ ToggleUIButtonsInteractivity(value); });

        if(value)
            firstSelectable.Select();
    }

    private void ToggleUIButtonsInteractivity(bool value)
    {
        uiCanvasGroup.blocksRaycasts = value;
        uiCanvasGroup.interactable = value;
    }

    public void OnContinueAfterDeath()
    {
        SceneManager.LoadScene(loadingSceneIndex, LoadSceneMode.Single);
    }
    
    public void OnConfirmQuit()
    {
        ToggleUIButtons(false);

        quitCancelButton.onClick.RemoveAllListeners();
        quitCancelButton.onClick.AddListener( delegate{ToggleUIButtons(true);} );

        quitModal.FadeInConfirmQuit();
    }

}

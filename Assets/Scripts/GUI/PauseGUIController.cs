using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class PauseGUIController : MonoBehaviour
{
    #region Variables

        [Header("Components")]
        [SerializeField] private QuitConfirmController quitModal;

        [SerializeField] private Settings_GUI settingsScreen;

        [SerializeField] private Button firstSelectedButton;

        [SerializeField] private Button quitCancelButton;

        [Header("Canvas Groups")]
        [SerializeField] private CanvasGroup canvasGroup;
        
        [SerializeField] private CanvasGroup titleCanvasGroup;

        [SerializeField] private CanvasGroup uiCanvasGroup;

        [Header("Pause Screen")]
        [SerializeField] private float fadeInTime = 1f;

        [SerializeField] private float fadeOutTime = 0.333f;

        [SerializeField] private RawImage frameBackground;

        [SerializeField] private float fadeInBackgroundMultiplier = 2f;



        private Color frameBackgroundAlphaZero = new Color();

        private string pauseSequenceID = "PauseGUI";

        private string pauseUISequenceID = "PauseUIButtons";

        private float pauseUIFadeTime = 0.333f;
        



	#endregion

	private void Awake()
    {
        frameBackgroundAlphaZero = frameBackground.color;
        frameBackgroundAlphaZero.a = 0;

        Reset();
    }

    public void Pause()
    {
        titleCanvasGroup.alpha = 1;

        if(DOTween.IsTweening(pauseSequenceID))
            DOTween.Kill(pauseSequenceID);

        Sequence onPauseSequence = DOTween.Sequence().SetId(pauseSequenceID);
        onPauseSequence.Append(canvasGroup.DOFade(1, fadeInTime))
                        .InsertCallback(0, delegate{ ToggleAllUIInteractivity(true); })
                        .Insert(0, frameBackground.DOFade(1, fadeInTime * fadeInBackgroundMultiplier))
                        .InsertCallback(fadeInTime, delegate{ ToggleUIButtons(true); });
    }

    public void Unpause()
    {
        if(DOTween.IsTweening(pauseSequenceID))
            DOTween.Kill(pauseSequenceID);

        Sequence offPauseSequence = DOTween.Sequence().SetId(pauseSequenceID);
        offPauseSequence.Append(canvasGroup.DOFade(0, fadeOutTime))
                        .InsertCallback(0, delegate{ ToggleAllUIInteractivity(false); })
                        .Insert(0, frameBackground.DOFade(0, fadeOutTime / fadeInBackgroundMultiplier))
                        .AppendCallback(Reset);
    }

    private void Reset()
    {
        canvasGroup.alpha = 0;
        ToggleAllUIInteractivity(false);

        titleCanvasGroup.alpha = 0;
        uiCanvasGroup.alpha = 0;
        
        frameBackground.color = frameBackgroundAlphaZero;
        
        quitModal.OnCancelQuit();
        settingsScreen.Off();
    }

    public void OnConfirmQuit()
    {
        ToggleUIButtons(false);

        quitCancelButton.onClick.RemoveAllListeners();
        quitCancelButton.onClick.AddListener( delegate{ToggleUIButtons(true);} );

        quitModal.FadeInConfirmQuit();
    }

    public void OnSettingsScreen()
    {
        ToggleUIButtons(false);
        settingsScreen.On();
        InputManager.Inputs.UI.Cancel.started += _ => LeaveSettings();
    }

    private void LeaveSettings()
    {
        ToggleUIButtons(true);
        settingsScreen.Off();
        InputManager.Inputs.UI.Cancel.started -= _ => LeaveSettings();
    }

    public void ToggleUIButtons(bool value)
    {
        if(DOTween.IsTweening(pauseUISequenceID))
            DOTween.Kill(pauseUISequenceID);
        
        Sequence uiSequence = DOTween.Sequence().SetId(pauseUISequenceID);
        uiSequence.Append(titleCanvasGroup.DOFade(Convert.ToInt32(value), pauseUIFadeTime))
                    .Insert(0, uiCanvasGroup.DOFade(Convert.ToInt32(value), pauseUIFadeTime));

        if(value)
            firstSelectedButton.Select();
    }

    private void ToggleAllUIInteractivity(bool value)
    {
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }

}

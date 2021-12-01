using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    #region Variables

        [Header("Components")]
        [SerializeField] private QuitConfirmController quitModal;
        [SerializeField] private Settings_GUI settingsScreen;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button loadingFinishedPlayButton;
        private Button firstSelectedButton;

        [Header("Canvas Groups")]
        [SerializeField] private CanvasGroup mainCanvasGroup;
        [SerializeField] private CanvasGroup titleCanvasGroup;
        [SerializeField] private CanvasGroup uiCanvasGroup;
        [SerializeField] private CanvasGroup creditsCanvasGroup;
        [SerializeField] private CanvasGroup storyCanvasGroup;
        [SerializeField] private CanvasGroup loadingProgressCanvasGroup;
        [SerializeField] private CanvasGroup finishedLoadingCanvasGroup;


        [Header("Animation Timing")]
        [SerializeField] private float initialWaitTime = 2f;
        [SerializeField] private float mainCanvasFadeTime = 3f;
        [SerializeField] private float titleCanvasFadeTime = 2f;
        [SerializeField] private float uiCanvasFadeTime = 1f;
        [SerializeField] private float creditsCanvasStartTime = 3f;
        [SerializeField] private float creditsCanvasFadeTime = 3f;

        [Header("Scene Management")]
        [SerializeField] private Slider loadingSceneProgressBar;
        [SerializeField] private TextMeshProUGUI loadingSceneProgressText;
        [SerializeField] private int mainMenuSceneIndex = 0;
        [SerializeField] private int gameSceneIndex = 1;


        private string sequenceID = "Main Menu Fade";
        private string pauseUISequenceID = "UI Buttons Fade";
        private string loadingProgressSequenceID = "Loading Progress Canvas Group Fade";
        private string sceneLoadedSequenceID = "Scene Loaded Canvas Group Fade";

	#endregion

	private void Awake()
	{
		Reset();

        if(Save.HasSavedGame())
        {
            continueButton.gameObject.SetActive(true);
            firstSelectedButton = continueButton;
            var nav = newGameButton.navigation;
            nav.selectOnUp = continueButton;
            newGameButton.navigation = nav;
        }
        else
        {
            continueButton.gameObject.SetActive(false);
            firstSelectedButton = newGameButton;
        }

        InputManager.SwitchToUIInputs();
	}

	private void Start()
	{
		AnimateIn();
	}

    [ContextMenu("Animate In")]
	private void AnimateIn()
	{
        var titleStartTime = initialWaitTime * 1.5f;
        var uiStartTime = titleStartTime + titleCanvasFadeTime;

		if (DOTween.IsTweening(sequenceID))
			DOTween.Kill(sequenceID);

		Sequence sequence = DOTween.Sequence().SetId(sequenceID);
                // Yellowjacket
		sequence.Insert(initialWaitTime, mainCanvasGroup.DOFade(1, mainCanvasFadeTime))

                // Title
				.Insert(titleStartTime, titleCanvasGroup.DOFade(1, titleCanvasFadeTime))

                // UI Buttons
				.Insert(uiStartTime, uiCanvasGroup.DOFade(1, uiCanvasFadeTime))
                .InsertCallback(uiStartTime, delegate{ ToggleCanvasGroupInteractivity(uiCanvasGroup, true);
                                                        firstSelectedButton.Select();})
                // Credits
                .Insert(creditsCanvasStartTime, creditsCanvasGroup.DOFade(1, creditsCanvasFadeTime))
                .AppendCallback(delegate{firstSelectedButton.Select();});
	}

    public void OnConfirmQuit()
    {
        ToggleUIButtons(false);
        quitModal.FadeInConfirmQuit();
    }

    public void OnSettings()
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
        uiSequence.Append(uiCanvasGroup.DOFade(Convert.ToInt32(value), uiCanvasFadeTime / 2))
                    .InsertCallback(0, delegate{ ToggleCanvasGroupInteractivity(uiCanvasGroup, value);})
                    .Insert(0, titleCanvasGroup.DOFade(Convert.ToInt32(value), uiCanvasFadeTime / 2));

        foreach(var button in uiCanvasGroup.GetComponentsInChildren<Button>())
        {
            button.interactable = value;
        }

        if(value)
            firstSelectedButton.Select();
    }

    public void OnNewGame()
    {
        PlayerPrefs.DeleteAll();
        OnLoadGame();
    }

    public void OnLoadGame() => StartCoroutine(LoadGameAsync());

    private IEnumerator LoadGameAsync()
    {
        Sequence loadingSceneSequence = DOTween.Sequence().SetId(loadingProgressSequenceID);
        loadingSceneSequence.AppendCallback( delegate{ToggleUIButtons(false);})
                            .Append(loadingProgressCanvasGroup.DOFade(1, 0.75f))
                            .Insert(0, storyCanvasGroup.DOFade(1, uiCanvasFadeTime * 2));

        var loadingScene = SceneManager.LoadSceneAsync(gameSceneIndex, LoadSceneMode.Additive);

        while(!loadingScene.isDone)
        {
            loadingSceneProgressBar.value = loadingScene.progress;
            loadingSceneProgressText.text = (loadingScene.progress * 100).ToString("00") + '%';
            yield return null;
        }

        if(DOTween.IsTweening(loadingProgressSequenceID))
            DOTween.Kill(loadingProgressSequenceID);

        Sequence sceneLoadedSequence = DOTween.Sequence().SetId(sceneLoadedSequenceID);
        sceneLoadedSequence.Append(loadingProgressCanvasGroup.DOFade(0, 0.5f))
                            .Insert(0, finishedLoadingCanvasGroup.DOFade(1, 0.5f))
                            .AppendCallback( delegate{ ToggleCanvasGroupInteractivity(finishedLoadingCanvasGroup, true);
                                                        loadingFinishedPlayButton.Select();});
        
    }

    public void OnPlayGame()
    {
        SceneManager.UnloadSceneAsync(mainMenuSceneIndex);
    }

    private void ToggleCanvasGroupInteractivity(CanvasGroup canvasGroup, bool value)
    {
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }

    [ContextMenu("Reset Canvas Groups")]
	private void Reset()
	{
		mainCanvasGroup.alpha = 0;
		titleCanvasGroup.alpha = 0;
        creditsCanvasGroup.alpha = 0;
        storyCanvasGroup.alpha = 0;
        
		uiCanvasGroup.alpha = 0;
        ToggleCanvasGroupInteractivity(uiCanvasGroup, false);

		loadingProgressCanvasGroup.alpha = 0;
		finishedLoadingCanvasGroup.alpha = 0;
        ToggleCanvasGroupInteractivity(finishedLoadingCanvasGroup, false);
	}

}

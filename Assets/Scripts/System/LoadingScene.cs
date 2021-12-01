using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    #region Variables

        [SerializeField] private Slider loadingSceneProgressBar;

        [SerializeField] private TextMeshProUGUI loadingSceneProgressText;

        [SerializeField] private CanvasGroup loadingProgressCanvasGroup;

        [SerializeField] private RawImage blackBG;

        private int gameSceneIndex = 1;

        private int thisSceneIndex = 2;
        
        private string loadingProgressSequenceID = "Loading Progress Canvas Group Fade";

        private string sceneLoadedSequenceID = "Scene Loaded Canvas Group Fade";

    #endregion

    private void Start()
    {
        StartCoroutine(LoadGameAsync());
    }

    private IEnumerator LoadGameAsync()
    {
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
                            .Insert(0, blackBG.DOFade(0, 0.5f))
                            .AppendCallback(UnloadScene);
        
    }

    private void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(thisSceneIndex);
    }
}

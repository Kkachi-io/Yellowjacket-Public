using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    #region Variables

        [SerializeField] private RawImage background;

        [SerializeField] private CanvasGroup title;

        [SerializeField] private CanvasGroup story0;

        [SerializeField] private CanvasGroup story1;

        [SerializeField] private CanvasGroup story2;

        [SerializeField] private CanvasGroup story3;

        [SerializeField] private CanvasGroup yellowJacket;

        [SerializeField] private CanvasGroup thankYou;

        [SerializeField] private CanvasGroup quitUI;

        [SerializeField] private CanvasGroup credits;

        [SerializeField] private Button quitBtn;

        private string sequenceID = "End Game Sequence";

	#endregion

	private void Start()
    {
        ResetAnimation();
        EndAnimationSequence();
    }

    [ContextMenu("EndSequences")]
    private void EndAnimationSequence()
    {
        if(DOTween.IsTweening(sequenceID))
            DOTween.Kill(sequenceID);
        
        Sequence endGameSequence = DOTween.Sequence().SetId(sequenceID);
        endGameSequence.Append(background.DOFade(1, 3))
                        .AppendCallback(UnloadGameScene)
                        .Append(title.DOFade(1, 2))

                        .Insert(6.5f, story0.DOFade(1,3))
                        .Append(story1.DOFade(1,3))
                        .Append(story2.DOFade(1,3))
                        .Append(story3.DOFade(1,3))

                        .Insert(15.5f, yellowJacket.DOFade(1, 5))
                        .Insert(20, story0.DOFade(0, 3))
                        .Insert(20, story1.DOFade(0, 3))
                        .Insert(20, story2.DOFade(0, 3))
                        .Insert(20, story3.DOFade(0, 3))
                        
                        .Append(thankYou.DOFade(1, 2))
                        .Insert(25, quitUI.DOFade(1, 2))
                        .InsertCallback(25, delegate{ quitUI.blocksRaycasts = true;
                                                        quitUI.interactable = true;
                                                        quitBtn.Select();})
                        .Insert(27, credits.DOFade(1, 10));
    }

    [ContextMenu("ResetAnimation")]
    private void ResetAnimation()
    {
        var clr = Color.black;
        clr.a = 0;
        background.color = clr;

        title.alpha = 0;
        story0.alpha = 0;
        story1.alpha = 0;
        story2.alpha = 0;
        story3.alpha = 0;
        yellowJacket.alpha = 0;
        thankYou.alpha = 0;
        quitUI.alpha = 0;
        quitUI.blocksRaycasts = false;
        quitUI.interactable = false;
        credits.alpha = 0;
    }

    private void UnloadGameScene()
    {
        SceneManager.UnloadSceneAsync(2);
    }

    public void OnQuit()
    {
        Application.Quit();
    }


}

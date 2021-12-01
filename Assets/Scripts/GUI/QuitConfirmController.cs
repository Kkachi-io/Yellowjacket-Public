using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class QuitConfirmController : MonoBehaviour
{
    #region Variables

        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private Button firstSelectedButton;

        [SerializeField] private float fadeOnTime = 0.5f;

        private string sequenceID = "FadeOnQuitConfirm";

    #endregion

    private void Awake()
    {
        OnCancelQuit();
    }

    public void FadeInConfirmQuit()
    {
        if(DOTween.IsTweening(sequenceID))
            DOTween.Kill(sequenceID);
        
        Sequence quitConfirmSequence = DOTween.Sequence().SetId(sequenceID);
        quitConfirmSequence.Append(canvasGroup.DOFade(1, fadeOnTime))
                            .InsertCallback(0, delegate{ ToggleConfirmQuitInteractivity(true); })
                            .AppendCallback( delegate{ firstSelectedButton.Select(); });

    }

    public void OnCancelQuit()
    {
        if(DOTween.IsTweening(sequenceID))
            DOTween.Kill(sequenceID);
        
        Sequence cancelSequence = DOTween.Sequence().SetId(sequenceID);
        cancelSequence.Append(canvasGroup.DOFade(0, fadeOnTime / 3))
                            .InsertCallback(0, delegate{ ToggleConfirmQuitInteractivity(false); });
    }

    public void OnQuit()
    {
        if(DOTween.IsTweening(sequenceID))
            DOTween.Kill(sequenceID);

        Application.Quit();
    }

    private void ToggleConfirmQuitInteractivity(bool value)
    {
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }


}

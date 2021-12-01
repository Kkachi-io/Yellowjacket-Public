using UnityEngine;
using DG.Tweening;
using System;

public class FullMapController : MonoBehaviour
{
    #region Variables

        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private float fadeDuration;

        public bool IsDisplaying
        {
            get => Convert.ToBoolean(canvasGroup.alpha);
        }

    #endregion

    public void FullMapToggle(bool value)
    {
        canvasGroup.DOFade(Convert.ToInt32(value), fadeDuration);
    }

}

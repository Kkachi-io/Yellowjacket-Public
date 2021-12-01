using System;
using DG.Tweening;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    #region Variables

        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private float fadeDuration;

        public bool IsDisplaying
        {
            get => Convert.ToBoolean(canvasGroup.alpha);
        }

    #endregion

    public void MiniMapToggle(bool value)
    {
        canvasGroup.DOFade(Convert.ToInt32(value), fadeDuration);
    }

}

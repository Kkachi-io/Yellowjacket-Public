using System.Collections.Generic;
using System.Linq;
using Kkachi;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeBarManager : MonoBehaviour
{
    #region Variables

        public static UpgradeBarManager Instance;
        
        [SerializeField] private static List<LayoutElement> slots;

        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip upgradeAudio;

        [SerializeField] private Vector3 scaleUI = new Vector3(35, 35, 35);

        [SerializeField] private float tweenAnimationTime = 1.25f;

        private string sequenceID = "Move Upgrade Icon to GUI Bar : ";

    #endregion

    private void Awake()
    {
        slots = GetComponentsInChildren<LayoutElement>().ToList();

        if(!Instance)
            Instance = this;
        else
            GameObject.Destroy(this.gameObject);

    }

    public static Transform NextSlot()
    {
        return slots.FirstFromPool().transform;
    }

    public void NewUpgrade(Transform icon, string upgradeName)
    {
        var slotTransform = UpgradeBarManager.NextSlot();
        var slotImage = slotTransform.GetComponent<RawImage>();
        sequenceID += upgradeName;

        if(DOTween.IsTweening(sequenceID))
            DOTween.Kill(sequenceID);
        
        Sequence upgradeSequence = DOTween.Sequence().SetId(sequenceID);
        upgradeSequence.AppendCallback(delegate{icon.SetParent(slotTransform);})
                        .Append(icon.DOLocalMove(Vector3.zero, tweenAnimationTime)).SetEase(Ease.InOutQuart)
                        .Insert(0, icon.DOScale(scaleUI * 2.5f, tweenAnimationTime * 0.333f)).SetEase(Ease.OutQuart)
                        .Insert(tweenAnimationTime * 0.333f, icon.DOScale(scaleUI, tweenAnimationTime * 0.666f)).SetEase(Ease.InQuart)

                        // Play audio clip
                        .InsertCallback(0, delegate{audioSource.PlayOneShot(upgradeAudio);})

                        // Animate Slot Ring
                        .Insert(tweenAnimationTime, slotImage.DOColor(Color.white, tweenAnimationTime));

    }


}

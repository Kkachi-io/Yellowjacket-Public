using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
[RequireComponent(typeof(Collider))]
public class UpgradeUnlock : MonoBehaviour
{
    #region Variables

        [SerializeField] private HeroUpgrade upgradeToUnlock;

        [Header("Components")]
        [SerializeField] private Collider col;

        [SerializeField] private TextMeshPro text;

        [SerializeField] private Transform icon;

        private string sequenceID = "Upgrade Removal";

        public HeroUpgrade UpgradeToUnlock { get => upgradeToUnlock; }

	#endregion

	private void Awake()
    {
        col = GetComponent<Collider>();

        if(!col.isTrigger)
        {
            Debug.Log($"UpgradeUnlock collider wasn't set as Trigger. But now is. You're welcome", this);
            col.isTrigger = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var upgradeManager = other.GetComponent<HeroUpgradeManager>();
        if(upgradeManager)
        {
            upgradeManager.Unlock(upgradeToUnlock);
            TriggerUnlock();
        }
    }

    public void TriggerUnlock()
    {
        col.enabled = false;
        UpgradeBarManager.Instance.NewUpgrade(icon, upgradeToUnlock.ToString());

        sequenceID += upgradeToUnlock.ToString();
        
        if(DOTween.IsTweening(sequenceID))
            DOTween.Kill(sequenceID);
        
        Sequence upgradeSequence = DOTween.Sequence().SetId(sequenceID);
        upgradeSequence.Insert(0, text.DOFade(0, 0.5f))
                        .InsertCallback(0.6f, delegate{GameObject.Destroy(this.gameObject);});
    }



}

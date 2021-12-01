using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class UpgradesScreen : MonoBehaviour, ISettingsTabScreen
{
	#region Variables

        [SerializeField] private List<UpgradeGUI_Obj> upgradeObjs = new List<UpgradeGUI_Obj>();

        [SerializeField] private Transform upgradeHolder;

        [SerializeField] private Transform upgradeScreen;

        [SerializeField] private float fadeTime = 0.25f;
        
        private string sequenceID = "Upgrade Screen";

	#endregion

	private void Awake()
    {
        upgradeObjs = GetComponentsInChildren<UpgradeGUI_Obj>().ToList();
        Deactivate();
    }

	public void Activate()
	{
        var count = 0;
        var upgradeList = HeroUpgradeManager.Instance.GetUpgradeSaveFile().Upgrades;

        if(upgradeList.Count == 0)
        {
            var upgradeGUI_Obj = upgradeObjs.Find(x => x.UpgradeType == HeroUpgrade.None);
            upgradeGUI_Obj.transform.parent = upgradeHolder;
            upgradeGUI_Obj.CanvasGroup.DOFade(1, fadeTime);
            return;
        }

        if(DOTween.IsTweening(sequenceID))
            DOTween.Kill(sequenceID);
        
        Sequence sequence = DOTween.Sequence().SetId(sequenceID);
		foreach(var upgrade in upgradeList)
        {
            var upgradeGUI_Obj = upgradeObjs.Find(x => x.UpgradeType == upgrade);
            upgradeGUI_Obj.transform.parent = upgradeHolder;
            sequence.Insert(((fadeTime * 0.333f) * count), upgradeGUI_Obj.CanvasGroup.DOFade(1, fadeTime));
            count++;
        }
	}

	public void Deactivate()
	{
		foreach(var upgradeGUI_Obj in upgradeObjs)
        {
            upgradeGUI_Obj.CanvasGroup.alpha = 0;
            upgradeGUI_Obj.transform.parent = upgradeScreen;
        }
	}
}

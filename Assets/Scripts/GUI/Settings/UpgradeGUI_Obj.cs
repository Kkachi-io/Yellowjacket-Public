using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeGUI_Obj : MonoBehaviour
{
    #region Variables

        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private HeroUpgrade upgradeType;

        public CanvasGroup CanvasGroup { get => canvasGroup;}

        public TextMeshProUGUI Text { get => text; }

        public HeroUpgrade UpgradeType { get => upgradeType; }

	#endregion



}

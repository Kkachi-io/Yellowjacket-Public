using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;
using DG.Tweening;
using System.Linq;

public class HeroUpgradeManager : MonoBehaviour
{
    #region Variables

        public static HeroUpgradeManager Instance;

        [SerializeField] private List<UpgradeUnlock> upgradeObjects = new List<UpgradeUnlock>();
        
        [SerializeField] private UpgradeSaveFile saveFile = new UpgradeSaveFile();

        // Double Jump
        [SerializeField] private bool mayDoubleJump = false;
        public bool MayDoubleJump { get => mayDoubleJump; }
        private DoubleJump doubleJump = new DoubleJump();
        public DoubleJump DoubleJump { 
            get
            {
                if(!mayDoubleJump) return null;
                return doubleJump;
            }
        }

        // Slide
        [SerializeField] private bool maySlide = false;
        [SerializeField] private Slide slide = new Slide();
        public Slide Slide { 
            get
            {
                if(!maySlide) return null;
                return slide;
            }
        }

        // MiniMap
        [SerializeField] private bool maySeeMiniMap;
        private MiniMap miniMap = new MiniMap();
        public MiniMap MiniMap { 
            get
            {
                if(!maySeeMiniMap) return null;
                return miniMap;
            } 
        }

        // Full Map
        [SerializeField] private bool maySeeFullMap = false;
        private FullMap fullMap = new FullMap();
        public FullMap FullMap { 
            get
            {
                if(!maySeeFullMap) return null;
                return fullMap;
            }
        }



	#endregion

	public HeroUpgradeManager() {}

	private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }

        // Monobehaviour setup for non-mono classes
        var controller = GetComponent<AdvancedWalkerController>();
        var mover = GetComponent<Mover>();
        slide.SetControllerAndMover(controller, mover);

        var guiManager = FindObjectOfType<GUIManager>();
        fullMap.SetGuiManager(guiManager);
        miniMap.SetGuiManager(guiManager);

        upgradeObjects = FindObjectsOfType<UpgradeUnlock>().ToList();

    }

    private void OnEnable()
    {
        AdvancedWalkerController.OnLand += doubleJump.ResetDoubleJump;
    }

    private void OnDisable()
    {
        AdvancedWalkerController.OnLand -= doubleJump.ResetDoubleJump;
    }

    private void Start()
    {
        Save.LoadGame();
    }

    public void Unlock(HeroUpgrade upgrade)
    {
        switch(upgrade)
        {
            case HeroUpgrade.DoubleJump:
                UnlockDoubleJump();
                break;
            case HeroUpgrade.Slide:
                UnlockSlide();
                break;
			case HeroUpgrade.Minimap:
				UnlockMiniMap();
				break;
			case HeroUpgrade.FullMap:
                UnlockFullMap();
                break;
            default:
                Debug.LogError($"Something tried to unlock an unknown upgrade. \nUnknown Upgrade: {upgrade.ToString()}");
                return;
        }

        saveFile.Upgrades.Add(upgrade);
        upgradeObjects.Remove(upgradeObjects.Find(x => x.UpgradeToUnlock == upgrade));
        Save.SaveGame();
    }


    #region Unlocks

        private void UnlockDoubleJump()
        {
            mayDoubleJump = true;
        }

        private void UnlockSlide()
        {
            maySlide = true;
        }

        private void UnlockMiniMap()
        {
            maySeeFullMap = true;
            miniMap.ToggleMiniMap(true);
        }

        private void UnlockFullMap()
        {
            maySeeFullMap = true;
        }

    #endregion

    public UpgradeSaveFile GetUpgradeSaveFile()
    {
        return saveFile;
    }

    public void LoadUpgrades(UpgradeSaveFile newSaveFile)
    {
        foreach(HeroUpgrade upgrade in newSaveFile.Upgrades)
        {
            upgradeObjects.Find(x => x.UpgradeToUnlock == upgrade).TriggerUnlock();
            Unlock(upgrade);
        }
    }

    #region Debug Save Methods

        [ContextMenu("Save")]
        private void Debug_Save()
        {
            Save.SaveGame();
        }

        [ContextMenu("Load")]
        private void Debug_Load()
        {
            Save.LoadGame();
        }

        [ContextMenu("Clear Save Game")]
        private void Debug_ClearSaveFile()
        {
            Save.ClearSaveGame();
        }

    #endregion




}

[System.Serializable]
public class UpgradeSaveFile
{
    public List<HeroUpgrade> Upgrades = new List<HeroUpgrade>();

	public UpgradeSaveFile() {}

	public UpgradeSaveFile(List<HeroUpgrade> upgrades)
    {
        Upgrades = upgrades;
    }
}
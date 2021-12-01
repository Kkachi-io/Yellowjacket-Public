using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Save
{
    #region Variables

    #endregion

    public static void SaveGame()
    {
        var saveFile = HeroUpgradeManager.Instance.GetUpgradeSaveFile();

        foreach(var upgrade in saveFile.Upgrades)
            PlayerPrefs.SetInt(upgrade.ToString(), 1);

        PlayerPrefs.Save();
    }

    public static void LoadGame()
    {
        UpgradeSaveFile saveFile = new UpgradeSaveFile();

        var upgrades = Enum.GetNames(typeof(HeroUpgrade));

        foreach(var upgrade in upgrades)
        {
            if(PlayerPrefs.HasKey(upgrade))
            {
                saveFile.Upgrades.Add((HeroUpgrade) Enum.Parse(typeof(HeroUpgrade), upgrade));
            }
        }
        HeroUpgradeManager.Instance.LoadUpgrades(saveFile);
    }

    public static void ClearSaveGame()
    {
        var upgrades = Enum.GetNames(typeof(HeroUpgrade));

        foreach(var upgrade in upgrades)
        {
            PlayerPrefs.DeleteKey(upgrade);
        }

        Debug.Log("Deleted All Player Prefs / Save Game");
    }

    public static bool HasSavedGame()
    {
        string[] keys = Enum.GetNames(typeof(HeroUpgrade));

        foreach(var key in keys)
        {
            if(PlayerPrefs.HasKey(key))
                return true;
        }

        return false;
    }

}

using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType
{
    TEST = 0,
}

public class AchievementMgr
{
    public bool IsInitialized => SteamManager.Initialized;

    private Dictionary<AchievementType, string> achievementDictionary = new Dictionary<AchievementType, string>()
    {
        {AchievementType.TEST, AchievementType.TEST.ToString() }
    };

    public void SetAchievement(AchievementType type)
    {
        if (achievementDictionary.TryGetValue(type, out string achievementType) == false)
            return;

        SteamUserStats.SetAchievement(achievementType);
        SteamUserStats.StoreStats();
    }

    public bool IsAchieved(AchievementType type)
    {
        if (achievementDictionary.TryGetValue(type, out string achievementType) == false)
            return false;

        SteamUserStats.GetAchievement(achievementType, out bool isAchieved);
        return isAchieved;
    }

    public int GetCurrentAchievementCount(AchievementType type)
    {
        if (achievementDictionary.TryGetValue(type, out string achievementType) == false)
            return 0;

        SteamUserStats.GetStat(achievementType, out int currentCount);
        return currentCount;
    }

    public void SetAchievementCount(AchievementType type, int count)
    {
        if (achievementDictionary.TryGetValue(type, out string achievementType) == false)
            return;

        SteamUserStats.SetStat(achievementType, count);
        SteamUserStats.StoreStats();
    }

    public void AddAchievement(AchievementType type)
    {
        int currentCount = GetCurrentAchievementCount(type);
        SetAchievementCount(type, currentCount + 1);
    }
}

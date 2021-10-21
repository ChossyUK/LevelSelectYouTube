using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefaultData
{
    // String to store the current level to load and set as spawn point in the level select menu
    public string currentLevelName;
    // List of the game levels to set the scene to load & locked status
    public List<LockedLevels> lockedLevels = new List<LockedLevels>();
}

[System.Serializable]
public class LockedLevels
{
    #region Variables
    // Scene Name
    public string sceneToLoad;
    // Locked status
    public bool isLocked;
    #endregion
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Variables
    [SerializeField] int currentLevelIndex = 0; 
    #endregion

    #region Unity Base Methods
    void Start()
    {
        // Start the load level method after 3 seconds
        Invoke("LoadLevel", 3f);
    }
    #endregion

    #region User Methods
    void LoadLevel()
    {
        // Check if the level we want to unlock is not out of bound of our list count.
        if (currentLevelIndex + 1 < DataManager.instance.gameData.lockedLevels.Count)
        {
            // Unlock the level
            DataManager.instance.gameData.lockedLevels[currentLevelIndex + 1].isLocked = false;
            // Save the data
            DataManager.instance.SaveGameData();
        }
        // Load back to the level select scene
        SceneManager.LoadScene("LevelSelect");
    }
    #endregion
}
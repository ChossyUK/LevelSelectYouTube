using UnityEngine;

namespace CedarWoodSoftware
{
	public class MapPointLocker : MonoBehaviour
	{
        #region Variables
        // Array or list to store map points in
        [SerializeField] MapPoint[] mapPoints;
        #endregion

        #region Unity Base Methods
        void Awake()
        {
            // Method to lock the map points
            SetMapPoints();
        }
        #endregion

        #region User Methods
        void SetMapPoints()
        {
            // Check if Level 2 is locked & if so lock any map points required
            if (DataManager.instance.gameData.lockedLevels[1].isLocked)
            {
                // Lock Corner 1
                mapPoints[0].isLocked = true;
            }

            // Check if Level 3 is locked & if so lock any map points required
            if (DataManager.instance.gameData.lockedLevels[2].isLocked)
            {
                // Lock Warp 1
                mapPoints[1].isLocked = true;
            }

            // Check if Level 4 is locked & if so lock any map points required
            if (DataManager.instance.gameData.lockedLevels[3].isLocked)
            {
                // Lock Corner 3
                mapPoints[2].isLocked = true;
            }

            // Check if Level 5 is locked & if so lock any map points required
            if (DataManager.instance.gameData.lockedLevels[4].isLocked)
            {
                // Do nothing we do not want to lock any map or warp points
            }
        }
        #endregion
    }
}
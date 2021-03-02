using UnityEngine;
using UnityEngine.SceneManagement;

namespace CedarWoodSoftware
{
	public class SceneLoader : MonoBehaviour
	{
        #region Variables
        [SerializeField] string levelToLoad = "";
        [SerializeField] int currentLevelIndex = 0;
        #endregion

        #region Unity Base Methods
        void Start()
		{
            Invoke("LoadLevel", 3f);
		}
		#endregion

		#region User Methods
	    void LoadLevel()
        {
            SceneManager.LoadScene(levelToLoad);
        }
		#endregion
	}
}
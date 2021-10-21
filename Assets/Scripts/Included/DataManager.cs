using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Variables
    public static DataManager instance;

    [Header("File Settings")]
    public string saveFileName = "GameData";
    public string folderName = "SaveData";

    [Header("Data Settings")]
    public DefaultData gameData = new DefaultData();

    string defaultPath;
    string fileName;
    #endregion

    #region Unity Base Methods
    void Awake()
    {
        // Create a static instance of the data manager & destroy a new one if it exists in the scene
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Set the folder and file names
        defaultPath = Application.persistentDataPath + "/" + folderName;
        fileName = defaultPath + "/" + saveFileName + ".json";

        // Check if the folder exists if not create it
        if (!FolderExists(defaultPath))
        {
            Directory.CreateDirectory(defaultPath);
        }

        // Load the data
        LoadGameData();
    }
    #endregion

    #region User Methods
    bool FolderExists(string folderPath)
    {
        return Directory.Exists(folderPath);
    }

    public void LoadGameData()
    {
        // Check if the file exists if not create it, if it does exist load the data
        if (File.Exists(fileName))
        {
            string saveData = File.ReadAllText(fileName);
            gameData = JsonUtility.FromJson<DefaultData>(saveData);
        }
        else
        {
            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        // Save the data
        string saveData = JsonUtility.ToJson(gameData);
        File.WriteAllText(fileName, saveData);
    }
    #endregion
}
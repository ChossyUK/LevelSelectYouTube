using TMPro;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    #region Variables
    [Header("WayPoints")]
    public MapPoint up;
    public MapPoint right, down, left;

    [Header("Scene options")]
    [SerializeField] int levelIndex = 0;
    [HideInInspector] public string sceneToLoad;
    [TextArea(1, 2)]
    public string levelName;

    [Header("MapPoint Options")]
    [HideInInspector] public bool isLocked;
    public bool isLevel;
    public bool isCorner;
    public bool isWarpPoint;

    [Header("Warp Options")]
    public bool autoWarp;
    [HideInInspector] public bool haswarped;
    public MapPoint warpPoint;

    [Header("Image Options")]
    [SerializeField] Sprite unlockedSprite = null;
    [SerializeField] Sprite lockedSprite = null;

    [Header("Level UI Objects")]
    [SerializeField] TextMeshProUGUI levelText = null;
    [SerializeField] GameObject levelPanel = null;

    SpriteRenderer spriteRenderer;
    #endregion

    #region Unity Base Methods
    void Start()
    {
        // Get the sprite renderer
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Hide the level panel image
        if (levelPanel != null)
            levelPanel.SetActive(false);

        // If not level or warp point set the sprite renderer image to null
        if (!isLevel && !isWarpPoint)
        {
            if (isLocked && lockedSprite != null)
                spriteRenderer.sprite = lockedSprite;
            else
                spriteRenderer.sprite = null;
        }
        // Else if level set the correct sprite renderer image
        else
        {
            // Set locked status via you own save system here.
            if (isLevel)
            {
                sceneToLoad = DataManager.instance.gameData.lockedLevels[levelIndex].sceneToLoad;
                isLocked = DataManager.instance.gameData.lockedLevels[levelIndex].isLocked;
            }

            if (isLocked)
            {
                // Use the locked image
                if (spriteRenderer.sprite != null)
                    spriteRenderer.sprite = lockedSprite;
            }
            else
            {
                // Use the unlocked image
                if (spriteRenderer.sprite != null)
                    spriteRenderer.sprite = unlockedSprite;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // If a locked level set the level text
            if (isLocked)
            {
                // Show the level panel image
                if (levelPanel != null)
                    levelPanel.SetActive(true);

                // Set the level panel text
                if (levelText != null)
                    levelText.text = "Level Locked";
            }
            // If a level set the level text
            else
            {
                // Show the level panel image
                if (levelPanel != null)
                    levelPanel.SetActive(true);

                // Set the level panel text
                if (levelText != null)
                    levelText.text = levelName;

                // Set any other panels active here the same way we have done the level panel and text
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Hide the level panel image
            if (levelPanel != null)
                levelPanel.SetActive(false);

            // reset the has warped bool
            haswarped = false;

            // Clear the level text
            if (levelText != null)
                levelText.text = "";
        }
    }
    #endregion

    #region User Methods

    #endregion
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LSPlayerController : MonoBehaviour
{
    #region Variables
    // Starting position
    [SerializeField] MapPoint startPoint = null;
    // Player move speed
    [SerializeField] float moveSpeed = 3f;
    // Warp point teleport time
    [SerializeField] float teleportTime = 1f;
    // Player sprites transform
    [SerializeField] Transform playerSprite = null;

    // Array to store all the map points in
    MapPoint[] allPoints;
    // Map points to store our current & previous map points in
    MapPoint prevPoint, currentPoint;

    //Reference to the animator
    Animator animator;
    // Player sprite sprite renderer
    SpriteRenderer spriteRenderer;

    // Floats to store the player movement in
    float x, y;
    // Bool to stop movement
    bool canMove = true;
    // Animation direction
    int direction;
    // Animation bool
    bool animationSet = false;
    // Vector 2 to store input in 
    Vector2 movement;
    #endregion

    #region Unity Base Methods
    void Awake()
    {
        // Find all the map points in the scene
        allPoints = FindObjectsOfType<MapPoint>();
    }

    void Start()
    {
        // Get the animator
        animator = GetComponentInChildren<Animator>();
        // Get the player sprites sprite renderer
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // Disable the sprite renderer
        spriteRenderer.enabled = false;
        // Set the can move bool to false
        canMove = false;
        // Set the players position
        SetPlayerPos();
    }

    void Update()
    {
        // Check if we can move
        if (canMove)
        {
            // Move the player to the next map point
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

            // Check if the player has reached the next map point
            if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f)
            {
                // Check what to do now we have reached a map point
                CheckMapPoint();
            }
            else
            {
                // Play the correct animation
                if (!animationSet)
                    SetAnimation();
            }
        }
    }
    #endregion

    #region User Methods
    void AutoMove()
    {
        // Check if the map point is not null & not the previous map point if so set the next map point to this
        if (currentPoint.up != null && currentPoint.up != prevPoint)
        {
            // Set the map point to this map point
            SetNextPoint(currentPoint.up);
            // Set the animation direction
            direction = 1;
            // Set the animation bool
            animationSet = false;
        }
        // Check if the map point is not null & not the previous map point if so set the next map point to this
        else if (currentPoint.right != null && currentPoint.right != prevPoint)
        {
            // Set the map point to this map point
            SetNextPoint(currentPoint.right);
            // Set the animation direction
            direction = 2;
            // Set the animation bool
            animationSet = false;
        }
        // Check if the map point is not null & not the previous map point if so set the next map point to this
        else if (currentPoint.down != null && currentPoint.down != prevPoint)
        {
            // Set the map point to this map point
            SetNextPoint(currentPoint.down);
            // Set the animation direction
            direction = 3;
            // Set the animation bool
            animationSet = false;
        }
        // Check if the map point is not null & not the previous map point if so set the next map point to this
        else if (currentPoint.left != null && currentPoint.left != prevPoint)
        {
            // Set the map point to this map point
            SetNextPoint(currentPoint.left);
            // Set the animation direction
            direction = 4;
            // Set the animation bool
            animationSet = false;
        }
    }

    void CheckInput()
    {
        // Check for player input & if the map point is not null or locked if so set the next map point to this
        if (y > 0.5f)
        {
            if (currentPoint.up != null && !currentPoint.up.isLocked)
            {
                // Set the map point to this map point
                SetNextPoint(currentPoint.up);
                // Set the animation direction
                direction = 1;
                // Set the animation bool
                animationSet = false;
            }
        }

        // Check for player input & if the map point is not null if so set the next map point to this
        if (x > 0.5f)
        {
            if (currentPoint.right != null && !currentPoint.right.isLocked)
            {
                // Set the map point to this map point
                SetNextPoint(currentPoint.right);
                // Set the animation direction
                direction = 2;
                // Set the animation bool
                animationSet = false;
            }
        }

        // Check for player input & if the map point is not null if so set the next map point to this
        if (y < -0.5f)
        {
            if (currentPoint.down != null && !currentPoint.down.isLocked)
            {
                // Set the map point to this map point
                SetNextPoint(currentPoint.down);
                // Set the animation direction
                direction = 3;
                // Set the animation bool
                animationSet = false;
            }
        }

        // Check for player input & if the map point is not null if so set the next map point to this
        if (x < -0.5f)
        {
            if (currentPoint.left != null && !currentPoint.left.isLocked)
            {
                // Set the map point to this map point
                SetNextPoint(currentPoint.left);
                // Set the animation direction
                direction = 4;
                // Set the animation bool
                animationSet = false;
            }
        }
    }

    void CheckMapPoint()
    {
        // Check if map point is a warp point
        if (currentPoint.isWarpPoint && !currentPoint.haswarped)
        {
            // Set the animation
            if (direction != 0)
            {
                direction = 0;
                SetAnimation();
            }
            // Check if auto warp if so teleport the player
            if (currentPoint.autoWarp && !currentPoint.isLocked)
                StartCoroutine(TeleportPlayer(teleportTime));
        }

        // Check if is a warp point & is being used like a corner
        if (currentPoint.isCorner && currentPoint.isWarpPoint)
        {
            // Set the animation
            if (direction != 0)
            {
                direction = 0;
                SetAnimation();
            }

            // Check for player input 
            CheckInput();
        }

        // Check if map point is a corner move the player to the next map point
        if (currentPoint.isCorner)
        {
            // Maybe have a bool to pause the check to give you time to play an animation
            AutoMove();
        }
        // If not map point check for player input
        else
        {
            // Set the animation
            if (direction != 0)
            {
                direction = 0;
                SetAnimation();
            }

            // Check for player input 
            CheckInput();
        }
    }

    void SetAnimation()
    {
        // Set the animation bool
        animationSet = true;

        // Set the correct animation depending on the map point direction
        switch (direction)
        {
            case 0:
                // Set the animation
                animator.Play("Idle");
                break;
            case 1:
                // Set the animation
                animator.Play("Up");
                break;
            case 2:
                // Set the animation
                animator.Play("Right");
                break;
            case 3:
                // Set the animation
                animator.Play("Down");
                break;
            case 4:
                // Set the animation
                animator.Play("Left");
                break;
        }
    }

    void SetNextPoint(MapPoint nextPoint)
    {
        // Reset the player sprite to stop any drift
        playerSprite.localPosition = Vector2.zero;
        // Set the previous point to the current point
        prevPoint = currentPoint;
        // Set the current point to the next map point
        currentPoint = nextPoint;
    }

    void SetPlayerPos()
    {
        // Load players starting position from your own save system, i will use the default start position set in the inspector
        if (DataManager.instance.gameData.currentLevelName == "")
        {
            // Set the starting position
            transform.position = startPoint.transform.position;
            // Enable the sprite renderer
            spriteRenderer.enabled = true;
            // Set the current point
            currentPoint = startPoint;
            // Set the previous point to the current point
            prevPoint = currentPoint;
            // Enable player movement
            canMove = true;
        }
        else
        {
            foreach (MapPoint point in allPoints)
            {
                if (point.isLevel)
                {
                    if (point.sceneToLoad == DataManager.instance.gameData.currentLevelName)
                    {
                        // Set the starting position
                        transform.position = point.transform.position;
                        // Enable the sprite renderer
                        spriteRenderer.enabled = true;
                        // Set the current point
                        currentPoint = point;
                        // Set the previous point to the current point
                        prevPoint = currentPoint;
                        // Enable player movement
                        canMove = true;
                    }
                }
            }
        }
    }

    public void GetMovement(InputAction.CallbackContext context)
    {
        // Get the movement vector
        movement = context.ReadValue<Vector2>();
        // Get the x & y input values
        x = movement.x;
        y = movement.y;
    }

    public void SelectLevel(InputAction.CallbackContext context)
    {
        // Check for input from the chosen button
        if (context.performed)
        {
            // Check if the current point is not null
            if (currentPoint != null)
            {
                // Check if a level & is locked or not else check if its a warp point & does it use auto warp
                if (!currentPoint.isLocked && currentPoint.isLevel)
                {
                    // Load your scene how ever you like if you have a scene loader
                    DataManager.instance.gameData.currentLevelName = currentPoint.sceneToLoad;
                    DataManager.instance.SaveGameData();

                    //Load the level
                    SceneManager.LoadScene(currentPoint.sceneToLoad);
                }
                else if (currentPoint.isWarpPoint && !currentPoint.autoWarp && !currentPoint.isLocked)
                {
                    // Do what ever you want regarding teleport animtion or whatever here

                    // Teleport the player
                    StartCoroutine(TeleportPlayer(teleportTime));
                }
            }
        }
    }

    IEnumerator TeleportPlayer(float time)
    {
        // Set the has warped bool to stop ping ponging between warp points
        currentPoint.haswarped = true;

        // Stop player movement
        canMove = false;

        // Make the player invisable
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(1, 0, t));
            spriteRenderer.color = newColor;
            yield return null;
        }

        // Teleport the player
        transform.position = currentPoint.warpPoint.transform.position;

        // Wait for the teleport time to pass
        yield return new WaitForSeconds(time);

        // Make the player visable
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(0, 1, t));
            spriteRenderer.color = newColor;
            yield return null;
        }

        // Set the current point
        currentPoint = currentPoint.warpPoint;

        // Set the has warped bool to stop ping ponging between warp points
        currentPoint.haswarped = true;

        // Start player movement
        canMove = true;

    }
    #endregion
}
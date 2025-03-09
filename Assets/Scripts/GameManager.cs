using UnityEngine;

enum GameState
{
    BUILDING,
    PLAYING
}

public class GameManager : MonoBehaviour
{
    public GameObject gridBuildGO;
    public GameObject gridPlayGO;

    public GameObject munchSpawnerGO;
    public GameObject placedBlocksPlayGO;
    public GameObject placedBlocksBuildGO;

    private GameState gameState = GameState.BUILDING;
    private bool stateChanged = false;

    private GameObject startGO;
    private GameObject finishGO;

    void Start() {
        // Set initial state to building
        gridBuildGO.SetActive(true);
        gridPlayGO.SetActive(false);

        // Store start and finish
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.name == "Start")
            {
                startGO = child.gameObject;
            }
            else if (child.name == "Finish")
            {
                finishGO = child.gameObject;
            }
        }

        // Set Munch start position
        munchSpawnerGO.transform.position = startGO.transform.position;
    }

    void Update()
    {
        if (!stateChanged)
        {
            return;
        }

        SpawnMunch munchSpawnerScript = munchSpawnerGO.GetComponent<SpawnMunch>();
        if (gameState == GameState.BUILDING)
        {
            // Remove all spawned munches
            for (int i = 0; i < munchSpawnerGO.transform.childCount; i++)
            {
                Destroy(munchSpawnerGO.transform.GetChild(i).gameObject);
            }

            // Remove all placed blocks
            for (int i = 0; i < placedBlocksPlayGO.transform.childCount; i++)
            {
                Destroy(placedBlocksPlayGO.transform.GetChild(i).gameObject);
            }

            // Stop spawning munches
            munchSpawnerScript.stopSpawn();

            // Flip flag states
            bool playMode = false;
            swapFlagStates(playMode);

            // Update grid visibility
            gridBuildGO.SetActive(true);
            gridPlayGO.SetActive(false);
        }
        else
        {
            // Update grid visibility
            gridBuildGO.SetActive(false);
            gridPlayGO.SetActive(true);

            // Copy all placed blocks to the play grid
            for (int i = 0; i < placedBlocksBuildGO.transform.childCount; i++)
            {
                Transform child = placedBlocksBuildGO.transform.GetChild(i);
                GameObject newBlock = Instantiate(child.gameObject, placedBlocksPlayGO.transform);
            }

            // Flip flag states
            bool playMode = true;
            swapFlagStates(playMode);

            // Start spawning munches
            munchSpawnerScript.startSpawn();
        }
        stateChanged = false;
    }

    private void swapFlagStates(bool playMode)
    {
        for (int i = 0; i < startGO.transform.childCount; i++)
        {
            Transform child = startGO.transform.GetChild(i);
            if (child.name == "StartPlay")
            {
                child.gameObject.SetActive(playMode);
            }
            else if (child.name == "StartBuild")
            {
                child.gameObject.SetActive(!playMode);
            }
        }
        for (int i = 0; i < finishGO.transform.childCount; i++)
        {
            Transform child = finishGO.transform.GetChild(i);
            if (child.name == "FinishPlay")
            {
                child.gameObject.SetActive(playMode);
            }
            else if (child.name == "FinishBuild")
            {
                child.gameObject.SetActive(!playMode);
            }
        }
    }

    public void onPlayPressed()
    {
        if (gameState == GameState.PLAYING)
        {
            return;
        }
        gameState = GameState.PLAYING;
        stateChanged = true;
    }

    public void onRestartPressed()
    {
        if (gameState == GameState.BUILDING)
        {
            return;
        }
        gameState = GameState.BUILDING;
        stateChanged = true;
    }

    public void onEndTriggerEntered(GameObject collided)
    {
        collided.GetComponent<MunchMovementV2>().StopMovement();
    }
}

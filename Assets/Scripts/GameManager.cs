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

    void Start() {
        gridBuildGO.SetActive(true);
        gridPlayGO.SetActive(false);
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

            // Start spawning munches
            munchSpawnerScript.startSpawn();
        }
        stateChanged = false;
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
}

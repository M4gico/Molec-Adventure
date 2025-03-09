using UnityEngine;

enum GameState
{
    BUILDING,
    PLAYING
}

public class GameManager : MonoBehaviour
{
    public GameObject gridBuild;
    public GameObject gridPlay;

    public GameObject munchSpawner;

    private GameState gameState = GameState.BUILDING;
    private bool stateChanged = false;

    void Start() {
        gridBuild.SetActive(true);
        gridPlay.SetActive(false);
    }

    void Update()
    {
        if (!stateChanged)
        {
            return;
        }

        SpawnMunch munchSpawnerScript = munchSpawner.GetComponent<SpawnMunch>();
        if (gameState == GameState.BUILDING)
        {
            // Remove all spawned munches
            for (int i = 0; i < munchSpawner.transform.childCount; i++)
            {
                Destroy(munchSpawner.transform.GetChild(i).gameObject);
            }

            // Stop spawning munches
            munchSpawnerScript.stopSpawn();

            // Update grid visibility
            gridBuild.SetActive(true);
            gridPlay.SetActive(false);
        }
        else
        {
            // Update grid visibility
            gridBuild.SetActive(false);
            gridPlay.SetActive(true);

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

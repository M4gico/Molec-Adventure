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
            gridBuild.SetActive(true);
            gridPlay.SetActive(false);
            munchSpawnerScript.stopSpawn();
        }
        else
        {
            gridBuild.SetActive(false);
            gridPlay.SetActive(true);
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

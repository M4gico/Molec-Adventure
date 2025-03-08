using System.Collections;
using UnityEngine;

public class SpawnMunch : MonoBehaviour
{
    [SerializeField] private GameObject munchPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private int numberSpawn;

    private void Start()
    {
        startSpawn();
    }

    private IEnumerator SpawnEntities()
    {
        for(int i = 0; i < numberSpawn; i++)
        {
            Instantiate(munchPrefab, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void startSpawn()
    {
        StartCoroutine(SpawnEntities());
    }

    public void stopSpawn()
    {
        StopAllCoroutines();
    }
}

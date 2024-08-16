using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public bool spawnOnAwake;
    public bool spawnOnTrigger;
    private bool wasTriggered = false;
    [SerializeField]
    public Transform droppedItemParent; 
    //spawn item parent not working//
    
    public GameObject[] spawnableObjects;
    public int spawnCount = 10;
    public float spawnRadius = 10f;
    public float spawnHeight = 0.5f;

    private void Awake()
    {
        if (spawnOnAwake)
        {
            SpawnItems();
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player" && spawnOnTrigger && !wasTriggered)
        {
            SpawnItems();
            wasTriggered = true;
        }
    }

    public void SpawnItems()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, spawnableObjects.Length);
            Vector3 randownSpawnRange = new Vector3(Random.Range(-spawnRadius, spawnRadius), spawnHeight, Random.Range(-spawnRadius, spawnRadius));
            Vector3 randomSpawnPosition = transform.position + randownSpawnRange;
            Instantiate(spawnableObjects[randomIndex], randomSpawnPosition, Quaternion.identity, droppedItemParent);
            
        }
    }

}

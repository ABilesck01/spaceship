using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private SpaceObject[] objectsToSpawn;

    private void OnEnable()
    {
        GameManager.OnTick += GameManager_OnTick;
        CampaingManager.OnTick += GameManager_OnTick;
    }

    private void OnDestroy()
    {
        GameManager.OnTick -= GameManager_OnTick;
        CampaingManager.OnTick -= GameManager_OnTick;
    }

    private void GameManager_OnTick(object sender, System.EventArgs e)
    {
        SpawnObject();
    }

    private void SpawnObject()
    {
        if (Random.value < 0.7) return;

        int rand = Random.Range(0, objectsToSpawn.Length);
        Instantiate(objectsToSpawn[rand], spawnPosition.position, transform.rotation);
    }
}

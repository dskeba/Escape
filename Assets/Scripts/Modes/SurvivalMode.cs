using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class SurvivalMode : MonoBehaviour
{
    private Transform _playerTransform;
    private List<GameObject> gunPrefabs;
    private List<GameObject> ammoPrefabs;
    private List<GameObject> consumablePrefabs;

    void Start()
    {
        LoadPrefabs();
        SpawnGameObjects();
        SetupCamera();
    }

    void LoadPrefabs()
    {
        gunPrefabs = new List<GameObject>();
        gunPrefabs.Add(Resources.Load<GameObject>("Prefabs/AssaultRifle"));
        gunPrefabs.Add(Resources.Load<GameObject>("Prefabs/Shotgun"));
        gunPrefabs.Add(Resources.Load<GameObject>("Prefabs/Pistol"));

        ammoPrefabs = new List<GameObject>();
        ammoPrefabs.Add(Resources.Load<GameObject>("Prefabs/PistolAmmo"));

        consumablePrefabs = new List<GameObject>();
        consumablePrefabs.Add(Resources.Load<GameObject>("Prefabs/Medkit"));
        consumablePrefabs.Add(Resources.Load<GameObject>("Prefabs/Boost"));
    }

    void SpawnGameObjects()
    {
        var playerSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPointPlayer");
        SpawnPlayer(playerSpawnPoint.transform.position);

        var zombieSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPointZombie");
        foreach (GameObject zombieSpawnPoint in zombieSpawnPoints)
        {
            SpawnZombie(zombieSpawnPoint.transform.position);
        }

        var gunSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPointGun");
        foreach (GameObject gunSpawnPoint in gunSpawnPoints)
        {
            SpawnRandomGun(gunSpawnPoint.transform.position);
        }

        var ammmoSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPointAmmo");
        foreach (GameObject ammmoSpawnPoint in ammmoSpawnPoints)
        {
            SpawnRandomAmmo(ammmoSpawnPoint.transform.position);
        }

        var consumableSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPointConsumable");
        foreach (GameObject consumableSpawnPoint in consumableSpawnPoints)
        {
            SpawnRandomConsumable(consumableSpawnPoint.transform.position);
        }
    }

    private void SetupCamera()
    {
        var thirdPersonCamera = GameObject.FindGameObjectWithTag("ThirdPersonCamera");
        var cm = thirdPersonCamera.GetComponent<CinemachineFreeLook>();
        cm.Follow = _playerTransform;
        cm.LookAt = GameObject.FindGameObjectWithTag("CameraFollowPoint").transform;
    }

    void SpawnPlayer(Vector3 position)
    {
        var prefab = Resources.Load<GameObject>("Prefabs/Player");
        var go = Instantiate(prefab, position, Quaternion.identity);
        _playerTransform = go.transform;
    }

    void SpawnZombie(Vector3 position)
    {
        var prefab = Resources.Load<GameObject>("Prefabs/Zombie");
        var go = Instantiate(prefab, position, Quaternion.identity);
        var zombie = go.GetComponent<Zombie>();
        zombie.player = _playerTransform;
        var zombieHealth = go.GetComponent<ZombieHealth>();
        zombieHealth.OnDie += ZombieHealth_OnDie;
    }

    private void ZombieHealth_OnDie(object sender, HealthEvent e)
    {
        var zombie = e.Health.gameObject.GetComponent<Zombie>();
        //zombie.gameObject.SetActive(false);
    }

    void SpawnRandomGun(Vector3 position)
    {
        int randNum = Random.Range(0, gunPrefabs.Count);
        var prefab = gunPrefabs[randNum];
        Instantiate(prefab, position, Quaternion.identity);
    }

    void SpawnRandomAmmo(Vector3 position)
    {
        int randNum = Random.Range(0, ammoPrefabs.Count);
        var prefab = ammoPrefabs[randNum];
        Instantiate(prefab, position, Quaternion.identity);
    }

    void SpawnRandomConsumable(Vector3 position)
    {
        int randNum = Random.Range(0, consumablePrefabs.Count);
        var prefab = consumablePrefabs[randNum];
        Instantiate(prefab, position, Quaternion.identity);
    }

}

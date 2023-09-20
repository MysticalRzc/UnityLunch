using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager sharedInstance = null;
    public RPGCameraManager cameraManager;
    public SpawnPoint playerSpawnPoint;

    public void SpawnPlayer()
    {
        if(playerSpawnPoint != null)
        {
            Debug.Log("spawn is running" + playerSpawnPoint.name);
            GameObject player = playerSpawnPoint.SpawnObject();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        Debug.Log("RPG game manager is running");
        SetupScene();
    }

    public void SetupScene()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

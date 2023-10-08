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
            GameObject playerRunning =  GameObject.FindGameObjectWithTag("player");
            if (playerRunning == null)
            {
                GameObject player = playerSpawnPoint.SpawnObject();
                cameraManager.virtualCamera.Follow = player.transform;
            }
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
        SetupScene();
    }

    public void SetupScene()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}

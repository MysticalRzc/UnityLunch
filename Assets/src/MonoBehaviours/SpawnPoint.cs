using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public GameObject prefabToSpawn;
    public float repeatInterval;
    
    void Start()
    {
        if(repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
    }

    public GameObject SpawnObject()
    {
        if(prefabToSpawn != null)
        {
            //Quaternion 表示全传，identity无旋转
            return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

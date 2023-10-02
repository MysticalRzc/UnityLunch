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
            InvokeRepeating("SpawnObject", 2.0f, repeatInterval);
        }
    }

    public GameObject SpawnObject()
    {
        if(prefabToSpawn != null)
        {
            //Quaternion ��ʾȫ����identity����ת
            return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    public int poolSize;
    //发射的子弹速度
    public float weaponVelocity;


    void Awake()
    {
        // object pool
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireAmmo();
        }
        //UpdateState();
    }
    //遍历对象池，找到第一个为活跃的对象
    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }

    void FireAmmo()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject ammo = SpawnAmmo(transform.position);
        if (ammo != null){
            Arc arcScript = ammo.GetComponent<Arc>();
            float travelDuration = 1.0f / weaponVelocity;
            StartCoroutine(arcScript.TravelArc(mousePosition, travelDuration));
        }
    }

    void OnDestroy()
    {
        ammoPool = null;
    }
}

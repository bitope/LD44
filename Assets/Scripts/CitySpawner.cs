using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<GameObject> spawned;
    public Transform spawnPoint;
    public int spawnCount;


    // Start is called before the first frame update
    void Start()
    {
        spawned = new List<GameObject>();
        for (int i = 0; i < spawnCount; i++)
        {
            var p = Random.onUnitSphere * 40f;
            var id = Random.Range(0, prefabs.Count);
            var go =Instantiate(prefabs[id], p, Quaternion.identity) as GameObject;
            spawned.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 1000 == 0)
        {
            spawned.RemoveAll(i => i == null);
        }
    }
}

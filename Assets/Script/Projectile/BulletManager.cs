using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;
    [System.Serializable] private struct BulletPrefabs 
    {
        public string tag;
        public GameObject prefab;
        public Transform parent;
    }
    [SerializeField] private BulletPrefabs[] pool;
    [SerializeField] private int startingAmount;
    private Dictionary<string, Queue<GameObject>> bullets;

    public Action<string, GameObject> QueueBullet;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    private void Start()
    {
        bullets = new Dictionary<string, Queue<GameObject>>();
        foreach (BulletPrefabs bs in pool)
        {
            Queue<GameObject> entities = new Queue<GameObject>();
            for (int i = 0; i < startingAmount; i++)
            {
                GameObject cache = Instantiate(bs.prefab, bs.parent);
                cache.GetComponent<Bullet>().tagBullet = bs.tag;
                cache.SetActive(false);
                entities.Enqueue(cache);
            }
            bullets.Add(bs.tag, entities);
        }
        QueueBullet += (string tag, GameObject bullet) => bullets[tag].Enqueue(bullet);
    }
    private (GameObject, Transform) GetPrefab(string tag)
    {
        foreach (BulletPrefabs bs in pool)
            if (bs.tag == tag)
                return (bs.prefab, bs.parent);
        return (null, null);
    }
    public GameObject GetBullet(string tag) 
    {
        if (bullets.ContainsKey(tag))
        {
            if (bullets[tag].Count > 0)
                return bullets[tag].Dequeue();
            (GameObject, Transform) cache = GetPrefab(tag);
            if (cache.Item1)
            {
                GameObject cacheGO = Instantiate(cache.Item1, cache.Item2);
                cacheGO.GetComponent<Bullet>().tagBullet = tag;
                cacheGO.SetActive(false);
                bullets[tag].Enqueue(cacheGO);
            }
            return GetBullet(tag);
        }
        return null;
    }
}

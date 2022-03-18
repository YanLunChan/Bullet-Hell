using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public static Enemy_Manager instance;
    [System.Serializable]
    private struct EnemyPrefabs
    {
        public string tag;
        public GameObject prefab;
        public Transform parent;
    }
    [SerializeField] private EnemyPrefabs[] pool;
    [SerializeField] private int startingAmount;
    
    private Dictionary<string, List<GameObject>> mob;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        mob = new Dictionary<string, List<GameObject>>();
        foreach(EnemyPrefabs e in pool) 
        {
            List<GameObject> entities = new List<GameObject>();
            for(int i = 0; i < startingAmount; i++) 
            {
                GameObject cache = Instantiate(e.prefab, e.parent);
                cache.SetActive(false);
                entities.Add(cache);
                cache.GetComponent<Enemy>().parent = e.parent;
            }
            mob.Add(e.tag, entities);
        }
            
    }
    public GameObject GetEnemy(string tag) 
    {
        if (mob.ContainsKey(tag)) 
        {
            foreach(GameObject g in mob[tag]) 
            {
                if(g.activeInHierarchy == false) 
                {
                    return g;
                }
            }
            foreach(EnemyPrefabs e in pool) 
            {
                if (e.tag == tag)
                {
                    GameObject cache = Instantiate(e.prefab, e.parent);
                    cache.SetActive(false);
                    mob[tag].Add(cache);
                    return cache;
                }
            }
        }
        return null;
    }
}

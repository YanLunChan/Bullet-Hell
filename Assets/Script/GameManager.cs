using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject boss;
    [SerializeField] private Volley[] volley;
    //background setting(s)
    public float scrollSpeed = 2f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    private void Start()
    {
        StartCoroutine(Spawning());
    }
    //spawning enemy
    public IEnumerator Spawning() 
    {
        foreach (Volley v in volley) 
        {
            yield return new WaitForSeconds(v.delay);
            for (int i = 0; i < v.numVolley; i++) 
            {
                if (!(v.SpawnLocal.name == "BossSpawn"))
                {
                    GameObject cache = Enemy_Manager.instance.GetEnemy(v.tag);
                    //get settings and put them into enemy
                    Enemy settings = cache.GetComponent<Enemy>();
                    settings.setMoves(v.settings);
                    cache.SetActive(true);
                    settings.setPathing(v.pathing);
                    settings.setTarget(v.target);
                    cache.transform.parent = v.SpawnLocal;
                    yield return new WaitForSeconds(v.iteration);
                }
                else 
                {
                    Boss cache = Instantiate(boss, v.SpawnLocal).GetComponent<Boss>();
                    cache.setMoves(v.settings);
                }
            }
        }
        yield return null;
    }
}

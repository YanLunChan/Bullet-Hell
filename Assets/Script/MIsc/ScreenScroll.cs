using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScroll : MonoBehaviour
{
    GameManager manager;
    private float height;
    public float Height { get => height;}
    // Start is called before the first frame update
    void Start()
    {
        height = GetComponent<SpriteRenderer>().size.y;
        manager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, manager.scrollSpeed * Time.deltaTime, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    //screen stuff
    private Vector2 screenBounds;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void LateUpdate()
    {
        Vector2 pos;
        pos.x = Mathf.Clamp(transform.position.x, (screenBounds.x - sprite.size.x) * -0.5f, (screenBounds.x - sprite.size.x) * 0.5f);
        pos.y = Mathf.Clamp(transform.position.y, (screenBounds.y - sprite.size.y) * -1, (screenBounds.y - sprite.size.y) * 1);

        transform.position = pos;
    }
}

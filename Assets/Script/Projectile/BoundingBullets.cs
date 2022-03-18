using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBullets : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>())
            collision.GetComponent<Bullet>().TurnOff();
    }

}

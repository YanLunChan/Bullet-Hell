using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            float height = collision.gameObject.GetComponent<ScreenScroll>().Height;
            collision.transform.position = transform.parent.position + new Vector3(0, height, 0);
        }
    }
}

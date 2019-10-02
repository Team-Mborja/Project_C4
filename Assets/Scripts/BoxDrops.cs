using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDrops : MonoBehaviour
{
    public GameObject box;
    public GameObject itemDrop;

    public void DropItem()
    {
        Instantiate(itemDrop, new Vector2(box.transform.position.x, box.transform.position.y - 0.5f), Quaternion.identity);
    }
}

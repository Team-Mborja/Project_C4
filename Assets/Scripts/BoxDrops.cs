using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDrops : MonoBehaviour
{
    // The box that the item drops from
        public GameObject box;
    // The item that drops from the box
        public GameObject itemDrop;

    // Fucntion that drops the item from the boxes location when it is destroyed
    public void DropItem()
    {
        Instantiate(itemDrop, new Vector2(box.transform.position.x, box.transform.position.y - 0.25f), Quaternion.identity);
    }
}

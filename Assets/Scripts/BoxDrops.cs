using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDrops : MonoBehaviour
{
    public GameObject box;
    public GameObject itemDrop;

    public void DropItem()
    {
        Instantiate(itemDrop, box.transform.position, Quaternion.identity);
    }
}

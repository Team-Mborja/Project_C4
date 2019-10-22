using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4_Stick : MonoBehaviour
{
        public GameObject c4;
        public bool isStuck;
    // List of tags that the C4 can stick to
        public List<string> stickTags = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        isStuck = false;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {

        if (stickTags.Contains(collider.gameObject.tag))
        {        
            c4.GetComponent<C4>().rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isStuck = true;
        }
    }
}

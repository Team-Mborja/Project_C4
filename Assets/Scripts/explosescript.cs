using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class explosescript : MonoBehaviour
{
    //private Vector3 mousePos;
    public int dist;
    //private Vector3 explosionPos;
    //public Transform GameObject;
    private Vector3 exPos;
        //public float radius = 5.0F;
        //public float power = 10.0F;
    //public float upforce = 1.0F;
    Animator anim;
    public Explodable other;
    public Explodable other1;
    public Explodable other2;
    public Explodable other3;
    public Explodable other4;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

        void Explose()
        {
        //Debug.Log("did the thing");
        anim.SetTrigger("explosetrigger");
        other.explode();
        other1.explode();
        other2.explode();
        other3.explode();
        other4.explode();
        /*Vector3 explosionPos = exPos;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, upforce, ForceMode.Impulse);
        }*/
    }

    void Update()
    {
       Vector3 mousePos = Input.mousePosition;
        mousePos.z = dist;
        exPos = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.position = exPos;
        if (Input.GetMouseButtonDown(0))
        
        {
            //Debug.Log(Input.mousePosition);
            //Debug.Log(Camera.main.ScreenToWorldPoint(mousePos));
            Explose();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }
    }

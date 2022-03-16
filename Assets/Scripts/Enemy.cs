using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fill")
        {
            Destroy(this.gameObject);
            Debug.Log("col calisti");
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Fill")
        {
            Destroy(this.gameObject);
            Debug.Log("col calisti");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fill")
        {
            Destroy(this.gameObject);
            Debug.Log("trig calisti");
        }
        if (other.gameObject.tag == "Tail")
        {
            Destroy(this.gameObject);
            Debug.Log("trig calisti");
        }
    }
    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fill")
        {
            Destroy(this.gameObject);
            Debug.Log("trig calisti");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fill")
        {
            Destroy(this.gameObject);
            Debug.Log("trig calisti");
        }
    }*/
}

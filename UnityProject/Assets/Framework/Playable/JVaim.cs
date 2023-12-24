using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JVaim : MonoBehaviour
{
    public bool Finished = true;

    public bool vaimDead = false;

    Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "B1" || collision.gameObject.tag == "B2" || collision.gameObject.tag == "B3" || collision.gameObject.tag == "B4")
        {
            if (Finished)
            {
                Animator.SetTrigger("Death");
                vaimDead = true;
                Finished = false;
            }
            GameObject.Destroy(collision.gameObject);
        }
    }
}

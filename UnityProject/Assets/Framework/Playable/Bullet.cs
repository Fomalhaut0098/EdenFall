using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            StartCoroutine(Touch());
        }
    }

    private IEnumerator Touch()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.Destroy(gameObject);
    }
}

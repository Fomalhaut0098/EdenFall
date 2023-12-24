using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public float BeamTime = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Doctor")
        {
            GameManager.Instance.hp -= 1;
            GameObject.Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        StartCoroutine(Counter());
    }

    private IEnumerator Counter()
    {
        yield return new WaitForSeconds(BeamTime);
        GameObject.Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Br : MonoBehaviour
{
    //�յ� ���ִ� ȿ���� �ִ� �� ���� ����
    public float amplitude = 0.3f; // y�� �̵� ���� (����)
    public float frequency = 0.7f; // �������� ��

    public AudioSource AudioSource;
    public AudioClip Item;

    private Vector3 startPos; // �ʱ� ��ġ

    void Start()
    {
        startPos = transform.position; // �ʱ� ��ġ ����
    }

    void Update()
    {
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude; // ���� �Լ��� �̿��� y�� ��ġ ���

        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("System"))
        {
            if(collision.gameObject.name == "ObjectScanArea")
            {
                AudioSource.PlayOneShot(Item);
                GameManager.Instance.leftAmmo[0] += 10;
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Br : MonoBehaviour
{
    //둥둥 떠있는 효과를 주는 것 같은 변수
    public float amplitude = 0.3f; // y축 이동 범위 (높이)
    public float frequency = 0.7f; // 움직임의 빈도

    public AudioSource AudioSource;
    public AudioClip Item;

    private Vector3 startPos; // 초기 위치

    void Start()
    {
        startPos = transform.position; // 초기 위치 저장
    }

    void Update()
    {
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude; // 사인 함수를 이용한 y축 위치 계산

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

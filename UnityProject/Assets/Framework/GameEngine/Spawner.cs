using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int index = 0;

    public GameObject[] Prefebs;

    public Transform[] SpawnPoint;
    float timer;

    public int PositionIndex = 0;

    private void Awake()
    {
        SpawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)) 
        {
            Spawn(index, PositionIndex, 2);
        }
    }

    public void Spawn(int inIndex, int inPosition, int inAmount)
    {
        for (int i = 0; i < inAmount; i++)
        {
            GameObject Monster = Instantiate(Prefebs[inIndex], transform);
            Monster.transform.position = SpawnPoint[inPosition].position + new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), 0f);
        }
    }
}

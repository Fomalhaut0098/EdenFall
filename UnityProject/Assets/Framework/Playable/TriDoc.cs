using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriDoc : MonoBehaviour
{

    public GameObject GameObject;
    private Animator Animator;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public void Move(float target_X, float target_Y, float speed)
    {
        // newPosition으로 이동
        Vector2 targetPosition = new Vector2(target_X, target_Y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void Visible(bool visible)
    {
        GameObject.SetActive(visible);
    }

    public void Wiggle(int inWiggle)
    {
        Animator.SetInteger("Wiggle", inWiggle);
    }


}
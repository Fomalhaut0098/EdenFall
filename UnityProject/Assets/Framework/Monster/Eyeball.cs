using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    private int Level = 1;
    private bool guided = true;

    Rigidbody2D RigidBody;
    SpriteRenderer SpriteRenderer;
    Animator Animator;
    Transform Transform;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Timer());
    }

    private void FixedUpdate()
    {
        Vector2 Direction = target.position - RigidBody.position;
        Vector2 NextPosition = Direction.normalized * speed * Time.fixedDeltaTime;

        if (guided)
        {
            RigidBody.MovePosition(RigidBody.position + NextPosition);
            RigidBody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Doctor")
        {
            GameManager.Instance.hp -= Level;
            GameObject.Destroy(gameObject);
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(3.5f);
        guided = false;

        yield return new WaitForSeconds(1f);
        Animator.SetTrigger("Boom");
        Level = 2;

        yield return new WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }

    private void OnEnable()
    {
        target = GameManager.Instance.Doctor.GetComponent<Rigidbody2D>();
    }
}

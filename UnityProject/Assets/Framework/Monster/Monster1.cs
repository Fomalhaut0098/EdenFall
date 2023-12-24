using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : MonoBehaviour
{
    public float Speed = 3;
    public Rigidbody2D target;

    public float blinkDuration = 0.3f; // 깜빡이는 전체 시간
    public float blinkInterval = 0.1f; // 깜빡임 간격

    public float knockbackForce = 20f;
    private Color originalColor; // 원래 색상 저장

    public int hp = 2;

    Rigidbody2D RigidBody;
    SpriteRenderer SpriteRenderer;
    Animator Animator;

    public bool alive = true;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();

        Speed += Random.Range(0.0f, 0.5f);
        originalColor = SpriteRenderer.color; // 원래 색상 저장
    }

    private void FixedUpdate()
    {
        if (hp > 0)
        {
            Vector2 Direction = target.position - RigidBody.position;
            Vector2 NextPosition = Direction.normalized * Speed * Time.fixedDeltaTime;

            if (Direction.magnitude < 35)
            {
                RigidBody.MovePosition(RigidBody.position + NextPosition);
            }

            RigidBody.velocity = Vector2.zero;
        }
    }

    private void LateUpdate()
    {
        SpriteRenderer.flipX = target.position.x < RigidBody.position.x;
        if(hp <= 0)
        {
            if (alive)
            {
                Animator.SetTrigger("Dead");
                alive = false;
                StartCoroutine(Delete());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알에 맞은 경우


        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized; // 밀려나는 방향
            RigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // 밀려나는 힘 적용

            if (collision.gameObject.tag == "B1")
            {
                StartCoroutine(Blink());
                hp -= 1;
            }
        }

        if (collision.gameObject.name == "Doctor")
        {
            Attack();
        }

    }

    private void Attack()
    {
        Animator.SetTrigger("Attack");
        GameManager.Instance.hp -= 1;
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.7f);
        GameObject.Destroy(gameObject);
    }

    IEnumerator Blink()
    {
        float endTime = Time.time + blinkDuration;

        while (Time.time < endTime)
        {
            // 스프라이트 색상을 흰색으로 변경
            SpriteRenderer.color = SpriteRenderer.color == originalColor ? Color.black : originalColor;

            // 지정된 간격만큼 대기
            yield return new WaitForSeconds(blinkInterval);
        }

        // 깜빡임 종료 후 원래 색상으로 설정
        SpriteRenderer.color = originalColor;
    }

    private void OnEnable()
    {
        target = GameManager.Instance.Doctor.GetComponent<Rigidbody2D>();
    }
}

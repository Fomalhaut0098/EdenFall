using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster4 : MonoBehaviour
{
    public float Speed = 2;
    public Rigidbody2D target;

    public GameObject Eyeball;

    public float blinkDuration = 0.3f; // �����̴� ��ü �ð�
    public float blinkInterval = 0.1f; // ������ ����

    public float knockbackForce = 20f;
    private Color originalColor; // ���� ���� ����

    public int hp = 2;

    Rigidbody2D RigidBody;
    SpriteRenderer SpriteRenderer;
    Animator Animator;

    private bool alive = true;
    private bool AttackMode = true;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();

        Speed += Random.Range(0.0f, 0.5f);
        originalColor = SpriteRenderer.color; // ���� ���� ����
    }

    private void FixedUpdate()
    {
        if(hp > 0)
        {
            Vector2 Direction = target.position - RigidBody.position;
            Vector2 NextPosition = Direction.normalized * Speed * Time.fixedDeltaTime;

            if (Direction.magnitude < 55 && Direction.magnitude > 20)
            {
                RigidBody.MovePosition(RigidBody.position + NextPosition);
            }
            else if (Direction.magnitude <= 20)
            {
                if (AttackMode)
                {
                    StartCoroutine(Launch());
                    Animator.SetTrigger("Attack");
                    AttackMode = false;
                }
            }
            RigidBody.velocity = Vector2.zero;
        }
    }

    private void LateUpdate()
    {
        SpriteRenderer.flipX = target.position.x > RigidBody.position.x;
        if (hp <= 0)
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
        // �Ѿ˿� ���� ���
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized; // �з����� ����
            RigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // �з����� �� ����

            if (collision.gameObject.tag == "B4")
            {
                StartCoroutine(Blink());
                hp -= 1;
            }
        }
    }

    private IEnumerator Launch()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(Eyeball, RigidBody.position, transform.rotation);

        yield return new WaitForSeconds(5f);
        AttackMode = true;
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }

    IEnumerator Blink()
    {
        float endTime = Time.time + blinkDuration;

        while (Time.time < endTime)
        {
            // ��������Ʈ ������ ������� ����
            SpriteRenderer.color = SpriteRenderer.color == originalColor ? Color.black : originalColor;

            // ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(blinkInterval);
        }

        // ������ ���� �� ���� �������� ����
        SpriteRenderer.color = originalColor;
    }
    private void OnEnable()
    {
        target = GameManager.Instance.Doctor.GetComponent<Rigidbody2D>();
    }
}

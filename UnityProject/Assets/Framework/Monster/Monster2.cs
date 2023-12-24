using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster2 : MonoBehaviour
{
    public float Speed = 2f;
    public Rigidbody2D target;

    public float blinkDuration = 0.3f; // �����̴� ��ü �ð�
    public float blinkInterval = 0.1f; // ������ ����

    public float knockbackForce = 20f;
    private Color originalColor; // ���� ���� ����

    public int hp = 2;

    Rigidbody2D RigidBody;
    SpriteRenderer SpriteRenderer;
    Animator Animator;

    public GameObject Beam;

    private bool alive = true;
    private bool AttackMode = true;

    public float BeamSpeed = 40f;


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

            if (Direction.magnitude < 60 && Direction.magnitude > 50)
            {
                RigidBody.MovePosition(RigidBody.position + NextPosition);
            }
            else if (Direction.magnitude <= 50)
            {
                if (AttackMode)
                {
                    Animator.SetTrigger("Attack");
                    StartCoroutine(Launch());
                    AttackMode = false;
                }
            }

            RigidBody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ˿� ���� ���
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized; // �з����� ����
            RigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // �з����� �� ����

            if (collision.gameObject.tag == "B2")
            {
                StartCoroutine(Blink());
                hp -= 1;
            }
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

    private IEnumerator Launch()
    {
        GameObject inBeam = null;

        yield return new WaitForSeconds(0.5f);
        inBeam = Instantiate(Beam, RigidBody.position, transform.rotation);

        if (inBeam != null)
        {

            Rigidbody2D rb = inBeam.GetComponent<Rigidbody2D>();
            SpriteRenderer sr = inBeam.GetComponent<SpriteRenderer>();
            Vector2 shootingDirection = Vector2.right;

            if (SpriteRenderer.flipX)
            {
                // ���� �������� ����
                shootingDirection = transform.right;
                sr.flipX = true;
            }
            else
            {
                // ������ �������� ����
                shootingDirection = -transform.right;
                sr.flipX = false;
            }

            rb.velocity = shootingDirection * BeamSpeed;
        }

        yield return new WaitForSeconds(3f);
        AttackMode = true;
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

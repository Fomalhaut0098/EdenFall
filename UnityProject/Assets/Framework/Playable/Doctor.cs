using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// E�ڻ� - ������ ���ΰ�
// �䱸 ���� �ý��� | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class Doctor : MonoBehaviour
{
    //InputManager�� ����ϴ� ����
    public Vector2 IM_keyInputVect; //Ű���� �Է°�
    public float IM_mvSpeed = 10; //Doctor �ӵ�

    //���� ������ ������ ����
    public bool wp_enable = false;

    //���� ���� ��ũ��Ʈ ����
    public GameObject Weapon;
    private Weapon WeaponScript;

    private int BeforeHp;

    //Doctor �������� �� ���ϸ��̼� ����
    Rigidbody2D Rigidbody;
    SpriteRenderer SpriteRenderer;
    public Animator Animator;
    public AudioSource AudioSource; // �߼Ҹ��� ����� AudioSource ������Ʈ
    
    public AudioClip Walk;
    public AudioClip Run;

    public float blinkDuration = 0.3f; // �����̴� ��ü �ð�
    public float blinkInterval = 0.1f; // ������ ����

    public float knockbackForce = 80f;
    private Color originalColor; // ���� ���� ����

    void Start()
    {
        //Doctor �������� �� ���ϸ��̼� ����
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();

        //���� ���� ��ũ��Ʈ ����
        WeaponScript = Weapon.GetComponent<Weapon>();

        BeforeHp = GameManager.Instance.hp;
        originalColor = SpriteRenderer.color;
    }

    void Update()
    {
        //�Է°��� ���� �̵� �Լ�
        Vector2 tempVect = IM_keyInputVect.normalized * IM_mvSpeed * Time.fixedDeltaTime; //�����ӿ� ���� �ӵ� �̵�
        Rigidbody.MovePosition(Rigidbody.position + tempVect);

        //���� ������Ʈ ����
        Weapon.SetActive(wp_enable);

        //���ϸ��̼� ���� ����
        Animator.SetFloat("Speed", IM_keyInputVect.magnitude);
        Animator.SetBool("HoldWeapon", wp_enable);

        //Ű���� �Է¿� ���� ���� ������
        if (IM_keyInputVect.x < 0 && IM_keyInputVect.y == 0)
        {
            SpriteRenderer.flipX = true;
        }
        else
        {
            SpriteRenderer.flipX = false;
        }

        if (GameManager.Instance.hp != BeforeHp)
        {
            StartCoroutine(Blink());
            BeforeHp = GameManager.Instance.hp;
        }
    }

    private void LateUpdate()
    {
        if(IM_keyInputVect.magnitude > 0)
        {
            if(wp_enable)
            {
                if (!AudioSource.isPlaying)
                {
                    AudioSource.clip = Walk;
                    AudioSource.Play();
                }
            }
            else
            {
                if (!AudioSource.isPlaying)
                {
                    AudioSource.clip = Run;
                    AudioSource.Play();
                }
            }
        }
    }

    public void fireWeapon()
    {
        WeaponScript.fireWeapon();
    }

    public void Move(float target_X, float target_Y, float speed)
    {
        // newPosition���� �̵�
        Vector2 targetPosition = new Vector2(target_X, target_Y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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

    public void LayDown(bool inlayDown)
    {
        if (inlayDown)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

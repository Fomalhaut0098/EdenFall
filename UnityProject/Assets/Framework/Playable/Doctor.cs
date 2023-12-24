using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// E박사 - 게임의 주인공
// 요구 종속 시스템 | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class Doctor : MonoBehaviour
{
    //InputManager와 통신하는 변수
    public Vector2 IM_keyInputVect; //키보드 입력값
    public float IM_mvSpeed = 10; //Doctor 속도

    //무기 종류를 선택할 변수
    public bool wp_enable = false;

    //무기 하위 스크립트 종속
    public GameObject Weapon;
    private Weapon WeaponScript;

    private int BeforeHp;

    //Doctor 물리엔진 및 에니메이션 설정
    Rigidbody2D Rigidbody;
    SpriteRenderer SpriteRenderer;
    public Animator Animator;
    public AudioSource AudioSource; // 발소리를 재생할 AudioSource 컴포넌트
    
    public AudioClip Walk;
    public AudioClip Run;

    public float blinkDuration = 0.3f; // 깜빡이는 전체 시간
    public float blinkInterval = 0.1f; // 깜빡임 간격

    public float knockbackForce = 80f;
    private Color originalColor; // 원래 색상 저장

    void Start()
    {
        //Doctor 물리엔진 및 에니메이션 설정
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();

        //하위 종속 스크립트 설정
        WeaponScript = Weapon.GetComponent<Weapon>();

        BeforeHp = GameManager.Instance.hp;
        originalColor = SpriteRenderer.color;
    }

    void Update()
    {
        //입력값에 따른 이동 함수
        Vector2 tempVect = IM_keyInputVect.normalized * IM_mvSpeed * Time.fixedDeltaTime; //프레임에 맞춰 속도 이동
        Rigidbody.MovePosition(Rigidbody.position + tempVect);

        //무기 오브젝트 설정
        Weapon.SetActive(wp_enable);

        //에니메이션 변수 설정
        Animator.SetFloat("Speed", IM_keyInputVect.magnitude);
        Animator.SetBool("HoldWeapon", wp_enable);

        //키보드 입력에 따라 방향 뒤집기
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
        // newPosition으로 이동
        Vector2 targetPosition = new Vector2(target_X, target_Y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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

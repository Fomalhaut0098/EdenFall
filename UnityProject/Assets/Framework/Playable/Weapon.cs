using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// E박사가 들고있는 무기
// 요구 종속 오브젝트 | Doctor

public class Weapon : MonoBehaviour
{
    //Doctor의 위치에 따라 움직일 무기 위치 오프셋
    public Vector2 horizonOffset = new Vector2 (1.8f, -0.5f);
    public Vector2 verticalOffset = new Vector2(1.8f, 0.5f);
    public Vector3 stayOffset = new Vector3 (1.0f, -1.6f, -30f);

    //오디오 시스템 불러오기
    public AudioSource AudioSource;
    public AudioClip Fire;

    //무기 및 총알 프리펩 로딩
    public GameObject Bullet1;
    public GameObject Bullet2;
    public GameObject Bullet3;
    public GameObject Bullet4;

    // 총알 속도
    public float bulletSpeed = 50f; 

    //종속된 Doctor 오브젝트 불러오기
    public GameObject Doctor;
    private Doctor DoctorScript;

    //자체 스프라이트 렌더러 불러오기
    SpriteRenderer SpriteRenderer;

    
    void Start()
    {
        //종속된 오브젝트 및 시스템 불러오기
        DoctorScript = Doctor.GetComponent<Doctor>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Doctor가 움직이는 중이라면
        if (DoctorScript.IM_keyInputVect.magnitude > 0) 
        {
            // Doctor의 움직이는 방향에 따라 무기의 위치와 방향을 변환
            // 무기의 위치를 플레이어 위치에 offset을 더해서 갱신
            if (DoctorScript.IM_keyInputVect.y > 0)
            {
                transform.position = Doctor.transform.position + new Vector3(verticalOffset.x, verticalOffset.y, 0);
                transform.rotation = Quaternion.Euler(0, 0, 70);
                SpriteRenderer.flipX = false;
            }
            else if (DoctorScript.IM_keyInputVect.x > 0 || DoctorScript.IM_keyInputVect.y < 0)
            {
                transform.position = Doctor.transform.position + new Vector3(horizonOffset.x, horizonOffset.y, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                SpriteRenderer.flipX = false;
            }
            else if (DoctorScript.IM_keyInputVect.x < 0)
            {
                transform.position = Doctor.transform.position + new Vector3(horizonOffset.x * (-1), horizonOffset.y, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                SpriteRenderer.flipX = true;
            }
        }

        // Doctor가 움직이지 않고 가만히 있는 경우
        else
        {
            transform.position = Doctor.transform.position + new Vector3(stayOffset.x, stayOffset.y, 0);
            transform.rotation = Quaternion.Euler(0, 0, stayOffset.z);
            SpriteRenderer.flipX = false;
        }
    }

    public void fireWeapon()
    {
        GameObject bullet = null;

        switch (GameManager.Instance.weaponType)
        {
            
            case 1:
                if (GameManager.Instance.leftAmmo[0] > 0)
                {
                    bullet = Instantiate(Bullet1, transform.position, transform.rotation);
                    GameManager.Instance.leftAmmo[0] -= 1;
                }
                break;
            case 2:
                if (GameManager.Instance.leftAmmo[1] > 0)
                {
                    bullet = Instantiate(Bullet2, transform.position, transform.rotation);
                    GameManager.Instance.leftAmmo[1] -= 1;
                }
                break;
            case 3:
                if (GameManager.Instance.leftAmmo[2] > 0)
                {
                    bullet = Instantiate(Bullet3, transform.position, transform.rotation);
                    GameManager.Instance.leftAmmo[2] -= 1;
                }
                break;
            case 4:
                if (GameManager.Instance.leftAmmo[3] > 0)
                {
                    bullet = Instantiate(Bullet4, transform.position, transform.rotation);
                    GameManager.Instance.leftAmmo[3] -= 1;
                }
                break;
        }

        AudioSource.PlayOneShot(Fire);

        if (bullet != null)
        {
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 shootingDirection = Vector2.right;

            // 무기가 왼쪽으로 향하는 경우
            if (SpriteRenderer.flipX) 
            {
                // 왼쪽 방향으로 설정
                shootingDirection = -transform.right; 
            }
            //오른쪽이거나 위쪽인 경우
            else
            {
                // 오른쪽 방향으로 설정
                shootingDirection = transform.right; 
            }

            rb.velocity = shootingDirection * bulletSpeed;
        }
    }
}

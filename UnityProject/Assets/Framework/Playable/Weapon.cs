using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// E�ڻ簡 ����ִ� ����
// �䱸 ���� ������Ʈ | Doctor

public class Weapon : MonoBehaviour
{
    //Doctor�� ��ġ�� ���� ������ ���� ��ġ ������
    public Vector2 horizonOffset = new Vector2 (1.8f, -0.5f);
    public Vector2 verticalOffset = new Vector2(1.8f, 0.5f);
    public Vector3 stayOffset = new Vector3 (1.0f, -1.6f, -30f);

    //����� �ý��� �ҷ�����
    public AudioSource AudioSource;
    public AudioClip Fire;

    //���� �� �Ѿ� ������ �ε�
    public GameObject Bullet1;
    public GameObject Bullet2;
    public GameObject Bullet3;
    public GameObject Bullet4;

    // �Ѿ� �ӵ�
    public float bulletSpeed = 50f; 

    //���ӵ� Doctor ������Ʈ �ҷ�����
    public GameObject Doctor;
    private Doctor DoctorScript;

    //��ü ��������Ʈ ������ �ҷ�����
    SpriteRenderer SpriteRenderer;

    
    void Start()
    {
        //���ӵ� ������Ʈ �� �ý��� �ҷ�����
        DoctorScript = Doctor.GetComponent<Doctor>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Doctor�� �����̴� ���̶��
        if (DoctorScript.IM_keyInputVect.magnitude > 0) 
        {
            // Doctor�� �����̴� ���⿡ ���� ������ ��ġ�� ������ ��ȯ
            // ������ ��ġ�� �÷��̾� ��ġ�� offset�� ���ؼ� ����
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

        // Doctor�� �������� �ʰ� ������ �ִ� ���
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

            // ���Ⱑ �������� ���ϴ� ���
            if (SpriteRenderer.flipX) 
            {
                // ���� �������� ����
                shootingDirection = -transform.right; 
            }
            //�������̰ų� ������ ���
            else
            {
                // ������ �������� ����
                shootingDirection = transform.right; 
            }

            rb.velocity = shootingDirection * bulletSpeed;
        }
    }
}

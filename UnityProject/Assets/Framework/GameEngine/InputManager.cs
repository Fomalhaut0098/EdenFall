using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// ��� Ű���� �Է��� �����ϴ� �ý��� ��ũ��Ʈ
// �䱸 ���Ӽ� | Doctor | UI_Info | ObjectScanArea | InteractiveManager

public class InputManager : MonoBehaviour
{
    //���� ���� �ֻ��� ����, �̰� �ٲٸ� ���� ��ü�� ����
    // ===============================================================
    public bool dr_movable = false; //���Ͱ� ������ �� �ִ°�?
    public bool dr_weapon = false; //���Ͱ� ���⸦ ����ִ°�?
    public bool iv_active = false; //�κ��丮�� �������ִ°�?
    // ===============================================================

    //WASD Ű���� �Է°��� ������ ����
    public Vector2 KeyInputVect;
    public float triggerValue;

    //���� ���� ������Ʈ �ҷ�����
    public GameObject Doctor;
    public GameObject UI_Info;
    public GameObject UI_Interaction;
    public GameObject ObjectScanArea;
    public GameObject InteractiveManager;

    private Doctor DoctorScript;
    private UI_Info UI_InfoScript;
    private InteractiveManager InteractiveManagerScript;

    private bool isTriggerPressed = false;

    private void Start()
    {
        DoctorScript = Doctor.GetComponent<Doctor>();
        UI_InfoScript = UI_Info.GetComponent<UI_Info>();
        InteractiveManagerScript = InteractiveManager.GetComponent<InteractiveManager>();
    }

    private void FixedUpdate()
    {
        KeyInputVect.x = Input.GetAxisRaw("Horizontal"); //�������� ���� ��
        KeyInputVect.y = Input.GetAxisRaw("Vertical");

        triggerValue = Input.GetAxisRaw("TriggerFire");
    }

    private void Update()
    {
        DoctorScript.wp_enable = dr_weapon;
        ObjectScanArea.SetActive(dr_movable);

        if(GameManager.Instance.Battery > 0 && dr_movable)
        {
            UI_Info.SetActive(true);
        }
        else
        {
            UI_Info.SetActive(false);
        }
        

        if (dr_movable)
        {
            //WASD Ű�� �Է¹��� �̵� ������ ���Ͱ����� ��ȯ
            DoctorScript.IM_keyInputVect = KeyInputVect;

            //����Ʈ�� ���� - E
            if (Input.GetButtonDown("Phone"))
            {
                if (iv_active)
                {
                    iv_active = false;
                }
                else
                {
                    iv_active = true;
                }
                UI_InfoScript.UI_Active(iv_active);
            }

            //���� ���� �� ���� - Q 
            if (Input.GetButtonDown("Weapon"))
            {
                if (dr_weapon == false)
                {
                    dr_weapon = true;
                    DoctorScript.IM_mvSpeed = 8f;
                }
                else
                {
                    dr_weapon = false;
                    DoctorScript.IM_mvSpeed = 15f;
                }
                
            }

            //���� ����
            if (Input.GetButtonDown("Change"))
            {
                GameManager.Instance.weaponType += 1;
                if (GameManager.Instance.weaponType > 4) 
                {
                    GameManager.Instance.weaponType = 1;
                }
            }


            //���� �߻�
            if (Input.GetButtonDown("Fire"))
            {
                if(DoctorScript.wp_enable)
                {
                    DoctorScript.fireWeapon();
                }
            }
            else if (triggerValue > 0.5f && !isTriggerPressed)
            {
                isTriggerPressed = true;
                if (DoctorScript.wp_enable)
                {
                    DoctorScript.fireWeapon();
                }
            }

            //������ �� ������Ʈ�� ��ȣ�ۿ� - R
            if (Input.GetButtonDown("Next"))
            {
                if(GameManager.Instance.TriggeredEvent != "")
                {
                    InteractiveManagerScript.Interact();
                    GameObject.Destroy(GameObject.Find(GameManager.Instance.TriggeredEvent));
                }
            }

            if (triggerValue <= 0.5f && isTriggerPressed)
            {
                isTriggerPressed = false;
                // Ʈ���Ÿ� ������ �� ������ �߰����� ������ �ִٸ� ���⿡ �ۼ�
            }
        }
    }
}

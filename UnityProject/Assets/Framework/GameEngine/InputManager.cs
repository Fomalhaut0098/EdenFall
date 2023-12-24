using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// 모든 키보드 입력을 제어하는 시스템 스크립트
// 요구 종속성 | Doctor | UI_Info | ObjectScanArea | InteractiveManager

public class InputManager : MonoBehaviour
{
    //게임 진행 최상위 변수, 이걸 바꾸면 게임 전체에 영향
    // ===============================================================
    public bool dr_movable = false; //닥터가 움직일 수 있는가?
    public bool dr_weapon = false; //닥터가 무기를 들고있는가?
    public bool iv_active = false; //인벤토리가 펼쳐져있는가?
    // ===============================================================

    //WASD 키보드 입력값을 저장할 백터
    public Vector2 KeyInputVect;
    public float triggerValue;

    //하위 종속 오브젝트 불러오기
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
        KeyInputVect.x = Input.GetAxisRaw("Horizontal"); //보정되지 않은 값
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
            //WASD 키로 입력받은 이동 변수를 벡터값으로 변환
            DoctorScript.IM_keyInputVect = KeyInputVect;

            //스마트폰 열기 - E
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

            //무기 장착 및 해제 - Q 
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

            //무기 변경
            if (Input.GetButtonDown("Change"))
            {
                GameManager.Instance.weaponType += 1;
                if (GameManager.Instance.weaponType > 4) 
                {
                    GameManager.Instance.weaponType = 1;
                }
            }


            //무기 발사
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

            //아이템 및 오브젝트와 상호작용 - R
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
                // 트리거를 떼었을 때 실행할 추가적인 로직이 있다면 여기에 작성
            }
        }
    }
}

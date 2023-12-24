using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class DeusExMachina : MonoBehaviour
{
    //======================================================
    //개발자 모드
    public Vector3 DevTriDocMove;
    public int DevTriDocWiggle = 0;
    public bool DevTriDocVisible = true;
    public bool DevDoctorMovable = false;
    public bool DevDoctorLayDown = false;
    public bool DevFanceVisible = true;
    public bool DevWeaponActivate = true;
    public int DevWeaponType = 0;

    public bool GameMode = true;

    //======================================================
    //외부에서 스프라이트 참조
    public GameObject Doctor;
    public GameObject TriDoc;
    public GameObject Fance;
    public GameObject Weapon;

    //외부 스트라이트 안 코드 참조
    private Doctor DoctorScript;
    private TriDoc TriDocScript;
    private Fance FanceScript;
    private Weapon WeaponScript;
    //======================================================

    void Start()
    {
        //외부 변수를 내부적으로 참조
        DoctorScript = Doctor.GetComponent<Doctor>();
        TriDocScript = TriDoc.GetComponent<TriDoc>();
        FanceScript = Fance.GetComponent<Fance>();
        WeaponScript = Weapon.GetComponent<Weapon>();
    }

    void Update()
    {
        Dev(); //개발자 모드

        //GameEngine();
    }

    void Dev() //개발자 모드
    {

        DoctorScript.LayDown(DevDoctorLayDown); //Doctor 눕기/일어나기

        TriDocScript.Move(DevTriDocMove.x, DevTriDocMove.y, DevTriDocMove.z); //TriDoc 원하는 위치로 이동
        TriDocScript.Visible(DevTriDocVisible); //TriDoc 보이기/숨기기
        TriDocScript.Wiggle(DevTriDocWiggle); //TriDoc 에니메이션 조절 0, 1, 2, 3

        
        FanceScript.Visible(DevFanceVisible);
    }

    void GameEngine()
    {
        DoctorScript.mv_movable = GameMode;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class DeusExMachina : MonoBehaviour
{
    //======================================================
    //������ ���
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
    //�ܺο��� ��������Ʈ ����
    public GameObject Doctor;
    public GameObject TriDoc;
    public GameObject Fance;
    public GameObject Weapon;

    //�ܺ� ��Ʈ����Ʈ �� �ڵ� ����
    private Doctor DoctorScript;
    private TriDoc TriDocScript;
    private Fance FanceScript;
    private Weapon WeaponScript;
    //======================================================

    void Start()
    {
        //�ܺ� ������ ���������� ����
        DoctorScript = Doctor.GetComponent<Doctor>();
        TriDocScript = TriDoc.GetComponent<TriDoc>();
        FanceScript = Fance.GetComponent<Fance>();
        WeaponScript = Weapon.GetComponent<Weapon>();
    }

    void Update()
    {
        Dev(); //������ ���

        //GameEngine();
    }

    void Dev() //������ ���
    {

        DoctorScript.LayDown(DevDoctorLayDown); //Doctor ����/�Ͼ��

        TriDocScript.Move(DevTriDocMove.x, DevTriDocMove.y, DevTriDocMove.z); //TriDoc ���ϴ� ��ġ�� �̵�
        TriDocScript.Visible(DevTriDocVisible); //TriDoc ���̱�/�����
        TriDocScript.Wiggle(DevTriDocWiggle); //TriDoc ���ϸ��̼� ���� 0, 1, 2, 3

        
        FanceScript.Visible(DevFanceVisible);
    }

    void GameEngine()
    {
        DoctorScript.mv_movable = GameMode;
    }
}

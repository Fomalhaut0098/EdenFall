using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

// ���丮 ���� (T1) - ��� ��ũ��Ʈ �� ���� ���� �� ����
// Doctor�� ���� ������Ʈ�� �����Ǿ��־�� ��
// �䱸 ���� �ý��� | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMB1 : MonoBehaviour
{
    //�⺻ ���� ��ũ��Ʈ ����
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;

    //�����ι� ����
    public GameObject Doctor;

    public Text Name;

    private TypeEffect EffectScript;
    private InputManager InputManagerScript;

    //���ΰ� ����
    private Doctor DoctorScript;

    private bool Exitable = true;

    //����Į������ ����
    public float accelerationTime = 2.0f; // ���ӿ� �ɸ��� �ð�
    public float constantSpeedTime = 2.0f; // ��� � �ð�
    public float decelerationTime = 2.0f; // ���ӿ� �ɸ��� �ð�
    public float maxSpeed = 10.0f; // �ִ� �ӵ�

    void Start()
    {
        //���� ��ũ��Ʈ �ʱ�ȭ
        EffectScript = TypeEffect.GetComponent<TypeEffect>();
        InputManagerScript = InputManager.GetComponent<InputManager>();

        //�����ι� ��ũ��Ʈ �ʱ�ȭ
        DoctorScript = Doctor.GetComponent<Doctor>();
    }

    // Edge�� ���� ���丮 ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Edge"))
        {
            Debug.Log($"Edge Ʈ���� ������: {collision.gameObject.name}");

            // �� Edge ���� �� �Ʒ��� ���丮 ����
            switch (collision.gameObject.name)
            {
                case "E1": StartCoroutine(E1()); break;
                case "E2": StartCoroutine(E2()); break;
                case "E3": StartCoroutine(E3()); break;
                case "E4": StartCoroutine(E4()); break; 
                case "E5": StartCoroutine(E5()); break;     
                case "E6": StartCoroutine(E6()); break;
                case "M1":
                    Spawn(1, 1, 1);
                    Spawn(0, 2, 2); Spawn(1, 2, 1);
                    break;
                case "M2":
                    Spawn(0, 3, 3); Spawn(1, 3, 1);
                    break;
                case "M3":
                    Spawn(0, 4, 2); Spawn(1, 4, 1);
                    break;
                case "M4":
                    Spawn(0, 6, 3); Spawn(3, 6, 1);
                    break;
                case "M5":
                    Spawn(0, 7, 3); Spawn(1, 7, 1); Spawn(3, 7, 1);
                    break;
                case "M6":
                    Spawn(0, 8, 2); Spawn(1, 8, 1); Spawn(3, 8, 1);
                    Spawn(1, 9, 1);
                    Spawn(1, 10, 1);
                    Spawn(0, 11, 1); Spawn(3, 11, 1);
                    break;
                case "M7":
                    Spawn(0, 12, 1); Spawn(3, 12, 1);
                    break;
                case "M8":
                    Spawn(0, 13, 1); Spawn(3, 13, 1);
                    Spawn(0, 14, 2);
                    break;
                case "M9":
                    Spawn(0, 15, 1); Spawn(2, 15, 1); Spawn(3, 15, 1);
                    Spawn(0, 16, 1); Spawn(3, 16, 1);
                    break;
                case "M10":
                    Spawn(0, 17, 1); 
                    Spawn(0, 18, 1); Spawn(2, 18, 1); Spawn(3, 18, 1);
                    Spawn(0, 19, 1); Spawn(3, 19, 1);
                    Spawn(0, 20, 1); Spawn(2, 20, 1); Spawn(3, 20, 1);
                    Spawn(0, 21, 1); Spawn(2, 21, 1); Spawn(3, 21, 1);
                    Spawn(0, 22, 1); Spawn(2, 22, 1); Spawn(3, 22, 1);
                    break;
                case "E_Next":
                    if (Exitable)
                    {
                        StartCoroutine(GoToG1());
                        Exitable = false;
                    }
                    break;
            }

            // Edge�� ���丮 Ʈ���� �� �����
            if (collision.gameObject.name != "E_Next")
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Exitable = true;
    }

    // �� ���丮���� �� �Ʒ� ����˴ϴ�. 
    private IEnumerator E1()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("���� 1���� �Ĺ� �������̾�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("Ư�� ���� �����ϴ� ������ �����Ϸ�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("��...!", "�˷���", true));

        StoryEnd();
    }

    private IEnumerator E2()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("��? �̰� ����?", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("��..�� �ΰ� ��������?", "�˷���", false));

        yield return StartCoroutine(ShowScript("���⿡�� ������ �����ϴ� ������� �־��� �� ����.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�����ؼ� �������°� ���ڱ���.", "E�ڻ�", true));

        StoryEnd();
    }

    private IEnumerator E3()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("���� ����! ����ȭ Ű����!", "�˷���", true));

        StoryEnd();
    }

    private IEnumerator E4()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("�� ���� ������ �ַ� ����߾��ܴ�. �����Ϸ�.", "E�ڻ�", true));

        GameManager.Instance.Spawner.Spawn(0, 5, 3);
        GameManager.Instance.Spawner.Spawn(3, 5, 2);

        StoryEnd();
    }

    private IEnumerator E5()
    {
        StoryStart();

        StartCoroutine(Escalater(Doctor.transform));

        yield return new WaitForSeconds(3.5f);

        yield return StartCoroutine(ShowScript("�� �ڷ� �ٽ� ���ư��� ���� �ʰڱ���.", "E�ڻ�", true));

        StoryEnd();
    }

    private IEnumerator E6()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("������� ������ ������̾�", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�� �ո� �����̰� ��� ������ ���ÿ� ����� ���̶���.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�������� ���� ���� ���� �ǰڱ���...!", "�˷���", true));

        StoryEnd();
    }

    private IEnumerator GoToG1()
    {
        if (GameManager.Instance.leftMemoryInScene > 0)
        {
            StoryStart();
            yield return StartCoroutine(ShowScript("���� ã�� ���� ����� ������ ���� �� ���ƿ�...", "�˷���", true));
            StoryEnd();
        }
        else
        {
            StoryStart();

            yield return StartCoroutine(ShowScript("���� �������� ����!", "E�ڻ�", true));

            SceneManager.LoadScene("G1");
        }
    }

    // =====================================================

    //���丮 ���۰� ���� ���
    private void StoryStart()
    {
        DoctorScript.IM_keyInputVect = new Vector2(0, 0);
        ObjectScanArea.SetActive(false);
        InputManagerScript.dr_movable = false;
        InputManagerScript.dr_weapon = false;
    }

    private void StoryEnd()
    {
        ObjectScanArea.SetActive(true);
        InputManagerScript.dr_movable = true;
    }

    private void Spawn(int monsterType, int inSpawnpoint, int amount)
    {
        GameManager.Instance.Spawner.Spawn(monsterType, inSpawnpoint, amount);
    }

    //���丮 ����
    private IEnumerator ShowScript(string inScript, string inName, bool lastword)
    {
        UI_Script.SetActive(true);
        Name.text = inName;
        EffectScript.StartTyping(inScript, lastword);

        yield return new WaitForSeconds(1f);
        // RŰ�� ���� ������ ���
        yield return new WaitUntil(() => Input.GetButtonDown("Next"));

        if (lastword)
        {
            UI_Script.SetActive(false);
        }
    }

    IEnumerator Escalater(Transform doctorTransform)
    {
        float startTime = Time.time;
        Vector3 startPosition = doctorTransform.position;
        Vector3 targetPosition = new Vector3(214f, doctorTransform.position.y, doctorTransform.position.z);

        // ���� �ܰ�
        while (Time.time < startTime + accelerationTime)
        {
            float t = (Time.time - startTime) / accelerationTime;
            float speed = Mathf.Lerp(0, maxSpeed, t);
            doctorTransform.position = Vector3.MoveTowards(doctorTransform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // ��� � �ܰ�
        startTime = Time.time;
        while (Time.time < startTime + constantSpeedTime)
        {
            doctorTransform.position = Vector3.MoveTowards(doctorTransform.position, targetPosition, maxSpeed * Time.deltaTime);
            yield return null;
        }

        // ���� �ܰ�
        startTime = Time.time;
        while (Time.time < startTime + decelerationTime)
        {
            float t = (Time.time - startTime) / decelerationTime;
            float speed = Mathf.Lerp(maxSpeed, 0, t);
            doctorTransform.position = Vector3.MoveTowards(doctorTransform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

}



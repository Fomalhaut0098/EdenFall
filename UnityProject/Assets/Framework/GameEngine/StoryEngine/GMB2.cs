using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���丮 ���� (T1) - ��� ��ũ��Ʈ �� ���� ���� �� ����
// Doctor�� ���� ������Ʈ�� �����Ǿ��־�� ��
// �䱸 ���� �ý��� | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMB2 : MonoBehaviour
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
            switch(collision.gameObject.name)
            {
                case "E1":
                    StartCoroutine(E1());
                    break;
                case "E2":
                    StartCoroutine(E2());
                    break;
                case "E3":
                    StartCoroutine(E3());
                    break;
                case "E4":
                    StartCoroutine(E4());
                    break;
                case "M1":
                    Monster1();
                    break;
                case "E5":
                    StartCoroutine(E5());
                    break;
                case "E_Next":
                    if(Exitable)
                    {
                        StartCoroutine(GoToB1());
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

        yield return StartCoroutine(ShowScript("�� �տ��� ��ι� ���� ������ �ַ� �߾���...", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("��, �Ʊ� ��ι��� ���� �ֿ����� �����̳׿�!", "�˷���", true));

        StoryEnd();
    }

    private IEnumerator E2()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("��� ������ �װ� �ƴ϶� �� ���� �����̶�� �ž�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("���͸��� �ſ� ������ �ٴ� ������ �ʰ� �����ϵ���", "E�ڻ�", true));

        StoryEnd();
    }

    private IEnumerator E3()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("��, �� �տ� ��ǻ�͵��� ���ƿ�! �о�� ������ ���״� �� �о����!", "�˷���", true));

        StoryEnd();
    }

    private IEnumerator E4()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("��, �� �տ� �� ����! �����̿���!", "�˷���", false));

        yield return StartCoroutine(ShowScript("�Ƹ� ��ιΰ� �����ҰŶ���! �����Ϸ�!", "E�ڻ�", true));

        StoryEnd();
    }

    private void Monster1()
    {
        //���� Ÿ��, ��ġ(1����), ������
        GameManager.Instance.Spawner.Spawn(0, 1, 5);
        GameManager.Instance.Spawner.Spawn(0, 2, 2);
    }

    private IEnumerator E5()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("���� ã�Ҵ�! ù��° ����ȭ Ű��.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("��� ������ �ϳ��� ������ ����ȭ ��ġ�� �����Ϸ��� �� ì�ܰ����Ѵܴ�.", "E�ڻ�", true));

        StoryEnd();
    }

    private IEnumerator GoToB1()
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

            yield return StartCoroutine(ShowScript("���� ���� 1���̱�. ���� �ܴ��� �Ե���.", "E�ڻ�", true));

            SceneManager.LoadScene("B1");
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
}



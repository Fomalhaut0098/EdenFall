using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���丮 ���� (T1) - ��� ��ũ��Ʈ �� ���� ���� �� ����
// Doctor�� ���� ������Ʈ�� �����Ǿ��־�� ��
// �䱸 ���� �ý��� | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMT1 : MonoBehaviour
{
    //�⺻ ���� ��ũ��Ʈ ����
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject UI_Blink;
    public GameObject ObjectScanArea;

    //�����ι� ����
    public GameObject Doctor;
    public GameObject TriDoc;
    public GameObject Fance;

    public Text Name;

    private TypeEffect EffectScript;
    private InputManager InputManagerScript;
    private UI_Blink BlinkScript;
    private TriDoc TriDocScript;
    
    //���ΰ� ����
    private Doctor DoctorScript;

    private bool Exitable = true;

    void Start()
    {
        //���� ��ũ��Ʈ �ʱ�ȭ
        EffectScript = TypeEffect.GetComponent<TypeEffect>();
        DoctorScript = Doctor.GetComponent<Doctor>();
        TriDocScript = TriDoc.GetComponent<TriDoc>();

        InputManagerScript = InputManager.GetComponent<InputManager>();
        BlinkScript = UI_Blink.GetComponent<UI_Blink>();

        //���丮 1 ����
        StartCoroutine(Story1());
    }

    // Edge�� ���� ���丮 ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Edge"))
        {
            Debug.Log($"Edge Ʈ���� ������: {collision.gameObject.name}");

            // �� Edge ���� �� �Ʒ��� ���丮 ����
            if (collision.gameObject.name == "E_Next")
            {
                if(Exitable)
                {
                    StartCoroutine(Story2());
                    Exitable = false;
                }
            }

            // Edge�� ���丮 Ʈ���� �� �����
            if(collision.gameObject.name != "E_Next")
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
    private IEnumerator Story1()
    {
        StoryStart();

        Fance.SetActive(true);
        UI_Blink.SetActive(true);
        yield return new WaitForSeconds(2f);
        DoctorScript.LayDown(true);

        // ù ��° ��� ����
        yield return StartCoroutine(ShowScript("�ڻ��", "???", false));
        BlinkScript.BlinkStart();
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(ShowScript("�ڻ�� !!!", "???", false));
        BlinkScript.BlinkStart();
        yield return new WaitForSeconds(1f);
        BlinkScript.BlinkStart();

        yield return new WaitForSeconds(3f);
        UI_Blink.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        DoctorScript.LayDown(false);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowScript("ļ�ƾ� ���� ���� ", "E�ڻ�", false));

        TriDocScript.Wiggle(1);
        yield return StartCoroutine(ShowScript("��񸸿�, �����ϼ���!!", "???", false));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("���ƾ� �������S��, ��, �����̴�!!!", "E�ڻ�", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("�ƴ� �����ƴϿ���! ������ ��, �˷���!", "�˷���", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("�˷���? �� �̱��װ��뿡�� ���Ͻ����� �����ֿ� �Ի��� �˷��� ���̳�?", "E�ڻ�", true));

        yield return StartCoroutine(ShowScript("�� ����� ��ü ����? ��... �װ� �ΰ��� ����� �ƴ��ݾ�!", "E�ڻ�", true));

        yield return StartCoroutine(ShowScript("���� ���м���ó�� ���ܼ���... ��� �� ���� �� �� �̷�!!!!!! ���ƾƾƾƾ�", "E�ڻ�", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("���ƾ� �ڻ��, ������ �ϼ���!!!", "�˷���", true));
        TriDocScript.Wiggle(0);


        yield return StartCoroutine(ShowScript("���� �׷� �˷���, �̰� ��ü �������̴�? ���ݺ��� �������ΰ� �ϳ��� ���µ�, ���� ���� �ٴ°ǰ�?", "E�ڻ�", true));

        yield return StartCoroutine(ShowScript("�׸���... �� �̷��� ��ﳪ�� �� ����? ��.. ������ ����� �� ����.", "E�ڻ�", false));

        TriDocScript.Wiggle(1);
        yield return StartCoroutine(ShowScript("��? ����������?", "�˷���", true));

        yield return StartCoroutine(ShowScript("�ٱ��� �������� �ٱ۰Ÿ��µ�...  �ڻ�Ա��� ��� ������̶��...!", "�˷���", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("�����Ǿ��ִ� �������� ���� ������ ���ع��Ⱦ��. �ױ� ������ ���Դ� ������ ¥�� ������ �ܿ� �ڻ�� �����Ƿ� �°Ŷ���!", "�˷���", true));

        yield return StartCoroutine(ShowScript("�Դٰ� �����Ƿ� �����ڸ��� �� ����... �̷� ������ ������� �ٲ����Ⱦ��!", "�˷���", true));

        yield return StartCoroutine(ShowScript("�ڻ�� �����... �� ���⼭ �̷��� �ױ� �Ⱦ��! �� ȭ������ ���ư��� �ʹٰ��...", "�˷���", true));

        TriDocScript.Wiggle(3);
        yield return StartCoroutine(ShowScript("�ڻ��... �ڻ��!!!!! ����!!!", "�˷���", true));


        yield return StartCoroutine(ShowScript("���̰� �̳��, ������ �� ��! �ִ� ��ﵵ �� ���ø����ݾ�!", "E�ڻ�", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("�������� �ٲ���ٰ�... �׷��ٸ� �и� '����ȭ ��ġ'�� �۵��Ѱɰž�.", "E�ڻ�", true));

        yield return StartCoroutine(ShowScript("�������� �͵��� ���� '����ȭ' �Ǿ, ������ ��ġ �޼�ó�� ���Ѱ���.", "E�ڻ�", true));

        yield return StartCoroutine(ShowScript("�������� ����... �⿩�� ���� ����±�.", "E�ڻ�", true));

        TriDocScript.Wiggle(1);
        yield return StartCoroutine(ShowScript("����ȭ ��ġ��? �װ� ��� �۵���Ų����?", "�˷���", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("�װ� ���� �����Ұ� �ƴϾ�. �ϴ� �츰 ���⿡�� ��������.", "E�ڻ�", true));

        TriDocScript.Wiggle(1);
        yield return StartCoroutine(ShowScript("��... ���� �Ҹ� �Ͻô°ſ���! ���� ��� ������� �����ƴµ�!!!", "�˷���", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("������ �и� �����Ŷ���! ���� �̷��� �ٸ�����Ʈ���� �ĳ��µ���!", "�˷���", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("�׷� ��ұ�? ���� �����ҿ��� ���� ���� ���� 2�� �� �� ���̶��.", "E�ڻ�", true));

        yield return StartCoroutine(ShowScript("����ȭ �Ǿ�� �������� ��� ����... ȭ�� ������ ����Ű�� �������� ���� �� �����ž�.", "E�ڻ�", true));

        InputManagerScript.dr_weapon = true;
        yield return StartCoroutine(ShowScript("�׸��� �� ����跲 ���� �տ����� ��ΰ� �������", "E�ڻ�", true));

        Fance.SetActive(false);
        yield return StartCoroutine(ShowScript("�� �ٸ�����Ʈ�� ġ������.", "E�ڻ�", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("��... ��¥ �����ſ���??? ��.. ���� �����!", "�˷���", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("���� ���弭�ڴ�. �����!", "E�ڻ�", true));

        yield return StartCoroutine(ShowScript("�켱 �ֺ��� �� �ѷ����߰ڱ�. �������� ����� ȭ�й������ �������͸�, �׸��� �� ����� ��ã�� �ܼ����� �ʿ���.", "E�ڻ�", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("��... �׷��ٸ� ��¦�̴� ���� �տ��� RŰ�� ����������...", "�˷���", true));

        yield return StartCoroutine(ShowScript("�ڻ�Բ� ������ �� ���� ����� �������� ã�� �� �����ſ���.", "�˷���", true));

        yield return StartCoroutine(ShowScript("E... E�� ������ �ڵ����� Ȯ���ص� ���ٰ� �����ؿ�.....!", "�˷���", true));
        TriDocScript.Wiggle(0);

        StoryEnd();
    }

    private IEnumerator Story2()
    {
        if(GameManager.Instance.leftMemoryInScene > 0)
        {
            StoryStart();
            yield return StartCoroutine(ShowScript("���� ã�� ���� ����� ������ ���� �� ���ƿ�...", "�˷���", true));
            StoryEnd();
        }
        else
        {
            StoryStart();

            yield return StartCoroutine(ShowScript("����!", "E�ڻ�", true));

            SceneManager.LoadScene("B2");
        }
    }

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

        if(lastword)
        {
            UI_Script.SetActive(false);
        }
    }
}



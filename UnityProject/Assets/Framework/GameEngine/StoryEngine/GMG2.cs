using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���丮 ���� (T1) - ��� ��ũ��Ʈ �� ���� ���� �� ����
// Doctor�� ���� ������Ʈ�� �����Ǿ��־�� ��
// �䱸 ���� �ý��� | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMG2 : MonoBehaviour
{
    //�⺻ ���� ��ũ��Ʈ ����
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;

    public Camera mainCamera; // ������ ī�޶�
    public float speed = 0.2f; // ���� ���� �ӵ�
    public float saturation = 0.5f; // ä�� (0 ~ 1 ������ ��)

    public float acceleration = 1000f; // ���ӵ�
    public float maxZoomOut = 100000f; // �ִ� ī�޶� ũ��

    public bool BOoom = false;

    //�����ι� ����
    public GameObject Doctor;

    public Text Name;

    private TypeEffect EffectScript;
    private InputManager InputManagerScript;

    //���ΰ� ����
    private Doctor DoctorScript;

    void Start()
    {
        //���� ��ũ��Ʈ �ʱ�ȭ
        EffectScript = TypeEffect.GetComponent<TypeEffect>();
        InputManagerScript = InputManager.GetComponent<InputManager>();

        //�����ι� ��ũ��Ʈ �ʱ�ȭ
        DoctorScript = Doctor.GetComponent<Doctor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Edge"))
        {
            Debug.Log($"Edge Ʈ���� ������: {collision.gameObject.name}");

            // �� Edge ���� �� �Ʒ��� ���丮 ����
            switch (collision.gameObject.name)
            {
                case "E1": StartCoroutine(E1()); break;
                case "M1": Spawn(0, 1, 2); Spawn(0, 2, 1); break;
                case "E2": StartCoroutine(E2()); break;
                case "E_Final": StartCoroutine(Final()); break;
            }

            // Edge�� ���丮 Ʈ���� �� �����
            Destroy(collision.gameObject);
        }
    }

    // �� ���丮���� �� �Ʒ� ����˴ϴ�. 
    private IEnumerator E1()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("�̰�...?", "�˷���", false));

        yield return StartCoroutine(ShowScript("����ȭ ��ġ�� �ʹ� ����� ſ�� ����ģ ����ȭ�� �̷���� �� ����.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�ٴ� �ٱ����� �������� �ʰ� �����Ϸ�. ������ �ʰ����� ���� �Ҵ´ܴ�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("���� �ٸ��� �ʾ� ���̴µ���...", "�˷���", true));
        
        StoryEnd();
    }

    private IEnumerator E2()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("����ȭ Ű����!", "�˷���", false));

        yield return StartCoroutine(ShowScript("�׷�, �̰� �츮�� ���� ���� �� �ִ� ������ Ű�ڱ���.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("������ Ű�� �Ƹ� �������� ������ ������ �����Ŵ�.", "E�ڻ�", true));

        StoryEnd();
    }

    private IEnumerator Final()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("����ȭ ��ġ...?", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�̰� ����ȭ ��ġ�ΰ���?", "�˷���", false));

        yield return StartCoroutine(ShowScript("�׷�, ������ ����������.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("��� �� ������? �и� �۵��ϰ� �־�� �ϴµ�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�׷�, ������ �۵��ϰ� �ְ� ���̾�.", "???", false));

        yield return StartCoroutine(ShowScript("�� ��Ҹ���!", "�˷���", true));

        BOoom = true;

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("BOSS");
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

    void Update()
    {
        // �ð��� ���� Hue ���� ����
        float hue = Mathf.Repeat(Time.time * speed, 1);
        Color color = Color.HSVToRGB(hue, saturation, 1);

        // ī�޶��� ���� ����
        mainCamera.backgroundColor = color;

        if (BOoom)
        {
            float currentSize = Camera.main.orthographicSize;

            // �� ī�޶� ũ�⸦ ����մϴ�. (���ӵ� ����)
            currentSize += acceleration * Time.deltaTime;

            // ī�޶� ũ�Ⱑ �ִ�ġ�� ���� �ʵ��� �մϴ�.
            currentSize = Mathf.Min(currentSize, maxZoomOut);

            // �� ī�޶� ũ�⸦ �����մϴ�.
            Camera.main.orthographicSize = currentSize;
        }
    }
}

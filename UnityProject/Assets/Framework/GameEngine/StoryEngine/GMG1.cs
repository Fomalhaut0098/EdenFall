using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���丮 ���� (T1) - ��� ��ũ��Ʈ �� ���� ���� �� ����
// Doctor�� ���� ������Ʈ�� �����Ǿ��־�� ��
// �䱸 ���� �ý��� | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMG1 : MonoBehaviour
{
    //�⺻ ���� ��ũ��Ʈ ����
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;

    public GameObject DarkSp;
    private SpriteRenderer Dark;

    public float fadeInDuration = 2.0f; // ���̵��ο� �ɸ��� �ð� (��)

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

        Dark = DarkSp.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Edge"))
        {
            Debug.Log($"Edge Ʈ���� ������: {collision.gameObject.name}");

            // �� Edge ���� �� �Ʒ��� ���丮 ����
            switch (collision.gameObject.name)
            {
                case "E1":
                    StartCoroutine(E1());
                    break;
                case "E2":
                    StartCoroutine(E2());
                    break;
                case "E3":
                    Monster1();
                    break;
                case "M1":
                    Spawn(0, 3, 1);
                    Spawn(3, 3, 1);
                    break;
                case "M2":
                    Spawn(0, 4, 2);
                    break;
                case "S1":
                    StartCoroutine(S1());
                    break;
                case "E4":
                    StartCoroutine(E4());
                    break;
                case "E_Next":
                    if (Exitable)
                    {
                        StartCoroutine(GoToG2());
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

        yield return StartCoroutine(ShowScript("��...", "E�ڻ�", false));

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowScript("�� �׷�����?", "�˷���", false));

        yield return StartCoroutine(ShowScript("��, â ���� �ʹ� �Ƹ��ٿ��� ��� ������ ����ܴ�", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("������ �޼��̶� ����� ���� �� ���ٴ°� �ƽ�����.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�׾� ������ ������ �Ƹ����� �ʾƿ�. �ٷ� ����Ʈ �տ� �ִµ��� ��.", "�˷���", false));

        yield return StartCoroutine(ShowScript("���� �ڻ翡�� �̷� �鵵 �ִ����� ������", "E�ڻ�", true));

        StoryEnd();
    }

    private IEnumerator E2()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("����, �ٷ� �� ������ ���� �����ֱ���.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("���� �Ӽ��� �������?", "�˷���", false));

        yield return StartCoroutine(ShowScript("���� ����ġ�� ���峪�� ��¿ �� �����ž�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("��� ����ġ�� ���� ���� �־�. ��¿ �� ���� ������ ���ٰ� Ž���ϴ� ���ۿ� ���ڱ�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("��, ���� �������� ��� ����?", "�˷���", false));

        yield return StartCoroutine(ShowScript("���� �����ؾ���.", "E�ڻ�", true));

        StoryEnd();
    }

    private void Monster1()
    {
        StartCoroutine(Darker());
        Spawn(0, 1, 1);
        Spawn(0, 2, 1);
    }

    private IEnumerator S1()
    {
        StoryStart();

        Dark.color = new Color(Dark.color.r, Dark.color.g, Dark.color.b, 0);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowScript("��.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("���� �ֺ��� �ѷ����ڲٳ�.", "E�ڻ�", true));

        StoryEnd();
    }

    private IEnumerator E4()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("���� ��κ��� ����� ã�� �� ����.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("���� ������ ���� �ڻ縦 ������ ��� ���� �˰Եǰ���...", "E�ڻ�", true));

        StoryEnd();
    }

    private IEnumerator GoToG2()
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

            yield return StartCoroutine(ShowScript("���� 2���� ���̶��. ������ ���̴±�.", "E�ڻ�", true));

            SceneManager.LoadScene("G2");
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

    IEnumerator Darker()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp(elapsedTime / fadeInDuration, 0, 0.85f); // 0���� 0.7 ������ ������ ��ȯ
            Dark.color = new Color(Dark.color.r, Dark.color.g, Dark.color.b, alpha);
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���丮 ���� (T1) - ��� ��ũ��Ʈ �� ���� ���� �� ����
// Doctor�� ���� ������Ʈ�� �����Ǿ��־�� ��
// �䱸 ���� �ý��� | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMBOSS : MonoBehaviour
{
    //�⺻ ���� ��ũ��Ʈ ����
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;
    

    public Camera MainCamera; // ������ ī�޶�
    public float speed = 0.1f; // ���� ���� �ӵ�
    public float saturation = 0.2f; // ä�� (0 ~ 1 ������ ��)

    public float fadeInDuration = 2.0f; // ���̵��ο� �ɸ��� �ð� (��)
    public float VfadeInDuration = 2.0f; // ���̵��ο� �ɸ��� �ð� (��)

    public GameObject LightSp;
    private SpriteRenderer Light;

    //�����ι� ����
    public GameObject Doctor;
    public GameObject JVaim;

    public Text Name;

    private TypeEffect EffectScript;
    private InputManager InputManagerScript;

    //���ΰ� ����
    private Doctor DoctorScript;
    private JVaim VaimScript;
    private SpriteRenderer Vaim;

    private bool finale = true;

    void Start()
    {
        //���� ��ũ��Ʈ �ʱ�ȭ
        EffectScript = TypeEffect.GetComponent<TypeEffect>();
        InputManagerScript = InputManager.GetComponent<InputManager>();

        //�����ι� ��ũ��Ʈ �ʱ�ȭ
        DoctorScript = Doctor.GetComponent<Doctor>();
        VaimScript = JVaim.GetComponent<JVaim>();

        Light = LightSp.GetComponent<SpriteRenderer>();
        Vaim = JVaim.GetComponent<SpriteRenderer>();

        GameManager.Instance.Battery = -10;
        StartCoroutine(Story1());
    }

    // �� ���丮���� �� �Ʒ� ����˴ϴ�. 
    private IEnumerator Story1()
    {
        StoryStart();

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("�ᱹ ������, �ʿ���. �������� ����.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�׷�. �������̱�, E�ڻ�.", "�������� ����", false));

        yield return StartCoroutine(ShowScript("���� �������� �̷� ���� ����Ų����? ���� �ڳװ� ���� ���� ���������� �˰� �ֱ⳪ �ϳ�?", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�˴� ����! �翬�� �Ҹ��� �����̰� �ֱ�!", "�������� ����", false));

        yield return StartCoroutine(ShowScript("�ݴ�� ȭ���� �� ����� ����! ���� �� �ڳ׿� �� ���п��� �ϳ��� ����ڳ�?", "�������� ����", false));

        yield return StartCoroutine(ShowScript("������ ���ο��� ���� ������ �־���! �� ���� �ʾ� ������ �Ŵ��� �¾�ǳ�� ���Ŀ� ������ ����Ѵٰ� ���̾�!", "�������� ����", false));

        yield return StartCoroutine(ShowScript("��ΰ� Ż �Ŵ��� ���ּ� ������ ����... ����ȭ ��ġ�� ��θ� �޼����� ���ֽ�Ű�°� ������ ����̾��ٰ�!", "�������� ����", false));

        yield return StartCoroutine(ShowScript("��Ҹ��� �ϴ±�. �׷� ��â�� ������ �־��ٸ� ���ΰ� �ܿ� ��� �ϳ����� ���������� ���ݾ�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�Դٰ� ���� ��ũ��ũ�� �ƴϰ� �̷� �ܰ��� �ִ� ������ ȭ�п����ҿ�... ��������� ���� �Ǵ� �̾߱��ΰ�?", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("������ �ʿ��Ѱ� ������ ��ҿ���.", "�������� ����", false));

        yield return StartCoroutine(ShowScript("���ʿ� '����ȭ ��ġ' ��� ����� �ְ�, �޼� ���谡 ������ �ذ�å�̶�� ������ ��ȸ�� ���̹и� ������ ����� �� ����?", "�������� ����", false));

        yield return StartCoroutine(ShowScript("�ڳ� ����� �׷� ���� ���θ� �����Ұ�, �ֱ� �� �׷��� ���� ������ �޾��� �� ����!", "�������� ����", false));

        yield return StartCoroutine(ShowScript("�� ������ '���� ����' ������ �����ų �� ���� ������ ������.", "�������� ����", false));

        yield return StartCoroutine(ShowScript("���δ� ������ ���󿡼� ���ζ��ϴ� ���� �������鵵 �����ҿ� ��ġ���׾�.", "�������� ����", false));

        yield return StartCoroutine(ShowScript("�׷��� �ڳװ� ���� �߶�����ݾ�!", "�������� ����", false));

        yield return StartCoroutine(ShowScript("���� �ȿ����� �Ĺ��� ���� ������ ����µ�, �ᱹ ���� å���� ���� ������� ����.", "�������� ����", false));

        yield return StartCoroutine(ShowScript("�̷���, ���� ���� �������� ���̳�?", "�������� ����", true));

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("�ڻ��...", "�˷���", false));

        yield return StartCoroutine(ShowScript("... �հ����� ���� ������� �˾Ҵµ� �ǿܷ� ������ �ΰ��̱�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("������ �ڳ״� ����ȸ�� �ڳ׸� ��ǥ�� ���ӽð����� ����ȭ ��ġ�� �Ѽ� ��θ� ������ �������.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("���� ������ ���� ���ϰ� ������ ���ߴٸ�, ���� �Ⲩ�� �ּ��� ���� �����־����Ŵ�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�ʰ� ��θ� ���ΰž�.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("��, ���� �׷��� ���� ����.", "�������� ����", false));

        yield return StartCoroutine(ShowScript("���� ���⼭ ������ �ص�, ������ ���������� ���� �𸣰ŵ�.", "�������� ����", false));

        yield return StartCoroutine(ShowScript("�̹� �� �����ڴ�. ���� �� �̻� �̷��� ����.", "�������� ����", false));

        yield return StartCoroutine(ShowScript("�� ���̰� ��� ���� ������.", "�������� ����", true));

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(ShowScript("...�׷�.", "E�ڻ�", true));

        for (int i = 0; i < 4; i++)
        {
            GameManager.Instance.leftAmmo[i] = 10;
        }
        InputManagerScript.dr_weapon = true;

        StoryEnd();
    }

    private IEnumerator E_Final()
    {
        StoryStart();
        InputManagerScript.dr_weapon = false;

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowScript("����...", "�������� ����", true));

        StartCoroutine(VaimDead());

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("��...", "E�ڻ�", false));
        yield return StartCoroutine(ShowScript("�̰屸��.", "E�ڻ�", true));

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("���� ��� �Ǵ°ɱ�.", "E�ڻ�", true));

        StartCoroutine(Lighter());

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Final");
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
        MainCamera.backgroundColor = color;

        if (VaimScript.vaimDead)
        {
            if (finale)
            {
                finale = false;
                StartCoroutine(E_Final());
            }
        }
    }

    IEnumerator Lighter()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp(elapsedTime / fadeInDuration, 0, 1f); // 0���� 0.7 ������ ������ ��ȯ
            Light.color = new Color(Light.color.r, Light.color.g, Light.color.b, alpha);
            yield return null;
        }
    }

    IEnumerator VaimDead()
    {
        float elapsedTime = 0;

        while (elapsedTime < VfadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsedTime / VfadeInDuration); // 1���� 0 ������ ������ ��ȯ
            Vaim.color = new Color(Vaim.color.r, Vaim.color.g, Vaim.color.b, alpha);
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���丮 ���� (T1) - ��� ��ũ��Ʈ �� ���� ���� �� ����
// Doctor�� ���� ������Ʈ�� �����Ǿ��־�� ��
// �䱸 ���� �ý��� | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMFINAL : MonoBehaviour
{
    //�⺻ ���� ��ũ��Ʈ ����
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;

    public Camera MainCamera; // ������ ī�޶�
    public float speed = 0.1f; // ���� ���� �ӵ�
    public float saturation = 0.2f; // ä�� (0 ~ 1 ������ ��)

    private float m_roughness;      //��ĥ�� ����
    private float m_magnitude;      //������ ����

    public float fadeInDuration = 7f; // ���̵��ο� �ɸ��� �ð� (��)

    public GameObject LightSp;
    private SpriteRenderer Light;

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

        Light = LightSp.GetComponent<SpriteRenderer>();

        StartCoroutine(Story1());
    }

    // �� ���丮���� �� �Ʒ� ����˴ϴ�. 
    private IEnumerator Story1()
    {
        StoryStart();

        GameManager.Instance.Battery = -10;

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("���⿴��.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("...", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�׷��� ���� ���Ƿ� ���ư������� ����.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("�Բ��ؼ� ��ſ���, �˷���.", "E�ڻ�", false));

        yield return StartCoroutine(ShowScript("������, �ڻ��. �����̾����ϴ�.", "�˷���", true));

        StartCoroutine(Lighter());

        
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

    void Update()
    {
        // �ð��� ���� Hue ���� ����
        float hue = Mathf.Repeat(Time.time * speed, 1);
        Color color = Color.HSVToRGB(hue, saturation, 1);

        // ī�޶��� ���� ����
        MainCamera.backgroundColor = color;
    }

    IEnumerator Shake(float duration)
    {
        float halfDuration = duration / 2;
        float elapsed = 0f;
        float tick = Random.Range(-10f, 10f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime / halfDuration;

            tick += Time.deltaTime * m_roughness;
            MainCamera.transform.position = new Vector3(
                Mathf.PerlinNoise(tick, 0) - .5f,
                Mathf.PerlinNoise(0, tick) - .5f,
                0f) * m_magnitude * Mathf.PingPong(elapsed, halfDuration);

            yield return null;
        }
    }

    IEnumerator Lighter()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp(elapsedTime / fadeInDuration, 0, 1f);
            Light.color = new Color(Light.color.r, Light.color.g, Light.color.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Ending");
    }

}

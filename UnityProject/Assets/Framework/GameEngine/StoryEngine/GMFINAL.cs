using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 스토리 엔진 (T1) - 대사 스크립트 및 엣지 감지 및 동작
// Doctor의 하위 오브젝트로 고정되어있어야 함
// 요구 종속 시스템 | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMFINAL : MonoBehaviour
{
    //기본 하위 스크립트 제어
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;

    public Camera MainCamera; // 변경할 카메라
    public float speed = 0.1f; // 색상 변경 속도
    public float saturation = 0.2f; // 채도 (0 ~ 1 사이의 값)

    private float m_roughness;      //거칠기 정도
    private float m_magnitude;      //움직임 범위

    public float fadeInDuration = 7f; // 페이드인에 걸리는 시간 (초)

    public GameObject LightSp;
    private SpriteRenderer Light;

    //등장인물 제어
    public GameObject Doctor;

    public Text Name;

    private TypeEffect EffectScript;
    private InputManager InputManagerScript;

    //주인공 제어
    private Doctor DoctorScript;

    void Start()
    {
        //하위 스크립트 초기화
        EffectScript = TypeEffect.GetComponent<TypeEffect>();
        InputManagerScript = InputManager.GetComponent<InputManager>();

        //등장인물 스크립트 초기화
        DoctorScript = Doctor.GetComponent<Doctor>();

        Light = LightSp.GetComponent<SpriteRenderer>();

        StartCoroutine(Story1());
    }

    // 각 스토리들이 이 아래 저장됩니다. 
    private IEnumerator Story1()
    {
        StoryStart();

        GameManager.Instance.Battery = -10;

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("여기였군.", "E박사", false));

        yield return StartCoroutine(ShowScript("...", "E박사", false));

        yield return StartCoroutine(ShowScript("그러면 이제 현실로 돌아가보도록 하지.", "E박사", false));

        yield return StartCoroutine(ShowScript("함께해서 즐거웠네, 알렉스.", "E박사", false));

        yield return StartCoroutine(ShowScript("저도요, 박사님. 영광이었습니다.", "알렉스", true));

        StartCoroutine(Lighter());

        
    }


    // =====================================================
    //스토리 시작과 종료 헤더
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

    //스토리 엔진
    private IEnumerator ShowScript(string inScript, string inName, bool lastword)
    {
        UI_Script.SetActive(true);
        Name.text = inName;
        EffectScript.StartTyping(inScript, lastword);

        yield return new WaitForSeconds(1f);
        // R키가 눌릴 때까지 대기
        yield return new WaitUntil(() => Input.GetButtonDown("Next"));

        if (lastword)
        {
            UI_Script.SetActive(false);
        }
    }

    void Update()
    {
        // 시간에 따라 Hue 값을 변경
        float hue = Mathf.Repeat(Time.time * speed, 1);
        Color color = Color.HSVToRGB(hue, saturation, 1);

        // 카메라의 배경색 변경
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

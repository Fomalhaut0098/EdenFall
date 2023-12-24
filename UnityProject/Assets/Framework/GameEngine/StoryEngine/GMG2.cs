using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 스토리 엔진 (T1) - 대사 스크립트 및 엣지 감지 및 동작
// Doctor의 하위 오브젝트로 고정되어있어야 함
// 요구 종속 시스템 | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMG2 : MonoBehaviour
{
    //기본 하위 스크립트 제어
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;

    public Camera mainCamera; // 변경할 카메라
    public float speed = 0.2f; // 색상 변경 속도
    public float saturation = 0.5f; // 채도 (0 ~ 1 사이의 값)

    public float acceleration = 1000f; // 가속도
    public float maxZoomOut = 100000f; // 최대 카메라 크기

    public bool BOoom = false;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Edge"))
        {
            Debug.Log($"Edge 트리거 감지됨: {collision.gameObject.name}");

            // 각 Edge 별로 이 아래에 스토리 진행
            switch (collision.gameObject.name)
            {
                case "E1": StartCoroutine(E1()); break;
                case "M1": Spawn(0, 1, 2); Spawn(0, 2, 1); break;
                case "E2": StartCoroutine(E2()); break;
                case "E_Final": StartCoroutine(Final()); break;
            }

            // Edge는 스토리 트리거 후 지우기
            Destroy(collision.gameObject);
        }
    }

    // 각 스토리들이 이 아래 저장됩니다. 
    private IEnumerator E1()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("이건...?", "알렉스", false));

        yield return StartCoroutine(ShowScript("관념화 장치와 너무 가까운 탓에 지나친 관념화가 이루어진 것 같다.", "E박사", false));

        yield return StartCoroutine(ShowScript("바닥 바깥으로 떨어지지 않게 조심하렴. 죽지는 않겠지만 길을 잃는단다.", "E박사", false));

        yield return StartCoroutine(ShowScript("별로 다르지 않아 보이는데요...", "알렉스", true));
        
        StoryEnd();
    }

    private IEnumerator E2()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("관념화 키에요!", "알렉스", false));

        yield return StartCoroutine(ShowScript("그래, 이게 우리가 쉽게 얻을 수 있는 마지막 키겠구나.", "E박사", false));

        yield return StartCoroutine(ShowScript("마지막 키는 아마 짐나르다 베임이 가지고 있을거다.", "E박사", true));

        StoryEnd();
    }

    private IEnumerator Final()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("관념화 장치...?", "E박사", false));

        yield return StartCoroutine(ShowScript("이게 관념화 장치인가요?", "알렉스", false));

        yield return StartCoroutine(ShowScript("그래, 전원은 꺼져있지만.", "E박사", false));

        yield return StartCoroutine(ShowScript("어떻게 된 일이지? 분명 작동하고 있어야 하는데.", "E박사", false));

        yield return StartCoroutine(ShowScript("그래, 여전히 작동하고 있고 말이야.", "???", false));

        yield return StartCoroutine(ShowScript("이 목소리는!", "알렉스", true));

        BOoom = true;

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("BOSS");
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

    private void Spawn(int monsterType, int inSpawnpoint, int amount)
    {
        GameManager.Instance.Spawner.Spawn(monsterType, inSpawnpoint, amount);
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
        mainCamera.backgroundColor = color;

        if (BOoom)
        {
            float currentSize = Camera.main.orthographicSize;

            // 새 카메라 크기를 계산합니다. (가속도 적용)
            currentSize += acceleration * Time.deltaTime;

            // 카메라 크기가 최대치를 넘지 않도록 합니다.
            currentSize = Mathf.Min(currentSize, maxZoomOut);

            // 새 카메라 크기를 설정합니다.
            Camera.main.orthographicSize = currentSize;
        }
    }
}

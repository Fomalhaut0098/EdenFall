using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

// 스토리 엔진 (T1) - 대사 스크립트 및 엣지 감지 및 동작
// Doctor의 하위 오브젝트로 고정되어있어야 함
// 요구 종속 시스템 | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMB1 : MonoBehaviour
{
    //기본 하위 스크립트 제어
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;

    //등장인물 제어
    public GameObject Doctor;

    public Text Name;

    private TypeEffect EffectScript;
    private InputManager InputManagerScript;

    //주인공 제어
    private Doctor DoctorScript;

    private bool Exitable = true;

    //에스칼레이터 변수
    public float accelerationTime = 2.0f; // 가속에 걸리는 시간
    public float constantSpeedTime = 2.0f; // 등속 운동 시간
    public float decelerationTime = 2.0f; // 감속에 걸리는 시간
    public float maxSpeed = 10.0f; // 최대 속도

    void Start()
    {
        //하위 스크립트 초기화
        EffectScript = TypeEffect.GetComponent<TypeEffect>();
        InputManagerScript = InputManager.GetComponent<InputManager>();

        //등장인물 스크립트 초기화
        DoctorScript = Doctor.GetComponent<Doctor>();
    }

    // Edge에 의한 스토리 진행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Edge"))
        {
            Debug.Log($"Edge 트리거 감지됨: {collision.gameObject.name}");

            // 각 Edge 별로 이 아래에 스토리 진행
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

            // Edge는 스토리 트리거 후 지우기
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

    // 각 스토리들이 이 아래 저장됩니다. 
    private IEnumerator E1()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("지하 1층은 식물 실험장이야.", "E박사", false));

        yield return StartCoroutine(ShowScript("특히 물과 반응하는 루비듐을 조심하렴.", "E박사", false));

        yield return StartCoroutine(ShowScript("넵...!", "알렉스", true));

        StoryEnd();
    }

    private IEnumerator E2()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("음? 이건 뭐지?", "E박사", false));

        yield return StartCoroutine(ShowScript("미..로 인것 같은데요?", "알렉스", false));

        yield return StartCoroutine(ShowScript("여기에도 괴물에 저항하던 사람들이 있었던 것 같군.", "E박사", false));

        yield return StartCoroutine(ShowScript("조심해서 지나가는게 좋겠구나.", "E박사", true));

        StoryEnd();
    }

    private IEnumerator E3()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("저길 봐요! 관념화 키에요!", "알렉스", true));

        StoryEnd();
    }

    private IEnumerator E4()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("이 앞은 수은을 주로 사용했었단다. 조심하렴.", "E박사", true));

        GameManager.Instance.Spawner.Spawn(0, 5, 3);
        GameManager.Instance.Spawner.Spawn(3, 5, 2);

        StoryEnd();
    }

    private IEnumerator E5()
    {
        StoryStart();

        StartCoroutine(Escalater(Doctor.transform));

        yield return new WaitForSeconds(3.5f);

        yield return StartCoroutine(ShowScript("이 뒤로 다시 돌아가긴 쉽지 않겠구나.", "E박사", true));

        StoryEnd();
    }

    private IEnumerator E6()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("여기부턴 전기장 실험실이야", "E박사", false));

        yield return StartCoroutine(ShowScript("뻥 뚫린 공간이고 모든 물질을 동시에 사용한 곳이란다.", "E박사", false));

        yield return StartCoroutine(ShowScript("여정에서 가장 힘든 곳이 되겠군요...!", "알렉스", true));

        StoryEnd();
    }

    private IEnumerator GoToG1()
    {
        if (GameManager.Instance.leftMemoryInScene > 0)
        {
            StoryStart();
            yield return StartCoroutine(ShowScript("아직 찾지 않은 기억의 조각이 남은 것 같아요...", "알렉스", true));
            StoryEnd();
        }
        else
        {
            StoryStart();

            yield return StartCoroutine(ShowScript("드디어 지상으로 간다!", "E박사", true));

            SceneManager.LoadScene("G1");
        }
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

    IEnumerator Escalater(Transform doctorTransform)
    {
        float startTime = Time.time;
        Vector3 startPosition = doctorTransform.position;
        Vector3 targetPosition = new Vector3(214f, doctorTransform.position.y, doctorTransform.position.z);

        // 가속 단계
        while (Time.time < startTime + accelerationTime)
        {
            float t = (Time.time - startTime) / accelerationTime;
            float speed = Mathf.Lerp(0, maxSpeed, t);
            doctorTransform.position = Vector3.MoveTowards(doctorTransform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // 등속 운동 단계
        startTime = Time.time;
        while (Time.time < startTime + constantSpeedTime)
        {
            doctorTransform.position = Vector3.MoveTowards(doctorTransform.position, targetPosition, maxSpeed * Time.deltaTime);
            yield return null;
        }

        // 감속 단계
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



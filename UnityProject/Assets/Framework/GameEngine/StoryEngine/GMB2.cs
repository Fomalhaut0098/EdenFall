using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 스토리 엔진 (T1) - 대사 스크립트 및 엣지 감지 및 동작
// Doctor의 하위 오브젝트로 고정되어있어야 함
// 요구 종속 시스템 | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMB2 : MonoBehaviour
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

        yield return StartCoroutine(ShowScript("저 앞에선 브로민 반응 실험을 주로 했었지...", "E박사", false));

        yield return StartCoroutine(ShowScript("아, 아까 브로민은 많이 주웠으니 다행이네요!", "알렉스", true));

        StoryEnd();
    }

    private IEnumerator E2()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("사실 문제는 그게 아니라 내 폰이 똥폰이라는 거야.", "E박사", false));

        yield return StartCoroutine(ShowScript("배터리가 매우 빠르게 다니 꺼지지 않게 조심하도록", "E박사", true));

        StoryEnd();
    }

    private IEnumerator E3()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("저, 저 앞에 컴퓨터들이 많아요! 읽어보면 도움이 될테니 꼭 읽어보세요!", "알렉스", true));

        StoryEnd();
    }

    private IEnumerator E4()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("저, 저 앞에 좀 봐요! 괴물이에요!", "알렉스", false));

        yield return StartCoroutine(ShowScript("아마 브로민과 반응할거란다! 조심하렴!", "E박사", true));

        StoryEnd();
    }

    private void Monster1()
    {
        //몬스터 타입, 위치(1부터), 마리수
        GameManager.Instance.Spawner.Spawn(0, 1, 5);
        GameManager.Instance.Spawner.Spawn(0, 2, 2);
    }

    private IEnumerator E5()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("드디어 찾았다! 첫번째 관념화 키군.", "E박사", false));

        yield return StartCoroutine(ShowScript("모든 층마다 하나씩 있으니 관념화 장치를 해제하려면 꼭 챙겨가야한단다.", "E박사", true));

        StoryEnd();
    }

    private IEnumerator GoToB1()
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

            yield return StartCoroutine(ShowScript("이제 지하 1층이군. 마음 단단히 먹도록.", "E박사", true));

            SceneManager.LoadScene("B1");
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
}



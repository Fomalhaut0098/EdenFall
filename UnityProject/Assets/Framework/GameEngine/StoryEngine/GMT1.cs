using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 스토리 엔진 (T1) - 대사 스크립트 및 엣지 감지 및 동작
// Doctor의 하위 오브젝트로 고정되어있어야 함
// 요구 종속 시스템 | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMT1 : MonoBehaviour
{
    //기본 하위 스크립트 제어
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject UI_Blink;
    public GameObject ObjectScanArea;

    //등장인물 제어
    public GameObject Doctor;
    public GameObject TriDoc;
    public GameObject Fance;

    public Text Name;

    private TypeEffect EffectScript;
    private InputManager InputManagerScript;
    private UI_Blink BlinkScript;
    private TriDoc TriDocScript;
    
    //주인공 제어
    private Doctor DoctorScript;

    private bool Exitable = true;

    void Start()
    {
        //하위 스크립트 초기화
        EffectScript = TypeEffect.GetComponent<TypeEffect>();
        DoctorScript = Doctor.GetComponent<Doctor>();
        TriDocScript = TriDoc.GetComponent<TriDoc>();

        InputManagerScript = InputManager.GetComponent<InputManager>();
        BlinkScript = UI_Blink.GetComponent<UI_Blink>();

        //스토리 1 진행
        StartCoroutine(Story1());
    }

    // Edge에 의한 스토리 진행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Edge"))
        {
            Debug.Log($"Edge 트리거 감지됨: {collision.gameObject.name}");

            // 각 Edge 별로 이 아래에 스토리 진행
            if (collision.gameObject.name == "E_Next")
            {
                if(Exitable)
                {
                    StartCoroutine(Story2());
                    Exitable = false;
                }
            }

            // Edge는 스토리 트리거 후 지우기
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

    // 각 스토리들이 이 아래 저장됩니다. 
    private IEnumerator Story1()
    {
        StoryStart();

        Fance.SetActive(true);
        UI_Blink.SetActive(true);
        yield return new WaitForSeconds(2f);
        DoctorScript.LayDown(true);

        // 첫 번째 대사 시작
        yield return StartCoroutine(ShowScript("박사님", "???", false));
        BlinkScript.BlinkStart();
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(ShowScript("박사님 !!!", "???", false));
        BlinkScript.BlinkStart();
        yield return new WaitForSeconds(1f);
        BlinkScript.BlinkStart();

        yield return new WaitForSeconds(3f);
        UI_Blink.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        DoctorScript.LayDown(false);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowScript("캬아악 저게 뭐야 ", "E박사", false));

        TriDocScript.Wiggle(1);
        yield return StartCoroutine(ShowScript("잠깐만요, 진정하세요!!", "???", false));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("으아악 으허허헣헉, 괴, 괴물이다!!!", "E박사", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("아니 괴물아니에요! 저에요 저, 알렉스!", "알렉스", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("알렉스? 그 미국항공대에서 인턴십으로 저번주에 입사한 알렉스 말이냐?", "E박사", true));

        yield return StartCoroutine(ShowScript("그 모습은 대체 뭐야? 그... 그건 인간의 모습이 아니잖아!", "E박사", true));

        yield return StartCoroutine(ShowScript("무슨 수학선생처럼 생겨서는... 잠깐 내 몸은 또 왜 이래!!!!!! 으아아아아악", "E박사", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("으아악 박사님, 진정좀 하세요!!!", "알렉스", true));
        TriDocScript.Wiggle(0);


        yield return StartCoroutine(ShowScript("허어억 그래 알렉스, 이게 대체 무슨일이니? 지금보니 정상적인게 하나도 없는데, 내가 꿈을 꾸는건가?", "E박사", true));

        yield return StartCoroutine(ShowScript("그리고... 왜 이렇게 기억나는 게 없지? 내.. 기억들이 사라진 것 같군.", "E박사", false));

        TriDocScript.Wiggle(1);
        yield return StartCoroutine(ShowScript("네? 괜찮으세요?", "알렉스", true));

        yield return StartCoroutine(ShowScript("바깥엔 괴물들이 바글거리는데...  박사님까지 기억 상실증이라니...!", "알렉스", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("보관되어있던 물질들이 전부 괴물로 변해버렸어요. 죽기 직전에 젖먹던 힘까지 짜내 도망쳐 겨우 박사님 연구실로 온거라고요!", "알렉스", true));

        yield return StartCoroutine(ShowScript("게다가 연구실로 들어오자마자 제 몸도... 이런 흉측한 모습으로 바뀌어버렸어요!", "알렉스", true));

        yield return StartCoroutine(ShowScript("박사님 어떡하죠... 전 여기서 이렇게 죽기 싫어요! 전 화전으로 돌아가고 싶다고요...", "알렉스", true));

        TriDocScript.Wiggle(3);
        yield return StartCoroutine(ShowScript("박사님... 박사님!!!!! 제발!!!", "알렉스", true));


        yield return StartCoroutine(ShowScript("아이고 이놈아, 조용이 좀 해! 있는 기억도 못 떠올리겠잖아!", "E박사", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("물질들이 바뀌었다고... 그렇다면 분명 '관념화 장치'가 작동한걸거야.", "E박사", true));

        yield return StartCoroutine(ShowScript("물질적인 것들이 전부 '관념화' 되어서, 세상이 마치 꿈속처럼 변한거지.", "E박사", true));

        yield return StartCoroutine(ShowScript("짐나르다 베임... 기여코 일을 만드는군.", "E박사", true));

        TriDocScript.Wiggle(1);
        yield return StartCoroutine(ShowScript("관념화 장치요? 그걸 어떻게 작동시킨거죠?", "알렉스", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("그건 지금 생각할게 아니야. 일단 우린 여기에서 나가야해.", "E박사", true));

        TriDocScript.Wiggle(1);
        yield return StartCoroutine(ShowScript("무... 무슨 소리 하시는거에요! 제가 어떻게 여기까지 도망쳤는데!!!", "알렉스", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("나가면 분명 죽을거라고요! 제가 이렇게 바리케이트까지 쳐놨는데요!", "알렉스", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("그럼 어떡할까? 여긴 연구소에서 가장 깊은 지하 2층 맨 끝 방이라고.", "E박사", true));

        yield return StartCoroutine(ShowScript("관념화 되었어도 물질들은 모두 물질... 화학 반응을 일으키면 괴물들을 잡을 수 있을거야.", "E박사", true));

        InputManagerScript.dr_weapon = true;
        yield return StartCoroutine(ShowScript("그리고 이 더블배럴 샷건 앞에서는 모두가 평등하지", "E박사", true));

        Fance.SetActive(false);
        yield return StartCoroutine(ShowScript("그 바리케이트도 치워버려.", "E박사", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("지... 진짜 나갈거에요??? 저.. 저는 몰라요!", "알렉스", true));
        TriDocScript.Wiggle(0);

        yield return StartCoroutine(ShowScript("내가 앞장서겠다. 따라와!", "E박사", true));

        yield return StartCoroutine(ShowScript("우선 주변을 좀 둘러봐야겠군. 괴물들을 상대할 화학물질들과 보조배터리, 그리고 내 기억을 되찾을 단서들이 필요해.", "E박사", true));

        TriDocScript.Wiggle(2);
        yield return StartCoroutine(ShowScript("그... 그렇다면 반짝이는 물건 앞에서 R키를 눌러보세요...", "알렉스", true));

        yield return StartCoroutine(ShowScript("박사님께 도움이 될 만한 기억의 조각들을 찾을 수 있을거에요.", "알렉스", true));

        yield return StartCoroutine(ShowScript("E... E를 눌러서 핸드폰을 확인해도 좋다고 생각해요.....!", "알렉스", true));
        TriDocScript.Wiggle(0);

        StoryEnd();
    }

    private IEnumerator Story2()
    {
        if(GameManager.Instance.leftMemoryInScene > 0)
        {
            StoryStart();
            yield return StartCoroutine(ShowScript("아직 찾지 않은 기억의 조각이 남은 것 같아요...", "알렉스", true));
            StoryEnd();
        }
        else
        {
            StoryStart();

            yield return StartCoroutine(ShowScript("가자!", "E박사", true));

            SceneManager.LoadScene("B2");
        }
    }

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

        if(lastword)
        {
            UI_Script.SetActive(false);
        }
    }
}



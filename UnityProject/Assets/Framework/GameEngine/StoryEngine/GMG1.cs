using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 스토리 엔진 (T1) - 대사 스크립트 및 엣지 감지 및 동작
// Doctor의 하위 오브젝트로 고정되어있어야 함
// 요구 종속 시스템 | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMG1 : MonoBehaviour
{
    //기본 하위 스크립트 제어
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;

    public GameObject DarkSp;
    private SpriteRenderer Dark;

    public float fadeInDuration = 2.0f; // 페이드인에 걸리는 시간 (초)

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

        Dark = DarkSp.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Edge"))
        {
            Debug.Log($"Edge 트리거 감지됨: {collision.gameObject.name}");

            // 각 Edge 별로 이 아래에 스토리 진행
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

        yield return StartCoroutine(ShowScript("아...", "E박사", false));

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowScript("왜 그러세요?", "알렉스", false));

        yield return StartCoroutine(ShowScript("아, 창 밖이 너무 아름다워서 잠시 정신을 뺏겼단다", "E박사", false));

        yield return StartCoroutine(ShowScript("여전히 꿈속이라 저기로 나갈 수 없다는게 아쉽구나.", "E박사", false));

        yield return StartCoroutine(ShowScript("그야 현실은 저렇게 아름답지 않아요. 바로 아파트 앞에 있는데요 뭐.", "알렉스", false));

        yield return StartCoroutine(ShowScript("베임 박사에게 이런 면도 있는줄은 몰랐군", "E박사", true));

        StoryEnd();
    }

    private IEnumerator E2()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("저런, 바로 앞 복도는 불이 꺼져있구나.", "E박사", false));

        yield return StartCoroutine(ShowScript("불을 켤수는 없을까요?", "알렉스", false));

        yield return StartCoroutine(ShowScript("조명 스위치가 고장나서 어쩔 수 없을거야.", "E박사", false));

        yield return StartCoroutine(ShowScript("백업 스위치가 복도 끝에 있어. 어쩔 수 없이 끝까지 갔다가 탐색하는 수밖에 없겠군.", "E박사", false));

        yield return StartCoroutine(ShowScript("보, 복도 끝까지는 어떻게 가죠?", "알렉스", false));

        yield return StartCoroutine(ShowScript("정면 돌파해야지.", "E박사", true));

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

        yield return StartCoroutine(ShowScript("휴.", "E박사", false));

        yield return StartCoroutine(ShowScript("이제 주변을 둘러보자꾸나.", "E박사", true));

        StoryEnd();
    }

    private IEnumerator E4()
    {
        StoryStart();

        yield return StartCoroutine(ShowScript("이제 대부분의 기억을 찾은 것 같다.", "E박사", false));

        yield return StartCoroutine(ShowScript("다음 층에서 베임 박사를 만나면 모든 것을 알게되겠지...", "E박사", true));

        StoryEnd();
    }

    private IEnumerator GoToG2()
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

            yield return StartCoroutine(ShowScript("남은 2층이 끝이라네. 고지가 보이는군.", "E박사", true));

            SceneManager.LoadScene("G2");
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

    IEnumerator Darker()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp(elapsedTime / fadeInDuration, 0, 0.85f); // 0에서 0.7 사이의 값으로 변환
            Dark.color = new Color(Dark.color.r, Dark.color.g, Dark.color.b, alpha);
            yield return null;
        }
    }
}

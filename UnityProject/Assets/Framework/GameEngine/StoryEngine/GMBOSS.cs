using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 스토리 엔진 (T1) - 대사 스크립트 및 엣지 감지 및 동작
// Doctor의 하위 오브젝트로 고정되어있어야 함
// 요구 종속 시스템 | UI_Script | TypeEffect | InputManager | ObjectScanArea

public class GMBOSS : MonoBehaviour
{
    //기본 하위 스크립트 제어
    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject InputManager;
    public GameObject ObjectScanArea;
    

    public Camera MainCamera; // 변경할 카메라
    public float speed = 0.1f; // 색상 변경 속도
    public float saturation = 0.2f; // 채도 (0 ~ 1 사이의 값)

    public float fadeInDuration = 2.0f; // 페이드인에 걸리는 시간 (초)
    public float VfadeInDuration = 2.0f; // 페이드인에 걸리는 시간 (초)

    public GameObject LightSp;
    private SpriteRenderer Light;

    //등장인물 제어
    public GameObject Doctor;
    public GameObject JVaim;

    public Text Name;

    private TypeEffect EffectScript;
    private InputManager InputManagerScript;

    //주인공 제어
    private Doctor DoctorScript;
    private JVaim VaimScript;
    private SpriteRenderer Vaim;

    private bool finale = true;

    void Start()
    {
        //하위 스크립트 초기화
        EffectScript = TypeEffect.GetComponent<TypeEffect>();
        InputManagerScript = InputManager.GetComponent<InputManager>();

        //등장인물 스크립트 초기화
        DoctorScript = Doctor.GetComponent<Doctor>();
        VaimScript = JVaim.GetComponent<JVaim>();

        Light = LightSp.GetComponent<SpriteRenderer>();
        Vaim = JVaim.GetComponent<SpriteRenderer>();

        GameManager.Instance.Battery = -10;
        StartCoroutine(Story1());
    }

    // 각 스토리들이 이 아래 저장됩니다. 
    private IEnumerator Story1()
    {
        StoryStart();

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("결국 예상대로, 너였군. 짐나르다 베임.", "E박사", false));

        yield return StartCoroutine(ShowScript("그래. 오랜만이군, E박사.", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("무슨 생각으로 이런 일을 일으킨거지? 지금 자네가 무슨 일을 저지른건지 알고 있기나 하나?", "E박사", false));

        yield return StartCoroutine(ShowScript("알다 마다! 당연한 소리를 지껄이고 있군!", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("반대로 화내야 할 사람은 나야! 내가 왜 자네와 그 대학원생 하나만 남겼겠나?", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("나에겐 정부에서 받은 지령이 있었어! 얼마 있지 않아 지구에 거대한 태양풍이 닥쳐와 세상이 멸망한다고 말이야!", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("모두가 탈 거대한 우주선 같은건 없어... 관념화 장치로 모두를 꿈속으로 이주시키는게 유일한 방법이었다고!", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("헛소리를 하는군. 그런 거창한 이유가 있었다면 정부가 겨우 당신 하나만을 보냈을리가 없잖아.", "E박사", false));

        yield return StartCoroutine(ShowScript("게다가 국립 싱크탱크도 아니고 이런 외곽에 있는 조용한 화학연구소에... 상식적으로 말이 되는 이야기인가?", "E박사", false));

        yield return StartCoroutine(ShowScript("국가가 필요한건 조용한 장소였어.", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("애초에 '관념화 장치' 라는 기술이 있고, 꿈속 세계가 유일한 해결책이라며 예산을 국회에 들이밀면 도장을 찍어줄 것 같나?", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("자네 말대로 그런 작은 변두리 연구소가, 최근 왜 그렇게 많은 지원을 받았을 것 같나!", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("그 하찮은 '광물 나무' 연구를 진행시킬 수 있을 정도로 말이지.", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("정부는 조용히 나라에서 내로라하는 수석 연구원들도 연구소에 배치시켰어.", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("그런데 자네가 전부 잘라버렸잖아!", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("정부 안에서도 파벌이 갈려 지원이 끊겼는데, 결국 최종 책임은 내가 뒤집어쓰게 됐지.", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("이래도, 내가 정말 죄인으로 보이나?", "짐나르다 베임", true));

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("박사님...", "알렉스", false));

        yield return StartCoroutine(ShowScript("... 손가락만 베인 사람인줄 알았는데 의외로 멀쩡한 인간이군.", "E박사", false));

        yield return StartCoroutine(ShowScript("하지만 자네는 위원회가 자네를 투표로 해임시겼을때 관념화 장치를 켜서 모두를 괴물로 만들었지.", "E박사", false));

        yield return StartCoroutine(ShowScript("차라리 나에게 모든걸 말하고 도움을 구했다면, 내가 기꺼이 최선을 다해 도와주었을거다.", "E박사", false));

        yield return StartCoroutine(ShowScript("너가 모두를 죽인거야.", "E박사", false));

        yield return StartCoroutine(ShowScript("뭐, 이제 그런건 관심 없어.", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("당장 여기서 나간다 해도, 세상이 멀쩡할지는 이젠 모르거든.", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("이미 난 살인자다. 이제 더 이상 미련이 없어.", "짐나르다 베임", false));

        yield return StartCoroutine(ShowScript("날 죽이고 모든 일을 끝내라.", "짐나르다 베임", true));

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(ShowScript("...그래.", "E박사", true));

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
        yield return StartCoroutine(ShowScript("고맙네...", "짐나르다 베임", true));

        StartCoroutine(VaimDead());

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("하...", "E박사", false));
        yield return StartCoroutine(ShowScript("이겼구나.", "E박사", true));

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(ShowScript("이제 어떻게 되는걸까.", "E박사", true));

        StartCoroutine(Lighter());

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Final");
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
            float alpha = Mathf.Clamp(elapsedTime / fadeInDuration, 0, 1f); // 0에서 0.7 사이의 값으로 변환
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
            float alpha = 1 - Mathf.Clamp01(elapsedTime / VfadeInDuration); // 1에서 0 사이의 값으로 변환
            Vaim.color = new Color(Vaim.color.r, Vaim.color.g, Vaim.color.b, alpha);
            yield return null;
        }
    }
}

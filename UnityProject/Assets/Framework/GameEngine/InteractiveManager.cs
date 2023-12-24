using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class InteractiveManager : MonoBehaviour
{
    public Dictionary<string, List<string>> Interactives = new Dictionary<string, List<string>>();
    public Dictionary<string, int> Memory = new Dictionary<string, int>();

    public GameObject TypeEffect;
    public GameObject UI_Script;
    public GameObject UI_Tutorial;
    public Text Name;

    private TypeEffect EffectScript;
    private UI_Tutorial TutorialScript;

    private void Start()
    {
        Scene Scene = SceneManager.GetActiveScene();
        EffectScript = TypeEffect.GetComponent<TypeEffect>();
        TutorialScript = UI_Tutorial.GetComponent<UI_Tutorial>();

        //T1 
        Interactives.Add("I_cabinetT1", new List<string> { "연구실 캐비넷", "간단한 캐비넷이다. 칸마다 서류 뭉치로 가득 차 있다." });
        Interactives.Add("I_BedT1", new List<string> { "침대?", "탁자에 얇은 담요가 깔려있다. 사용감이 있는 점으로 미루어 보았을 때 꽤 오랫동안 침대로 사용된 것 같다." });
        Interactives.Add("I_TablesT1", new List<string> { "쌓인 테이블들", "테이블이 정리되어 있다. 마지막 끝 테이블에 '엔더슨 화학연구소'라는 딱지가 붙어있다. " });
        Interactives.Add("I_ConsolesT1", new List<string> { "연구 콘솔", "연구 설비들을 조작할 수 있는 콘솔. 모든 항목이 오프라인이다." });

        //B2
        Interactives.Add("I_Tutorial1B2", new List<string> { "도움말", "1" });
        Interactives.Add("I_Tutorial2B2", new List<string> { "도움말", "2" });
        Interactives.Add("I_Tutorial3B2", new List<string> { "도움말", "3" });
        Interactives.Add("I_Tutorial4B2", new List<string> { "도움말", "4" });
        Interactives.Add("I_Machine1B2", new List<string> { "이상한 실험기계", "혼란 속에서도 웅웅거리며 동작하고 있다. 안에서 무슨 일이 일어나고 있는지는 불명이다..." });
        Interactives.Add("I_B2SignB2", new List<string> { "지하 2층 표시", "이 건물의 맨 아래층인 것 같다. 누군가 메직으로 '왜 이 건물은 지하 15층이 없는거야?'라고 적어놨다. " });
        Interactives.Add("I_Key1B2", new List<string> { "관념화 열쇠", "1" });
        Interactives.Add("I_Table1B2", new List<string> { "물병이 올려진 테이블", "물병에 쌓인 먼지로 보아 지하 2층에는 사람이 뜸한 듯 하다." });
        Interactives.Add("I_Table2B2", new List<string> { "브로민이 올려진 테이블", "테이블 아래 찢어진 종교 전단지가 끼어있다. '멸망으로부터 구원받으려면 어떻게 해야할까요?'" });

        //B1
        Interactives.Add("I_TreesB1", new List<string> { "나무?", "나무에 목적을 알 수 없는 관이 잔뜩 끼워져 있다. 무엇인가 나무에 주입되는 듯 하다." });
        Interactives.Add("I_Info1B1", new List<string> { "VOC ..1", "요즘 상부로부터 의도를 알 수 없는 명령이 자꾸 떨어지고 있습니다. 모든 연구 규모를 축소하라는 방침입니다." });
        Interactives.Add("I_Info2B1", new List<string> { "VOC ..2", "짐나르다 베임, 얼마전에 들어온 그 인간이 자꾸 위원회에 기웃거린다는 말이 들립니다. " });
        Interactives.Add("I_Info3B1", new List<string> { "VOC ..3", "광물 나무는 저희가 10년이 넘게 연구해온 안건입니다. 게다가 이런 시국에는 더더욱 멈출수는 없어요!" });
        Interactives.Add("I_Info4B1", new List<string> { "사이버 대자보", "지금이라도 늦지 않았습니다. 짐나르다 베임 고문을 위원회에서 끌어내립시다! 모두의 단합이 필요합니다." });
        Interactives.Add("I_Key2B1", new List<string> { "관념화 열쇠", "2" });
        Interactives.Add("I_Tree1B1", new List<string> { "실험체 1_기록", "나무에 수은을 50% 농도로 주입했다. 항광물화항원을 HR-4 절차에 따라 투입했다. 실패." });
        Interactives.Add("I_Tree2B1", new List<string> { "실험체 2_기록", "나무에 수은 20%, 항광물화항원을 HR-4 절차에 따라 투입함. 눈에 띄는 수축이 관측됨." });
        Interactives.Add("I_Tree3B1", new List<string> { "실험체 3_기록", "나무에 수은 20%, 항광물화항원은 주입하지 않았다. 대신 DR-2 절차를 시행함. 실패." });
        Interactives.Add("I_Tree4B1", new List<string> { "실험체 4_기록", "... 베임 박사에게 이 실험이 성공했다는 것을 알리지 말도록. 방금 들은 미친 뉴스 때문에 집중할수가 없다." });
        Interactives.Add("I_Tree5B1", new List<string> { "실험체 4 백업_기록", "... 그 '관념화 실험' 이라고 하는게 원인인 것 같다. 젠장..." });
        Interactives.Add("I_Tree6B1", new List<string> { "실험체 3 백업_기록", "동일한 결과를 얻어내는 것이 불가능한 것으로 보인다. 최근 시작된 실험의 영향을 배제할 수 없다." });
        Interactives.Add("I_Tree7B1", new List<string> { "실험체 2 백업_기록", "동일한 조건에서 실험함. 경화 과정은 관찰되지 않았다. 우리가 모르는 변수가 있는 것 같다." });
        Interactives.Add("I_Tree8B1", new List<string> { "실험체 1 백업_기록", "동일한 조건에서 실험했으나 동일한 결과가 나오지 않는다." });
        Interactives.Add("I_Cabinet1B1", new List<string> { "캐비넷에 붙은 쪽지", "관념화 실험은 완전한 사기입니다! 사내 정치에 엔더슨 연구소가 무너질수는 없습니다!" });
        Interactives.Add("I_Cabinet2B1", new List<string> { "옆에 붙은 또다른 쪽지", "관념화 기술은 전인류를 위한 기술입니다! 관념화 연구팀에 합류하시려면 내선 01번으로 문의주세요!" });
        Interactives.Add("I_ExperimenterB1", new List<string> { "복잡한 실험장치", "유리관에 알 수 없는 액체들이 가득 차있다. 왼쪽에서부터 HR-3, HR-4, DR-2 라벨이 붙어있다." });
        Interactives.Add("I_Vand1B1", new List<string> { "구급상자 자판기", "쪽지가 붙어있다: 9월 2일부터 사내 연구비 충당을 위해 구급상자 보급 방침이 유상 지급으로 변경되었습니다." });
        Interactives.Add("I_Vand2B1", new List<string> { "탄산음료 자판기", "'누카-콜라'를 단돈 5000달러에 팔고있다. '이건 누구 아이디어야? 얼굴좀 보고싶군.' 이라는 쪽지가 붙어있다." });
        Interactives.Add("I_Vand3B1", new List<string> { "자판기에 붙은 신문", "기사가 오려져 붙어있다. '속보) 다음주 즈음 초거대 태양풍 방출 확실시... 지구 멸망 임박.'" });

        //G1
        Interactives.Add("I_SwitchBox1G1", new List<string> { "...전등 스위치", "보다시피 박살났다." });
        Interactives.Add("I_SwitchBox2G1", new List<string> { "부서진 전등 스위치", "부서진 방향과 형태가 동일하다. 누군가 의도적으로 파괴한 듯 하다." });
        Interactives.Add("I_Key3G1", new List<string> { "관념화 열쇠", "3" });
        Interactives.Add("I_LockerG1", new List<string> {"락커", "'이곳에 관념화 열쇠를 보관하세요'라는 경고문이 붙어있다." });

        //G2
        Interactives.Add("I_Key4G2", new List<string> { "관념화 열쇠", "4" });

        Memory.Add("T1", 4);
        Memory.Add("B2", 4);
        Memory.Add("B1", 19);
        Memory.Add("G1", 3);
        Memory.Add("G2", 0);
        Memory.Add("BOSS", 0);
        Memory.Add("Final", 0);
    }

    public void Interact ()
    {
        StartCoroutine(interact_corutine(GameManager.Instance.TriggeredEvent));
    }

    public string getName(string inID)
    {
        string key = inID + SceneManager.GetActiveScene().name;

        if (Interactives.TryGetValue(key, out List<string> values))
        {
            return values[0];
        }
        else
        {
            return "이미 얻은 기억의 조각.";
        }
    }

    private IEnumerator interact_corutine (string inID)
    {
        string key = inID + SceneManager.GetActiveScene().name;

        if (Interactives.TryGetValue(key, out List<string> values))
        {
            string command = values[0];
            string script = values[1];

            if (command == "관념화 열쇠")
            {
                if (int.Parse(script) > GameManager.Instance.keyLevel + 1)
                {
                    yield return StartCoroutine(ShowScript("저런, 아랫층 열쇠를 챙기지 않은 것 같군!", "E박사", true));
                }
                else if (int.Parse(script) < GameManager.Instance.keyLevel + 1)
                {
                    yield return StartCoroutine(ShowScript("이 열쇠는 이미 챙겼다네!", "E박사", true));
                }
                else
                {
                    GameManager.Instance.keyLevel++;
                    string talkTemp = (GameManager.Instance.keyLevel) + "번째 관념화 장치 열쇠도 챙겼군!";
                    yield return StartCoroutine(ShowScript(talkTemp, "E박사", true));
                }
            }
            else if (command == "도움말")
            {
                StartCoroutine(ShowTutorial(int.Parse(script)));
            }
            else
            {
                yield return StartCoroutine(ShowScript(script, command, true));

                Memory[SceneManager.GetActiveScene().name] -= 1;
                Interactives.Remove(key);
            }
        }
        else
        {
            yield return StartCoroutine(ShowScript("여긴 아까 살펴봤었어요...", "알렉스", true));
        }
    }

    private IEnumerator ShowTutorial(int index)
    {
        UI_Tutorial.SetActive(true);
        TutorialScript.SetTutorialImage(index);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Input.GetButtonDown("Next"));
        UI_Tutorial.SetActive(false);
    }

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

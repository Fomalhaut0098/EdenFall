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
        Interactives.Add("I_cabinetT1", new List<string> { "������ ĳ���", "������ ĳ����̴�. ĭ���� ���� ��ġ�� ���� �� �ִ�." });
        Interactives.Add("I_BedT1", new List<string> { "ħ��?", "Ź�ڿ� ���� ��䰡 ����ִ�. ��밨�� �ִ� ������ �̷�� ������ �� �� �������� ħ��� ���� �� ����." });
        Interactives.Add("I_TablesT1", new List<string> { "���� ���̺��", "���̺��� �����Ǿ� �ִ�. ������ �� ���̺� '������ ȭ�п�����'��� ������ �پ��ִ�. " });
        Interactives.Add("I_ConsolesT1", new List<string> { "���� �ܼ�", "���� ������� ������ �� �ִ� �ܼ�. ��� �׸��� ���������̴�." });

        //B2
        Interactives.Add("I_Tutorial1B2", new List<string> { "����", "1" });
        Interactives.Add("I_Tutorial2B2", new List<string> { "����", "2" });
        Interactives.Add("I_Tutorial3B2", new List<string> { "����", "3" });
        Interactives.Add("I_Tutorial4B2", new List<string> { "����", "4" });
        Interactives.Add("I_Machine1B2", new List<string> { "�̻��� ������", "ȥ�� �ӿ����� �����Ÿ��� �����ϰ� �ִ�. �ȿ��� ���� ���� �Ͼ�� �ִ����� �Ҹ��̴�..." });
        Interactives.Add("I_B2SignB2", new List<string> { "���� 2�� ǥ��", "�� �ǹ��� �� �Ʒ����� �� ����. ������ �������� '�� �� �ǹ��� ���� 15���� ���°ž�?'��� �������. " });
        Interactives.Add("I_Key1B2", new List<string> { "����ȭ ����", "1" });
        Interactives.Add("I_Table1B2", new List<string> { "������ �÷��� ���̺�", "������ ���� ������ ���� ���� 2������ ����� ���� �� �ϴ�." });
        Interactives.Add("I_Table2B2", new List<string> { "��ι��� �÷��� ���̺�", "���̺� �Ʒ� ������ ���� �������� �����ִ�. '������κ��� ������������ ��� �ؾ��ұ��?'" });

        //B1
        Interactives.Add("I_TreesB1", new List<string> { "����?", "������ ������ �� �� ���� ���� �ܶ� ������ �ִ�. �����ΰ� ������ ���ԵǴ� �� �ϴ�." });
        Interactives.Add("I_Info1B1", new List<string> { "VOC ..1", "���� ��ηκ��� �ǵ��� �� �� ���� ����� �ڲ� �������� �ֽ��ϴ�. ��� ���� �Ը� ����϶�� ��ħ�Դϴ�." });
        Interactives.Add("I_Info2B1", new List<string> { "VOC ..2", "�������� ����, ������ ���� �� �ΰ��� �ڲ� ����ȸ�� ����Ÿ��ٴ� ���� �鸳�ϴ�. " });
        Interactives.Add("I_Info3B1", new List<string> { "VOC ..3", "���� ������ ���� 10���� �Ѱ� �����ؿ� �Ȱ��Դϴ�. �Դٰ� �̷� �ñ����� ������ ������� �����!" });
        Interactives.Add("I_Info4B1", new List<string> { "���̹� ���ں�", "�����̶� ���� �ʾҽ��ϴ�. �������� ���� ���� ����ȸ���� ������ô�! ����� ������ �ʿ��մϴ�." });
        Interactives.Add("I_Key2B1", new List<string> { "����ȭ ����", "2" });
        Interactives.Add("I_Tree1B1", new List<string> { "����ü 1_���", "������ ������ 50% �󵵷� �����ߴ�. �ױ���ȭ�׿��� HR-4 ������ ���� �����ߴ�. ����." });
        Interactives.Add("I_Tree2B1", new List<string> { "����ü 2_���", "������ ���� 20%, �ױ���ȭ�׿��� HR-4 ������ ���� ������. ���� ��� ������ ������." });
        Interactives.Add("I_Tree3B1", new List<string> { "����ü 3_���", "������ ���� 20%, �ױ���ȭ�׿��� �������� �ʾҴ�. ��� DR-2 ������ ������. ����." });
        Interactives.Add("I_Tree4B1", new List<string> { "����ü 4_���", "... ���� �ڻ翡�� �� ������ �����ߴٴ� ���� �˸��� ������. ��� ���� ��ģ ���� ������ �����Ҽ��� ����." });
        Interactives.Add("I_Tree5B1", new List<string> { "����ü 4 ���_���", "... �� '����ȭ ����' �̶�� �ϴ°� ������ �� ����. ����..." });
        Interactives.Add("I_Tree6B1", new List<string> { "����ü 3 ���_���", "������ ����� ���� ���� �Ұ����� ������ ���δ�. �ֱ� ���۵� ������ ������ ������ �� ����." });
        Interactives.Add("I_Tree7B1", new List<string> { "����ü 2 ���_���", "������ ���ǿ��� ������. ��ȭ ������ �������� �ʾҴ�. �츮�� �𸣴� ������ �ִ� �� ����." });
        Interactives.Add("I_Tree8B1", new List<string> { "����ü 1 ���_���", "������ ���ǿ��� ���������� ������ ����� ������ �ʴ´�." });
        Interactives.Add("I_Cabinet1B1", new List<string> { "ĳ��ݿ� ���� ����", "����ȭ ������ ������ ����Դϴ�! �系 ��ġ�� ������ �����Ұ� ���������� �����ϴ�!" });
        Interactives.Add("I_Cabinet2B1", new List<string> { "���� ���� �Ǵٸ� ����", "����ȭ ����� ���η��� ���� ����Դϴ�! ����ȭ �������� �շ��Ͻ÷��� ���� 01������ �����ּ���!" });
        Interactives.Add("I_ExperimenterB1", new List<string> { "������ ������ġ", "�������� �� �� ���� ��ü���� ���� ���ִ�. ���ʿ������� HR-3, HR-4, DR-2 ���� �پ��ִ�." });
        Interactives.Add("I_Vand1B1", new List<string> { "���޻��� ���Ǳ�", "������ �پ��ִ�: 9�� 2�Ϻ��� �系 ������ ����� ���� ���޻��� ���� ��ħ�� ���� �������� ����Ǿ����ϴ�." });
        Interactives.Add("I_Vand2B1", new List<string> { "ź������ ���Ǳ�", "'��ī-�ݶ�'�� �ܵ� 5000�޷��� �Ȱ��ִ�. '�̰� ���� ���̵���? ���� ����ͱ�.' �̶�� ������ �پ��ִ�." });
        Interactives.Add("I_Vand3B1", new List<string> { "���Ǳ⿡ ���� �Ź�", "��簡 ������ �پ��ִ�. '�Ӻ�) ������ ���� �ʰŴ� �¾�ǳ ���� Ȯ�ǽ�... ���� ��� �ӹ�.'" });

        //G1
        Interactives.Add("I_SwitchBox1G1", new List<string> { "...���� ����ġ", "���ٽ��� �ڻ쳵��." });
        Interactives.Add("I_SwitchBox2G1", new List<string> { "�μ��� ���� ����ġ", "�μ��� ����� ���°� �����ϴ�. ������ �ǵ������� �ı��� �� �ϴ�." });
        Interactives.Add("I_Key3G1", new List<string> { "����ȭ ����", "3" });
        Interactives.Add("I_LockerG1", new List<string> {"��Ŀ", "'�̰��� ����ȭ ���踦 �����ϼ���'��� ����� �پ��ִ�." });

        //G2
        Interactives.Add("I_Key4G2", new List<string> { "����ȭ ����", "4" });

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
            return "�̹� ���� ����� ����.";
        }
    }

    private IEnumerator interact_corutine (string inID)
    {
        string key = inID + SceneManager.GetActiveScene().name;

        if (Interactives.TryGetValue(key, out List<string> values))
        {
            string command = values[0];
            string script = values[1];

            if (command == "����ȭ ����")
            {
                if (int.Parse(script) > GameManager.Instance.keyLevel + 1)
                {
                    yield return StartCoroutine(ShowScript("����, �Ʒ��� ���踦 ì���� ���� �� ����!", "E�ڻ�", true));
                }
                else if (int.Parse(script) < GameManager.Instance.keyLevel + 1)
                {
                    yield return StartCoroutine(ShowScript("�� ����� �̹� ì��ٳ�!", "E�ڻ�", true));
                }
                else
                {
                    GameManager.Instance.keyLevel++;
                    string talkTemp = (GameManager.Instance.keyLevel) + "��° ����ȭ ��ġ ���赵 ì�屺!";
                    yield return StartCoroutine(ShowScript(talkTemp, "E�ڻ�", true));
                }
            }
            else if (command == "����")
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
            yield return StartCoroutine(ShowScript("���� �Ʊ� ����þ����...", "�˷���", true));
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
        // RŰ�� ���� ������ ���
        yield return new WaitUntil(() => Input.GetButtonDown("Next"));

        if (lastword)
        {
            UI_Script.SetActive(false);
        }
    }

}

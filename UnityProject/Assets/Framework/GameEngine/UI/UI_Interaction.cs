using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ȣ�ۿ� ������ �������� ǥ���ϴ� UI
// �䱸 ���Ӽ� | InteractionManager | ObjectScanArea

public class UI_Interaction : MonoBehaviour
{
    // UI�� ǥ���� ������ �����ִ� ����(���߿�)
    public string dev_id = "";
    public string dev_string = "";

    // �������� ��������Ʈ �ҷ�����
    public Text UI_Interaction_Text;
    public GameObject ObjectScanArea;
    public GameObject InteractiveManager;
    private ObjectScanArea ObjectScanCode;
    private InteractiveManager InteractiveScript;
   

    void Start()
    {
        //�������� ��������Ʈ �ڵ� �ҷ�����
        ObjectScanCode = ObjectScanArea.GetComponent<ObjectScanArea>();
        InteractiveScript = InteractiveManager.GetComponent<InteractiveManager>();
    }

    void Update()
    {
        //ObjectScanArea���� �ҷ��� ���� ����ִ� ������Ʈ ID ǥ���ϱ�
        dev_id = ObjectScanCode.TriggeredInteracterEvent;

        dev_string = InteractiveScript.getName(dev_id);

        // UI�� ǥ���� ������ �����ִ� ������ ǥ��(���߿�)
        UI_Interaction_Text.text = dev_string;
    }
}

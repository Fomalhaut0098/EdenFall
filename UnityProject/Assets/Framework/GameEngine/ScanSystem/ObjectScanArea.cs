using UnityEngine;
using UnityEngine.UI;

// �����۰� ��ȣ�ۿ� ������ ������Ʈ���� �����ϴ� Doctor ���� ��������Ʈ
// �䱸 ���Ӽ� | UI_Interface | InputManager | Doctor | 

public class ObjectScanArea : MonoBehaviour
{
    //������ ������Ʈ�� ID�� ����Ǵ� ����
    public string TriggeredInteracterEvent = "";

    //������ ��������Ʈ �ҷ�����
    public GameObject UI_Interaction;
    public GameObject InputManager;

    private InputManager keyInputScript;

    private void Start()
    {
        //������ ��������Ʈ �ڵ� �ҷ�����
        keyInputScript = InputManager.GetComponent<InputManager>();
    }

    //������Ʈ�� ������� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ȣ�ۿ� ������ ������Ʈ�� ���� ���
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactives"))
        {
            Debug.Log($"��ȣ�ۿ� ������ ������Ʈ ������: {collision.gameObject.name}");
            TriggeredInteracterEvent = collision.gameObject.name;
            GameManager.Instance.TriggeredEvent = TriggeredInteracterEvent;
        }

    }

    // ������Ʈ���� �������� ��
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactives"))
        {
            Debug.Log($"��ȣ�ۿ� ������ ������Ʈ ������ ���: {collision.gameObject.name}");

            //���� ������ ������Ʈ�� ���������� ���� ������Ʈ��� ��Ȳ�� ����� ����
            if (TriggeredInteracterEvent == collision.gameObject.name)
            {
                //��Ȳ�� ����Ǿ����Ƿ� ���� ����
                TriggeredInteracterEvent = "";
                GameManager.Instance.TriggeredEvent = "";
            }
        }
    }

    private void Update()
    {
        //������Ʈ ���� ���ο� ���� ��ȣ�ۿ�â ǥ��/�����
        if (TriggeredInteracterEvent != "" && keyInputScript.iv_active == false)
        {
            UI_Interaction.SetActive(true);
        }
        else
        {
            UI_Interaction.SetActive(false);
        }
    }
}

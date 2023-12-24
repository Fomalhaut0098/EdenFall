using UnityEngine;
using UnityEngine.UI;

// 아이템과 상호작용 가능한 오브젝트들을 감지하는 Doctor 하위 스프라이트
// 요구 종속성 | UI_Interface | InputManager | Doctor | 

public class ObjectScanArea : MonoBehaviour
{
    //감지된 오브젝트의 ID가 저장되는 변수
    public string TriggeredInteracterEvent = "";

    //종속적 스프라이트 불러오기
    public GameObject UI_Interaction;
    public GameObject InputManager;

    private InputManager keyInputScript;

    private void Start()
    {
        //종속적 스프라이트 코드 불러오기
        keyInputScript = InputManager.GetComponent<InputManager>();
    }

    //오브젝트와 닿았을때 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 상호작용 가능한 오브젝트와 닿은 경우
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactives"))
        {
            Debug.Log($"상호작용 가능한 오브젝트 감지됨: {collision.gameObject.name}");
            TriggeredInteracterEvent = collision.gameObject.name;
            GameManager.Instance.TriggeredEvent = TriggeredInteracterEvent;
        }

    }

    // 오브젝트에서 떨어졌을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactives"))
        {
            Debug.Log($"상호작용 가능한 오브젝트 범위를 벗어남: {collision.gameObject.name}");

            //만약 떨어진 오브젝트가 마지막으로 닿은 오브젝트라면 상황이 종료된 것임
            if (TriggeredInteracterEvent == collision.gameObject.name)
            {
                //상황이 종료되었으므로 종료 선언
                TriggeredInteracterEvent = "";
                GameManager.Instance.TriggeredEvent = "";
            }
        }
    }

    private void Update()
    {
        //오브젝트 닿음 여부에 따라 상호작용창 표시/숨기기
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

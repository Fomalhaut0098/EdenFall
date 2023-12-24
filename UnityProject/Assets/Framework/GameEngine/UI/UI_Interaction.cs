using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 상호작용 가능한 아이템을 표시하는 UI
// 요구 종속성 | InteractionManager | ObjectScanArea

public class UI_Interaction : MonoBehaviour
{
    // UI에 표시할 문구를 보여주는 변수(개발용)
    public string dev_id = "";
    public string dev_string = "";

    // 종속적인 스프라이트 불러오기
    public Text UI_Interaction_Text;
    public GameObject ObjectScanArea;
    public GameObject InteractiveManager;
    private ObjectScanArea ObjectScanCode;
    private InteractiveManager InteractiveScript;
   

    void Start()
    {
        //종속적인 스프라이트 코드 불러오기
        ObjectScanCode = ObjectScanArea.GetComponent<ObjectScanArea>();
        InteractiveScript = InteractiveManager.GetComponent<InteractiveManager>();
    }

    void Update()
    {
        //ObjectScanArea에서 불러온 현재 닿아있는 오브젝트 ID 표시하기
        dev_id = ObjectScanCode.TriggeredInteracterEvent;

        dev_string = InteractiveScript.getName(dev_id);

        // UI에 표시할 문구를 보여주는 변수에 표시(개발용)
        UI_Interaction_Text.text = dev_string;
    }
}

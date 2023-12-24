using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    //대사를 받아와 저장할 변수
    public string Message = "";
    public bool finalScript = false;

    //글자를 출력할 속도
    public float CharacterPerSec = 2f;

    private int index;

    //종속적 오브젝트 추가
    public GameObject UI_NextButton;
    public Text inScript;

    public void StartTyping(string inConversation, bool inFinalScript)
    {
        Message = inConversation;
        finalScript = inFinalScript;
        EffectStart();
    }

    private void EffectStart()
    {
        inScript.text = "";
        index = 0;
        UI_NextButton.SetActive(false);
        Invoke("Effecting" , 1/CharacterPerSec);
    }

    private void Effecting()
    {
        if (inScript.text == Message)
        {
            EffectEnd();
            return;
        }

        inScript.text += Message[index];
        index++;
        
        Invoke("Effecting", 1 / CharacterPerSec);
    }

    private void EffectEnd()
    {
        UI_NextButton.SetActive(true);
    }
}

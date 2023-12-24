using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    //��縦 �޾ƿ� ������ ����
    public string Message = "";
    public bool finalScript = false;

    //���ڸ� ����� �ӵ�
    public float CharacterPerSec = 2f;

    private int index;

    //������ ������Ʈ �߰�
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

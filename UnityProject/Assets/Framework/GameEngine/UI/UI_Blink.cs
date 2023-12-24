using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Blink : MonoBehaviour
{
    public Image Blink;
    public float fadeInTime = 0.1f;  // ������ ���������� ��ȯ�ϴ� �� �ɸ��� �ð�
    public float fadeOutTime = 0.1f; // �������� �������� ��ȯ�ϴ� �� �ɸ��� �ð�

    public void BlinkStart()
    {
        StartCoroutine(FlashScreen());
    }

    IEnumerator FlashScreen()
    {
        yield return StartCoroutine(FadeToClear());

        // ������ ����������
        yield return StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeInTime);
            Blink.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    IEnumerator FadeToClear()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - elapsedTime / fadeOutTime);
            Blink.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}

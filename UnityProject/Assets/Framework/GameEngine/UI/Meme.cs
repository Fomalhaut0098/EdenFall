using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU
// DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU
// DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU
// DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU
// DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU
// DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU
// DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU DOCTOR IS WATCHING YOU

public class Meme : MonoBehaviour
{
    public RectTransform uiElement;

    void Start()
    {
        MoveToPosition(new Vector2(-2320, 490), 30f);
    }

    public void MoveToPosition(Vector2 targetPosition, float speed)
    {
        StartCoroutine(MoveOverSpeed(uiElement, targetPosition, speed));
    }

    private IEnumerator MoveOverSpeed(RectTransform objectToMove, Vector2 end, float speed)
    {
        while (Vector2.Distance(objectToMove.anchoredPosition, end) > 0.01f)
        {
            objectToMove.anchoredPosition = Vector2.MoveTowards(objectToMove.anchoredPosition, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        objectToMove.anchoredPosition = end; // 최종 위치로 설정

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Reborn());
    }

    public void respawnButton()
    {
        GameManager.Instance.hp = 4;
        GameManager.Instance.keyLevel = GameManager.Instance.re_keyLevel;
        GameManager.Instance.Battery = GameManager.Instance.re_Battery;
        GameManager.Instance.InteractiveManager.Interactives = GameManager.Instance.re_Interactives;
        GameManager.Instance.InteractiveManager.Memory = GameManager.Instance.re_Memory;

        for (int i = 0; i < 4; i++)
        {
            GameManager.Instance.leftAmmo[i] = GameManager.Instance.re_leftAmmo[i];
        }

        GameManager.Instance.notDead = true;
        GameManager.Instance.AudioSource.Play();
        SceneManager.LoadScene(GameManager.Instance.preScene);
    }

    private IEnumerator Reborn()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Next"));

        GameManager.Instance.hp = 4;
        GameManager.Instance.keyLevel = GameManager.Instance.re_keyLevel;
        GameManager.Instance.Battery = GameManager.Instance.re_Battery;
        GameManager.Instance.InteractiveManager.Interactives = GameManager.Instance.re_Interactives;
        GameManager.Instance.InteractiveManager.Memory = GameManager.Instance.re_Memory;

        for (int i = 0; i < 4; i++)
        {
            GameManager.Instance.leftAmmo[i] = GameManager.Instance.re_leftAmmo[i];
        }

        GameManager.Instance.notDead = true;
        GameManager.Instance.AudioSource.Play();
        SceneManager.LoadScene(GameManager.Instance.preScene);

    }
}

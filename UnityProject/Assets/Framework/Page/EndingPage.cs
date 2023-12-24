using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingPage : MonoBehaviour
{
    public GameObject GameTitle;
    public GameObject TheEnd;
    public GameObject Credit;

    void Start()
    {
        StartCoroutine(Ending());
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(3f);

        TheEnd.SetActive(true);

        yield return new WaitForSeconds(5f);

       GameTitle.SetActive(true);

        yield return new WaitForSeconds(3f);

        Credit.SetActive(true);

        yield return new WaitForSeconds(15f);

        SceneManager.LoadScene("MainScreen");
    }
}

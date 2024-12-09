using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Image blackScreen;
    public float alphaValue;

    public int sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        blackScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadingScene()
    {
        blackScreen.gameObject.SetActive(true);
        StartCoroutine(FadeToBlack());
        Invoke("NewScene", 1.5f);
    }

    public IEnumerator FadeToBlack()
    {
        alphaValue = alphaValue + .1f;

        blackScreen.color = new Color(0, 0, 0, alphaValue);

        yield return new WaitForSeconds(.1f);

        StartCoroutine(FadeToBlack());
    }

    public void NewScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}

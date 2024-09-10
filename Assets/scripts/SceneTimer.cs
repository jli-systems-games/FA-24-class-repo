using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 10f; // how long to wait before loading

    [SerializeField]
    private string sceneNameToLoad;

    private float timeElapsed;

    private void Update()
    {
        timeElapsed += Time.deltaTime; //timer

        if (timeElapsed > delayBeforeLoading)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
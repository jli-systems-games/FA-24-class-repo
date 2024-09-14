using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameLevelController : MonoBehaviour
{
    public TextMeshPro scoreText;
    private int score = 0;

    public GameObject platformUnit;          
    public Transform[] generateAnchor;      

    private int currentAnchorIndex;          
    private int nextAnchorIndex;             
    private int anchorCount = 5;           

    private void Start()
    {
        scoreText.text = "0";
        currentAnchorIndex = 0;
        StartCoroutine(SpawnPlatform());
    }

    IEnumerator SpawnPlatform()
    {
        while (true)
        {
            GameObject newPlatform = Instantiate(platformUnit, generateAnchor[currentAnchorIndex].position, Quaternion.identity);

            // 随机改变新生成物体的scale.x
            float randomScaleX = Random.Range(0.9f, 3.5f);
            newPlatform.transform.localScale = new Vector3(randomScaleX, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);

            nextAnchorIndex = Mathf.Clamp(currentAnchorIndex + Random.Range(-1, 2), 0, anchorCount - 1);

            // 更新当前生成点的索引
            currentAnchorIndex = nextAnchorIndex;

            // 调整下一次生成的等待时间
            yield return new WaitForSeconds((2 * randomScaleX) / 5);
        }
    }

    public void AddScore()
    { 
        score ++;
        scoreText.text = score.ToString();
    }
}

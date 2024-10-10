using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class CookingArea : MonoBehaviour
{
    public string nextSceneName;  // 成功时跳转的场景名称
    public string failSceneName;  // 失败时跳转的场景名称
    public GameObject juiceBlender;  // 带有果汁的 Blender Sprite
    public GameObject blender;       // 普通的 Blender Sprite

    [System.Serializable]
    public class DrinkRecipe
    {
        public string drinkName;  
        public List<string> correctFruits;  
    }

    public List<DrinkRecipe> drinkRecipes; // 饮品配方列表
    private int currentDrinkIndex = 0; // 当前饮品的索引
    private List<string> placedFruits = new List<string>(); // 已经放入的水果列表
    private List<GameObject> placedFruitObjects = new List<GameObject>(); // 保存放入的水果对象
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>(); // 记录每个水果的初始位置
    private int fruitCount = 0; 
    private bool gameOver = false; 
    private bool isWaitingForNextDrink = false; 
    private bool canPlaceFruits = true; 

    public float timeLimit = 30f; 
    private float remainingTime; // 剩余时间

    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI statusText; 

    private GameObject fruitsParent; // 父对象，包含所有水果

    private void Start()
    {
        remainingTime = timeLimit; // 初始化倒计时

        // 获取水果的父对象 "FruitsParent"
        fruitsParent = GameObject.Find("FruitsParent");

        // 记录每个水果的初始位置，遍历父对象下的所有水果
        foreach (Transform fruit in fruitsParent.transform)
        {
            originalPositions[fruit.gameObject] = fruit.position;
        }

        
        juiceBlender.SetActive(false);

        StartDrink(); // 开始第一个饮品
    }

    private void Update()
    {
        
        if (gameOver || isWaitingForNextDrink)
            return;

        
        remainingTime -= Time.deltaTime;

        
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(remainingTime).ToString(); 
        }

        // 检查时间是否到达零
        if (remainingTime <= 0)
        {
            Debug.Log("时间到！游戏失败。");
            Fail(); // 调用失败 跳转到失败场景
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (gameOver || isWaitingForNextDrink || !canPlaceFruits)
            return;

        // 每次放入水果，隐藏并加入放入的水果列表
        placedFruits.Add(other.tag);
        placedFruitObjects.Add(other.gameObject); 
        other.gameObject.SetActive(false); 
        fruitCount++; 

        // 显示带有果汁的 Blender，不隐藏普通的 Blender
        juiceBlender.SetActive(true);

        // 当放入了3个水果时，进行判断
        if (fruitCount >= 3)
        {
            canPlaceFruits = false; // 暂停放置水果
            CheckDrinkResult(); // 检查当前饮品结果
        }
    }

    private void StartDrink()
    {
        
        isWaitingForNextDrink = false; 
        canPlaceFruits = true; 
        fruitCount = 0; 

        
        foreach (Transform fruit in fruitsParent.transform)
        {
            GameObject fruitObject = fruit.gameObject;
            fruitObject.transform.position = originalPositions[fruitObject]; 
            fruitObject.SetActive(true); 
        }

        placedFruitObjects.Clear(); 
        placedFruits.Clear(); 

        // 重置 Blender 的显示状态
        juiceBlender.SetActive(false);

        statusText.text = "Making drink: " + drinkRecipes[currentDrinkIndex].drinkName; 
        Debug.Log("开始制作饮品: " + drinkRecipes[currentDrinkIndex].drinkName);
    }

    private void CheckDrinkResult()
    {
        
        foreach (string correctFruit in drinkRecipes[currentDrinkIndex].correctFruits)
        {
            if (!placedFruits.Contains(correctFruit))
            {
                Debug.Log("失败！饮品 " + drinkRecipes[currentDrinkIndex].drinkName + " 制作错误。");
                Fail(); 
                return;
            }
        }

        Debug.Log("成功！饮品 " + drinkRecipes[currentDrinkIndex].drinkName + " 完成");

        currentDrinkIndex++;

        // 判断是否是最后一个饮品
        if (currentDrinkIndex < drinkRecipes.Count)
        {
            StartCoroutine(WaitBeforeNextDrink()); // 等待3秒后开始下一个饮品
        }
        else
        {
            Success(); // 全部饮品制作完成，游戏成功
        }
    }

    IEnumerator WaitBeforeNextDrink()
    {
        isWaitingForNextDrink = true; // 标记进入等待状态
        statusText.text = "Waiting for the next smoothie..."; 
        yield return new WaitForSeconds(3f); // 等待3秒

        StartDrink(); // 3秒后开始下一个饮品
    }

    private void Success()
    {
        gameOver = true;
        statusText.text = "All smoothies completed successfully!"; 
        Debug.Log("游戏成功！");

        // 开始协程，3秒后切换场景
        StartCoroutine(ProceedToNextSceneAfterDelay(3f));
    }

    private IEnumerator ProceedToNextSceneAfterDelay(float delay)
    {
        // 等待指定的秒数
        yield return new WaitForSeconds(delay);

        
        SceneManager.LoadScene(nextSceneName);
    }

    private void Fail()
    {
        gameOver = true;
        statusText.text = "Game over!"; 
        Debug.Log("游戏失败！");

        
        SceneManager.LoadScene(failSceneName);
    }
}

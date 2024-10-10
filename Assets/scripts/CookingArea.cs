using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class CookingArea : MonoBehaviour
{
    public string nextSceneName;  // �ɹ�ʱ��ת�ĳ�������
    public string failSceneName;  // ʧ��ʱ��ת�ĳ�������
    public GameObject juiceBlender;  // ���й�֭�� Blender Sprite
    public GameObject blender;       // ��ͨ�� Blender Sprite

    [System.Serializable]
    public class DrinkRecipe
    {
        public string drinkName;  
        public List<string> correctFruits;  
    }

    public List<DrinkRecipe> drinkRecipes; // ��Ʒ�䷽�б�
    private int currentDrinkIndex = 0; // ��ǰ��Ʒ������
    private List<string> placedFruits = new List<string>(); // �Ѿ������ˮ���б�
    private List<GameObject> placedFruitObjects = new List<GameObject>(); // ��������ˮ������
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>(); // ��¼ÿ��ˮ���ĳ�ʼλ��
    private int fruitCount = 0; 
    private bool gameOver = false; 
    private bool isWaitingForNextDrink = false; 
    private bool canPlaceFruits = true; 

    public float timeLimit = 30f; 
    private float remainingTime; // ʣ��ʱ��

    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI statusText; 

    private GameObject fruitsParent; // �����󣬰�������ˮ��

    private void Start()
    {
        remainingTime = timeLimit; // ��ʼ������ʱ

        // ��ȡˮ���ĸ����� "FruitsParent"
        fruitsParent = GameObject.Find("FruitsParent");

        // ��¼ÿ��ˮ���ĳ�ʼλ�ã������������µ�����ˮ��
        foreach (Transform fruit in fruitsParent.transform)
        {
            originalPositions[fruit.gameObject] = fruit.position;
        }

        
        juiceBlender.SetActive(false);

        StartDrink(); // ��ʼ��һ����Ʒ
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

        // ���ʱ���Ƿ񵽴���
        if (remainingTime <= 0)
        {
            Debug.Log("ʱ�䵽����Ϸʧ�ܡ�");
            Fail(); // ����ʧ�� ��ת��ʧ�ܳ���
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (gameOver || isWaitingForNextDrink || !canPlaceFruits)
            return;

        // ÿ�η���ˮ�������ز���������ˮ���б�
        placedFruits.Add(other.tag);
        placedFruitObjects.Add(other.gameObject); 
        other.gameObject.SetActive(false); 
        fruitCount++; 

        // ��ʾ���й�֭�� Blender����������ͨ�� Blender
        juiceBlender.SetActive(true);

        // ��������3��ˮ��ʱ�������ж�
        if (fruitCount >= 3)
        {
            canPlaceFruits = false; // ��ͣ����ˮ��
            CheckDrinkResult(); // ��鵱ǰ��Ʒ���
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

        // ���� Blender ����ʾ״̬
        juiceBlender.SetActive(false);

        statusText.text = "Making drink: " + drinkRecipes[currentDrinkIndex].drinkName; 
        Debug.Log("��ʼ������Ʒ: " + drinkRecipes[currentDrinkIndex].drinkName);
    }

    private void CheckDrinkResult()
    {
        
        foreach (string correctFruit in drinkRecipes[currentDrinkIndex].correctFruits)
        {
            if (!placedFruits.Contains(correctFruit))
            {
                Debug.Log("ʧ�ܣ���Ʒ " + drinkRecipes[currentDrinkIndex].drinkName + " ��������");
                Fail(); 
                return;
            }
        }

        Debug.Log("�ɹ�����Ʒ " + drinkRecipes[currentDrinkIndex].drinkName + " ���");

        currentDrinkIndex++;

        // �ж��Ƿ������һ����Ʒ
        if (currentDrinkIndex < drinkRecipes.Count)
        {
            StartCoroutine(WaitBeforeNextDrink()); // �ȴ�3���ʼ��һ����Ʒ
        }
        else
        {
            Success(); // ȫ����Ʒ������ɣ���Ϸ�ɹ�
        }
    }

    IEnumerator WaitBeforeNextDrink()
    {
        isWaitingForNextDrink = true; // ��ǽ���ȴ�״̬
        statusText.text = "Waiting for the next smoothie..."; 
        yield return new WaitForSeconds(3f); // �ȴ�3��

        StartDrink(); // 3���ʼ��һ����Ʒ
    }

    private void Success()
    {
        gameOver = true;
        statusText.text = "All smoothies completed successfully!"; 
        Debug.Log("��Ϸ�ɹ���");

        // ��ʼЭ�̣�3����л�����
        StartCoroutine(ProceedToNextSceneAfterDelay(3f));
    }

    private IEnumerator ProceedToNextSceneAfterDelay(float delay)
    {
        // �ȴ�ָ��������
        yield return new WaitForSeconds(delay);

        
        SceneManager.LoadScene(nextSceneName);
    }

    private void Fail()
    {
        gameOver = true;
        statusText.text = "Game over!"; 
        Debug.Log("��Ϸʧ�ܣ�");

        
        SceneManager.LoadScene(failSceneName);
    }
}

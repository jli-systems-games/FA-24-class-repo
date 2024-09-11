using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    public List<GameObject> hands; // 拖入手物体的 GameObject 列表

    public GameObject win;

    private List<HandsPop> handsPopComponents = new List<HandsPop>(); // 存储 HandsPop 组件的列表
    private int clearedCount = 0; // 记录已被击打的手数量

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager")
           .GetComponent<GameManager>();


        win.SetActive(false);

        // 初始化 handsPopComponents 列表
        foreach (GameObject handObject in hands)
        {
            Debug.Log("HandsAppear");
            HandsPop handsPop = handObject.GetComponent<HandsPop>();
            if (handsPop != null)
            {
                handsPopComponents.Add(handsPop);
                handObject.SetActive(false); // 确保初始状态为非激活
            }
            else
            {
                Debug.LogError($"Hand GameObject {handObject.name} does not have a HandsPop component.");
            }
        }

        StartNextHand(); // 开始第一个手的弹出
    }

    // 开始下一个随机弹出的手
    private void StartNextHand()
    {
        if (clearedCount >= hands.Count)
        {
            Debug.Log("HandsCleared!");
            win.SetActive(true);
            _gameManager.ChangeState(GameState.Transition);
            return;
        }

        // 随机抽取一个未被击打的手
        List<HandsPop> availableHands = new List<HandsPop>();
        foreach (HandsPop hand in handsPopComponents)
        {
            if (!hand.gameObject.activeSelf)
            {
                availableHands.Add(hand);
            }
        }

        if (availableHands.Count > 0)
        {
            int randomIndex = Random.Range(0, availableHands.Count);
            HandsPop selectedHand = availableHands[randomIndex];
            selectedHand.gameObject.SetActive(true);
            selectedHand.Initialize(this);
            StartCoroutine(selectedHand.ShowHand());
        }
        else
        {
            Debug.Log("No available hands to activate.");
        }
    }

    // 当一个手被击打后
    public void OnHandCleared()
    {
        clearedCount++;
        StartNextHand();
    }
}

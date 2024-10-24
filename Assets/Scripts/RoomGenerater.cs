using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerater : MonoBehaviour
{
    public GameObject[] rooms;
    
    public Transform playerTransform;

    private Dictionary<int, GameObject> corridors = new Dictionary<int, GameObject>();
    private int currentSegment = 0;    // 玩家所在的走廊区段
    private int segmentLength = 16;     // 每个走廊区段的长度

    private void Start()
    {
        GenerateSegment(0);
        GenerateSegment(-1);
        GenerateSegment(1);
    }

    void Update()
    {
        // 检查玩家是否进入了新的走廊区段
        int newSegment = Mathf.FloorToInt(playerTransform.position.x / segmentLength);

        if (newSegment != currentSegment)
        {
            UpdateSegments(newSegment);
        }
    }
    void UpdateSegments(int newSegment)
    {
        // 更新当前的走廊区段
        currentSegment = newSegment;

        // 检查是否需要生成新的走廊并删除旧的
        if (!corridors.ContainsKey(newSegment))
        {
            GenerateSegment(newSegment);
        }

        // 当玩家向左移动（进入左边的区段）
        if (!corridors.ContainsKey(newSegment - 1))
        {
            GenerateSegment(newSegment - 1);
        }

        // 当玩家向右移动（进入右边的区段）
        if (!corridors.ContainsKey(newSegment + 1))
        {
            GenerateSegment(newSegment + 1);
        }

        // 删除超过范围的走廊，保持只有左右两侧的走廊
        List<int> segmentsToRemove = new List<int>();
        foreach (int segment in corridors.Keys)
        {
            if (Mathf.Abs(segment - newSegment) > 1)
            {
                segmentsToRemove.Add(segment);
            }
        }

        foreach (int segment in segmentsToRemove)
        {
            Destroy(corridors[segment]);
            corridors.Remove(segment);
        }
    }

    void GenerateSegment(int segment)
    {
        // 随机从 corridorPrefabs 数组中选择一个预制体
        int randomIndex = Random.Range(0, rooms.Length);
        GameObject selectedPrefab = rooms[randomIndex];

        // 生成走廊单元
        Vector3 position = new Vector3(segment * segmentLength, 0, -0.8f); // 根据区段位置计算生成位置
        GameObject corridor = Instantiate(selectedPrefab, position, Quaternion.identity);

        // 存储区段信息
        corridors[segment] = corridor;
    }
}

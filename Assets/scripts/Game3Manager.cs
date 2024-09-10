using UnityEngine;

public class Game3Manager : MonoBehaviour
{
    public GameObject[] puzzlePieces; // 将拼图块拖入此数组
    private bool isPuzzleCompleted = false;

    void Update()
    {
        if (!isPuzzleCompleted && CheckPuzzleCompletion())
        {
            Debug.Log("puzzle Complete！");
            isPuzzleCompleted = true;
            // 可以在这里触发完成的音效或者动画
        }
    }

    bool CheckPuzzleCompletion()
    {
        foreach (GameObject piece in puzzlePieces)
        {
            // 简单的完成检查逻辑：比较每个拼图块的位置
            if (Vector3.Distance(piece.transform.position, piece.GetComponent<PuzzlePiece>().originalPosition) > 0.1f)
            {
                return false;
            }
        }
        return true;
    }
}

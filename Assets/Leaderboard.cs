using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.Events;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "a2bb849de9c6f40ff42a95c7fe7cafe0740613fcb330e9d91aa8f3c4902cebd9";

    public UnityEvent<string, int> submitScoreEvent;

    private GameGoals gameGoals;

    [SerializeField]
    private TextMeshProUGUI inputScore;
    [SerializeField]
    private TMP_InputField inputName;

    private void Start()
    {

        gameGoals = FindObjectOfType<GameGoals>();


    }

    public void GetScore()
    {
        UpdateInputScore();

        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }

    public void UpdateInputScore()
    {
        if (gameGoals != null)
        {
            inputScore.text = GameGoals.score.ToString();
        }
    }

    public void SubmitScore()
    {
        if (int.TryParse(inputScore.text, out int scoreValue))
        {
            submitScoreEvent.Invoke(inputName.text, scoreValue);
        }
        else
        {
            Debug.LogWarning("Score input is not a valid number: " + inputScore.text);
        }
    }
}

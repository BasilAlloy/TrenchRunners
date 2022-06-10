using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI highscoreText;

    int score = 0;
	int highscore = 0;

	private void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		highscore = PlayerPrefs.GetInt("highscore", 0);
		UpdateScoreText();
    }

	public void AddPoint()
	{
		score += 1;
		if (highscore < score)
		{
			highscore = score;
			PlayerPrefs.SetInt("highscore", score);
		}
		UpdateScoreText();
	}

	public void ResetScore()
	{
		score = 0;
		UpdateScoreText();
	}

	private void UpdateScoreText()
	{
		scoreText.text = "Score: " + score.ToString();
		highscoreText.text = "Highscore: " + highscore.ToString();
	}
}

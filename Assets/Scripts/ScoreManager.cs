using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

	[SerializeField] private GameObject playerShip;

    [SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI highscoreText;

    int bonusPoints = 0;
	int distance = 0;

	int highscore = 0;
	int score = 0;

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

	// Update is called once per frame
	void Update()
	{
		distance = (int)(playerShip.transform.position.z / 10);
		UpdateScoreText();
	}

	public void AddPoints(int points)
	{
		bonusPoints += points;
		UpdateScoreText();
	}

	public void ResetScore()
	{
		bonusPoints = 0;
		UpdateScoreText();
	}

	private void UpdateScoreText()
	{
		score = bonusPoints + distance;
		if (highscore < score)
		{
			highscore = score;
			PlayerPrefs.SetInt("highscore", score);
		}

		scoreText.text = "Score: " + score.ToString();
		highscoreText.text = "Highscore: " + highscore.ToString();
	}
}

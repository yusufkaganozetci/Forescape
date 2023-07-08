using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeScoreText(int changeAmount)
    {
        int currentScore = int.Parse(scoreText.text);
        scoreText.text = (currentScore + changeAmount).ToString();
    }
}

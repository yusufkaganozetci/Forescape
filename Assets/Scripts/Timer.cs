using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] int startingNumber;
    private GameHandler gameHandler;
    private TextMeshProUGUI timerText;
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        gameHandler = FindObjectOfType<GameHandler>();
        StartCoroutine(StartTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartTimer()
    {
        int currentNumber = startingNumber;
        timerText.text = startingNumber.ToString();
        yield return new WaitForSecondsRealtime(0.5f);
        while (currentNumber > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            currentNumber -= 1;
            timerText.text = currentNumber.ToString();
        }
        gameObject.SetActive(false);
        gameHandler.StartTheGame();
    }
}

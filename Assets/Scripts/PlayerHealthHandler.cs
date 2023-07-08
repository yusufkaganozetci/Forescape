using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] float currentPlayerHealth;
    [SerializeField] Image[] imagePlaces;
    [SerializeField] Sprite[] heartSprites;
    [SerializeField] Animator playerAnimator;
    private bool canPlayerTakeDamage = true;
    private GameHandler gameHandler;

    private void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();
    }

    public void ChangePlayerHealth(float negativeHealthAmount)
    {
        if (canPlayerTakeDamage)
        {
            canPlayerTakeDamage = false;
            float tempHealth = currentPlayerHealth - negativeHealthAmount;
            currentPlayerHealth = tempHealth;

            int lastChangedIndex = 0;
            while (tempHealth >= 1)
            {
                imagePlaces[lastChangedIndex].sprite = heartSprites[0];//full heart
                lastChangedIndex++;
                tempHealth -= 1;
            }
            while (tempHealth >= 0.5f)
            {
                imagePlaces[lastChangedIndex].sprite = heartSprites[1];//half heart
                lastChangedIndex++;
                tempHealth -= 0.5f;
            }
            for (int i = lastChangedIndex; i < imagePlaces.Length; i++)
            {
                imagePlaces[i].sprite = heartSprites[2];//zero heart
            }
            if (currentPlayerHealth <= 0)
            {
                gameHandler.HandleOnFinishedGame();
            }
            else
            {
                if (negativeHealthAmount > 0)
                {
                    StartCoroutine(ChangeCanPlayerTakeDamage());
                }
                else
                {
                    canPlayerTakeDamage = true;
                }
            }
        }
        
    }

    private IEnumerator ChangeCanPlayerTakeDamage()
    {
        playerAnimator.SetBool("isTouchable",false);
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool("isTouchable", true);
        canPlayerTakeDamage = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineDeneme : MonoBehaviour
{
    [SerializeField] float animTime;
    private float startDelay = 0f;
    private float delayBetweenAnims = 4f;
    
    public IEnumerator HandleCrocodileAnimation()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(CrocodileOutOfPoolAnimation(animTime, 1.2f, 2));
        yield return new WaitForSeconds(delayBetweenAnims);
        if (gameObject.activeSelf)
        {
            StartCoroutine(CrocodileBackToPoolAnimation(animTime, 1.2f, 2));
        }
    }

    private IEnumerator CrocodileOutOfPoolAnimation(float animTime, float diffFromCurrentScale, float diffFromCurrentPosition)
    {
        transform.localScale = new Vector3(1.2f, 0, 1.2f);
        Vector3 currentScale = transform.localScale;
        Vector3 currentPosition = transform.position;
        float desiredYPosition = currentPosition.y - diffFromCurrentPosition;
        float desiredYScale = currentScale.y + diffFromCurrentScale;
        float increaseAmountForScale = (desiredYScale - currentScale.y) / animTime;
        float increaseAmountForPosition = (desiredYPosition - currentPosition.y) / animTime;
        while (animTime > 0)
        {
            currentPosition = transform.position;
            currentScale.y += increaseAmountForScale * Time.deltaTime;
            currentPosition.y += increaseAmountForPosition * Time.deltaTime;
            transform.localScale = currentScale;
            transform.position = currentPosition;
            animTime -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CrocodileBackToPoolAnimation(float animTime, float diffFromCurrentScale, float diffFromCurrentPosition)
    {
        
            Vector3 currentScale = transform.localScale;
            Vector3 currentPosition = transform.position;
            float desiredYPosition = currentPosition.y + diffFromCurrentPosition;
            float desiredYScale = currentScale.y - diffFromCurrentScale;
            float increaseAmountForScale = (desiredYScale - currentScale.y) / animTime;
            float increaseAmountForPosition = (desiredYPosition - currentPosition.y) / animTime;
            while (animTime > 0)
            {
                currentPosition = transform.position;
                currentScale.y += increaseAmountForScale * Time.deltaTime;
                currentPosition.y += increaseAmountForPosition * Time.deltaTime;
                transform.localScale = currentScale;
                transform.position = currentPosition;
                animTime -= Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        
        
        
    }

}

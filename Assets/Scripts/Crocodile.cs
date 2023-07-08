using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour,IEnemy
{
    [SerializeField] float totalHealth;
    [SerializeField] float dealDamageAmount;
    [SerializeField] Color32 damageTakenColor;
    [SerializeField] GameObject crocodileDyingParticle;
    [SerializeField] float animTime;
    [SerializeField] bool isCrocodileComesUp;
    [SerializeField] int givenCoinAmount;
    private Vector3 startPosition;
    private Vector3 startScale;
    private float startDelay = 0f;
    private float delayBetweenAnims = 4f;
    private Color32 startColor;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CoinManager coinManager;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        startScale = transform.localScale;
        //animator = GetComponent<Animator>();
        startColor = spriteRenderer.color;
        currentHealth = totalHealth;
        //StartCoroutine(HandleCrocodileAnimation());
    }


    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {

            //animator.SetTrigger("isDying");
            coinManager.ChangeScoreText(givenCoinAmount);
            StartCoroutine(HandleCrocodileDying());
            

        }
        else
        {
            StartCoroutine(HandleSpriteWhenDamageIsTaken());
        }

    }

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
        currentHealth = totalHealth;
        transform.localScale = startScale;
        transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);// startPosition;
        Vector3 currentScale = transform.localScale;
        Vector3 currentPosition = transform.position;
        float desiredYPosition;
        if (isCrocodileComesUp)
        {
            desiredYPosition = currentPosition.y - diffFromCurrentPosition;
        }
        else
        {
            desiredYPosition = currentPosition.y + diffFromCurrentPosition;
        }
        
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
        float desiredYPosition;// = currentPosition.y + diffFromCurrentPosition;
        if (isCrocodileComesUp)
        {
            desiredYPosition = currentPosition.y + diffFromCurrentPosition;
        }
        else
        {
            desiredYPosition = currentPosition.y - diffFromCurrentPosition;
        }
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
    private IEnumerator HandleCrocodileDying()
    {
        yield return null;
        Instantiate(crocodileDyingParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
    private IEnumerator HandleSpriteWhenDamageIsTaken()
    {
        spriteRenderer.color = damageTakenColor;
        yield return new WaitForSecondsRealtime(0.2f);
        spriteRenderer.color = startColor;
    }

    public float DealDamageAmount()
    {
        return dealDamageAmount;
    }
}

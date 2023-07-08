using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour,IEnemy
{
    [SerializeField] float totalHealth;
    [SerializeField] float dealDamageAmount;
    [SerializeField] Color32 damageTakenColor;
    [SerializeField] GameObject beeDyingParticle;
    [SerializeField] int givenCoinAmount;
    private CoinManager coinManager;
    private Color32 startColor;
    private SpriteRenderer spriteRenderer;
    private float currentHealth;
    private float lastXPosition;
    private bool isTouched=false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        currentHealth = totalHealth;
        coinManager = FindObjectOfType<CoinManager>();

    }

    private void Update()
    {
        HandleFlip();
    }

    private void HandleFlip()
    {
        if (transform.position.x >= lastXPosition)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        lastXPosition = transform.position.x;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            coinManager.ChangeScoreText(givenCoinAmount);
            StartCoroutine(SetDelay());
            Destroy(gameObject, 0.1f);
        }
        else
        {
            StartCoroutine(HandleSpriteWhenDamageIsTaken());
        }

    }

    private IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(0.05f);
        Instantiate(beeDyingParticle, transform.position,Quaternion.identity);

    }
    private IEnumerator HandleSpriteWhenDamageIsTaken()
    {
        spriteRenderer.color = damageTakenColor;
        yield return new WaitForSecondsRealtime(0.2f);
        spriteRenderer.color = startColor;
    }

    public float DealDamageAmount()
    {
        if (!isTouched)
        {
            StartCoroutine(HandleBeeDamage());
            return 0;//0
        }
        else
        {
            return dealDamageAmount;
        }
        
    }

    private IEnumerator HandleBeeDamage()
    {
        isTouched = true;
        yield return new WaitForSeconds(1f);
        isTouched = true;
    }

}

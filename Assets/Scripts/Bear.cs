using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour,IEnemy
{
    [SerializeField] float totalHealth;
    [SerializeField] float dealDamageAmount;
    [SerializeField] Color32 damageTakenColor;
    [SerializeField] GameObject bearDyingParticle;
    [SerializeField] int givenCoinAmount;
    private Color32 startColor;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CoinManager coinManager;
    private float currentHealth;
    private float lastXPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        startColor = spriteRenderer.color;
        currentHealth = totalHealth;

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
            animator.SetTrigger("isDying");
            StartCoroutine(SetDelay());
            Destroy(gameObject,0.1f);
        }
        else
        {
            StartCoroutine(HandleSpriteWhenDamageIsTaken());
        }
        
    }

    private IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(0.05f);
        Instantiate(bearDyingParticle, transform.position, Quaternion.identity);

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour,IEnemy
{
    [SerializeField] float totalHealth;
    [SerializeField] float dealDamageAmount;
    [SerializeField] Color32 damageTakenColor;
    [SerializeField] GameObject wolfDyingParticle;
    [SerializeField] int givenCoinAmount;
    private CoinManager coinManager;
    private Color32 startColor;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float currentHealth;
    private float lastXPosition;
    public Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coinManager = FindObjectOfType<CoinManager>();
        animator = GetComponent<Animator>();
        startColor = spriteRenderer.color;
        currentHealth = totalHealth;

    }

    private void Update()
    {
        HandleFlip();
        PositionCheckForDestroy();
    }

    private void PositionCheckForDestroy()
    {
        if (Vector2.Distance(transform.position, targetTransform.transform.position) < 1)
        {
            Destroy(gameObject);
        }
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
            //animator.SetTrigger("isDying");
            Instantiate(wolfDyingParticle, transform.position, Quaternion.identity);
            //StartCoroutine(SetDelay());
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(HandleSpriteWhenDamageIsTaken());
        }

    }

    private IEnumerator SetDelay()
    {
        //yield return new WaitForSeconds(0.05f);
        yield return null;
        Instantiate(wolfDyingParticle, transform.position, Quaternion.identity);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float boostedPlayerSpeed;

    private Animator animator;

    private Rigidbody2D rb;
    private PolygonCollider2D playerCollider;
    private float lastHorizontalSpeed = 0;
    private float horizontalSpeed =0;
    private float verticalSpeed;

    private PlayerHealthHandler playerHealthHandler;
    float currentDistance, lastDistance;

    private float timerForUpBoost = 0;
    private float timerForDownBoost = 0;
    private float timerForRightBoost = 0;
    private float timerForLeftBoost = 0;

    private bool canUpBoost = true;
    private bool canDownBoost = true;
    private bool canRightBoost = true;
    private bool canLeftBoost = true;

    private bool isReadyForAcceleration = false;
    private bool isBoostApplying = false;

    public bool isGameActive = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<PolygonCollider2D>();
        playerHealthHandler = FindObjectOfType<PlayerHealthHandler>();


    }

 
    private void FixedUpdate()
    {
        if (!isBoostApplying && isGameActive)
        {
            rb.velocity = playerSpeed * Time.fixedDeltaTime * new Vector2(horizontalSpeed, verticalSpeed);
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            if (!isBoostApplying)
            {
                lastHorizontalSpeed = horizontalSpeed;
                horizontalSpeed = Input.GetAxis("Horizontal");
                verticalSpeed = Input.GetAxis("Vertical");
                //HandleBoost();
            }
            CheckPlayerHasSpeed();
            CheckCollider();
        }
        
    }

    private void CheckCollider()
    {
        if (Mathf.Abs(transform.position.x) > 9f)
        {
            gameObject.layer = 12;
        }
        else
        {
            gameObject.layer = 8;
        }
    }
    private void HandleBoost()
    {
        if (canUpBoost)
        {
            HandleUpBoost();
        }
        if (canDownBoost)
        {
            HandleDownBoost();
        }
        if (canRightBoost)
        {
            HandleRightBoost();
        }
        if (canLeftBoost)
        {
            HandleLeftBoost();
        }
    }

    private void HandleUpBoost()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            timerForUpBoost = 0.2f;
        }
        if (Input.GetKeyDown(KeyCode.W) && timerForUpBoost > 0)
        {
            isBoostApplying = true;
            timerForUpBoost = 0;
            canUpBoost = false;
            playerCollider.enabled = false;
            StartCoroutine(ChangeBoostApplyingVal());
            StartCoroutine(ChangeCanUpBoost(true));
            rb.velocity += boostedPlayerSpeed * Time.deltaTime * new Vector2(0, 1);
            
        }
        if (timerForUpBoost > 0)
        {
            timerForUpBoost -= Time.deltaTime;
        }
    }

    private void HandleDownBoost()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            timerForDownBoost = 0.3f;
        }
        if (Input.GetKeyDown(KeyCode.S) && timerForDownBoost > 0)
        {
            isBoostApplying = true;
            timerForDownBoost = 0;
            canDownBoost = false;
            playerCollider.enabled = false;
            StartCoroutine(ChangeBoostApplyingVal());
            StartCoroutine(ChangeCanDownBoost(true));
            rb.velocity += boostedPlayerSpeed * Time.deltaTime * new Vector2(0, -1);

        }
        if (timerForDownBoost > 0)
        {
            timerForDownBoost -= Time.deltaTime;
        }
    }

    private void HandleRightBoost()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            timerForRightBoost = 0.3f;
        }
        if (Input.GetKeyDown(KeyCode.D) && timerForRightBoost > 0)
        {
            isBoostApplying = true;
            timerForRightBoost = 0;
            canRightBoost = false;
            playerCollider.enabled = false;
            StartCoroutine(ChangeBoostApplyingVal());
            StartCoroutine(ChangeCanRightBoost(true));
            rb.velocity += boostedPlayerSpeed * Time.deltaTime * new Vector2(1, 0);

        }
        if (timerForRightBoost > 0)
        {
            timerForRightBoost -= Time.deltaTime;
        }
    }

    private void HandleLeftBoost()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            timerForLeftBoost = 0.3f;
        }
        if (Input.GetKeyDown(KeyCode.A) && timerForLeftBoost > 0)
        {
            isBoostApplying = true;
            timerForLeftBoost = 0;
            canLeftBoost = false;
            playerCollider.enabled = false;
            StartCoroutine(ChangeBoostApplyingVal());
            StartCoroutine(ChangeCanLeftBoost(true));
            rb.velocity += boostedPlayerSpeed * Time.deltaTime * new Vector2(-1, 0);

        }
        if (timerForLeftBoost > 0)
        {
            timerForLeftBoost -= Time.deltaTime;
        }
    }

    private IEnumerator ChangeCanUpBoost(bool val)
    {
        yield return new WaitForSecondsRealtime(1f);
        playerCollider.enabled = val;
        canUpBoost = val;
    }

    private IEnumerator ChangeCanDownBoost(bool val)
    {
        yield return new WaitForSecondsRealtime(1f);
        playerCollider.enabled = val;
        canDownBoost = val;
    }

    private IEnumerator ChangeCanRightBoost(bool val)
    {
        yield return new WaitForSecondsRealtime(1f);
        playerCollider.enabled = val;
        canRightBoost = val;
    }

    private IEnumerator ChangeCanLeftBoost(bool val)
    {
        yield return new WaitForSecondsRealtime(1f);
        playerCollider.enabled = val;
        canLeftBoost = val;
    }

    private IEnumerator ChangeBoostApplyingVal()
    {
        yield return new WaitForSeconds(0.1f);
        isBoostApplying = false;

    }
    
    
    public void FlipSprite(bool val/**float xScale**/)
    {
        GetComponent<SpriteRenderer>().flipX = val;
        //transform.localScale = new Vector2(xScale, transform.localScale.y);
    }

    private void CheckPlayerHasSpeed()
    {
        bool playerHasVelocity = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon || Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasVelocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IEnemy>() != null)
        {
            playerHealthHandler.ChangePlayerHealth(collision.GetComponent<IEnemy>().DealDamageAmount());
        }

    }
}

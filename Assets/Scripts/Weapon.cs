using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject targetImage;
    [SerializeField] Transform shotPoint;
    [SerializeField] float timeBetweenShots;
    [SerializeField] Animator shake;
    private float currentTimeBetweenShots;
    private PlayerController playerController;
    private bool isPressing = false;
    private void Start()
    {
        shake = Camera.main.GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        currentTimeBetweenShots = 0;
        StartCoroutine(TurnOffFireEffect());
    }

    public void SetTargetImage(GameObject targetImage)
    {
        this.targetImage = targetImage;
    }
    void Update()
    {
        if (playerController.isGameActive)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetImage.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            Vector3 difference = mousePosition - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            HandleFlipAcordingToWeapon(rotZ);
            if (Input.GetMouseButtonDown(0) && currentTimeBetweenShots <= 0)
            {
                isPressing = true;
                int shakeIndex = Random.Range(1, 3);
                shake.SetTrigger("Shake" + shakeIndex.ToString());
                currentTimeBetweenShots = timeBetweenShots;
                //StartCoroutine(Fire());
                fireEffect.SetActive(true);
                GameObject bullet = Instantiate(projectile, shotPoint.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90));
                bullet.GetComponent<IBullet>().ChangeBulletDirection(difference);
            }
            /*if (Input.GetMouseButtonUp(0))
            {
                isPressing = false;
                StopCoroutine(Fire());
            }*/
            if (fireEffect.activeSelf)
            {
                StartCoroutine(TurnOffFireEffect());
            }
            currentTimeBetweenShots -= Time.deltaTime;
        }
        
    }

    private IEnumerator Fire()
    {
        Vector3 difference;
        while (isPressing)
        {
            fireEffect.SetActive(true);
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            GameObject bullet = Instantiate(projectile, shotPoint.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90));
            bullet.GetComponent<IBullet>().ChangeBulletDirection(difference);
            yield return new WaitForSecondsRealtime(timeBetweenShots);
        }
    }

    private IEnumerator TurnOffFireEffect()
    {
        yield return new WaitForSeconds(0.05f);
        fireEffect.SetActive(false);
    }
    private void HandleFlipAcordingToWeapon(float rotZ)
    {
        if (Mathf.Abs(rotZ) < 90)
        {
            playerController.FlipSprite(false);//1
        }
        else
        {
            playerController.FlipSprite(true);
        }
    }
}

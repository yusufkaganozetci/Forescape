using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject targetImage;
    [SerializeField] int currentWeaponIndex;
    private PlayerController playerController;
    private GameObject currentWeapon;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        //currentWeaponIndex = PlayerPrefs.GetInt("CurrentWeaponIndex", 0);
        AssignCurrentWeapon();
    }

    private void Update()
    {
        HandleWeaponChanging();
    }

    private void HandleWeaponChanging()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AssignWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AssignWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AssignWeapon(2);
        }
    }

    private void AssignWeapon(int weaponIndex)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }
        currentWeapon = Instantiate(weapons[weaponIndex], playerController.transform);
        currentWeapon.GetComponent<Weapon>().SetTargetImage(targetImage);
    }

    private void AssignCurrentWeapon()
    {
        currentWeapon = Instantiate(weapons[currentWeaponIndex], playerController.transform);
        currentWeapon.GetComponent<Weapon>().SetTargetImage(targetImage);
    }
}

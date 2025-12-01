using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Weapon : MonoBehaviour
{
    public Camera PlayerCam;
    public TMP_Text bullet;
    public TMP_Text reload;

    private Animator anim;

    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    public float spreadIntensity;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifetime = 3f;
    public float currentBullet;
    public float maxBullet;
    public float reloadDelayPerBullet = 0.1f;
    public float damage;
    public float maxReload = 2;
    public float currentReload;

    public bool isReloading;

    void Start()
    {
        currentReload = maxReload;
        currentBullet = maxBullet;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return;

        bool wasShooting = isShooting;

        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Burst || currentShootingMode == ShootingMode.Single)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (currentShootingMode == ShootingMode.Auto && !isShooting && !readyToShoot)
        {
            anim.SetBool("isShooting", false);
        }

        if (readyToShoot && isShooting && currentBullet > 0)
        {
            burstBulletsLeft = bulletsPerBurst;
            currentBullet -= 1;
            FireWeapon();
            UpdateUI();
        }
        if (currentBullet == 0)
        {
            readyToShoot = false;
            anim.SetBool("isShooting", false);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (currentBullet < maxBullet)
            {
                StartCoroutine(Reload());
                UpdateUI();
            }
        }
        UpdateUI();
    }

    public void AddReload(float amount)
    {
        currentReload = Mathf.Clamp(currentReload + amount, 0, maxReload);
        UpdateUI();
    }
    
    void FireWeapon()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            anim.SetBool("isShooting", true);
        }
        else
        {
            anim.SetTrigger("Fire");
        }

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpreac().normalized;

        GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawn.position,
            Quaternion.LookRotation(shootingDirection) * Quaternion.Euler(0, 90, 90));

        bulletInstance.GetComponent<Bullet>().damage = damage;

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = shootingDirection * bulletVelocity;

        StartCoroutine(DestroyBulletAfterTime(bulletInstance, bulletPrefabLifetime));

        if (allowReset)
        {
            Invoke("ReserShot", shootingDelay);
            allowReset = false;
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }
    
    private IEnumerator Reload()
    {
        if (currentReload > 0)
        {
            currentReload -= 1;
            isReloading = true;
            readyToShoot = false;

            while (currentBullet < maxBullet)
            {
                currentBullet += 1;
                UpdateUI();
                yield return new WaitForSeconds(reloadDelayPerBullet);
            }

            isReloading = false;
            readyToShoot = true;
        }
    }


    private void ReserShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpreac()
    {
        Ray ray = PlayerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = (targetPoint - bulletSpawn.position).normalized;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        Vector3 spread = PlayerCam.transform.right * x + PlayerCam.transform.up * y;

        Vector3 finalDir = (direction + spread).normalized;
        return finalDir;
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }


    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        anim = GetComponent<Animator>();
    }

    void UpdateUI()
    {
        if (bullet != null)
        {
            bullet.text = Mathf.FloorToInt(currentBullet) + "/" + maxBullet;
        }

        if (reload != null)
        {
            reload.text = Mathf.FloorToInt(currentReload) + "/" + maxReload;
        }
    }
}
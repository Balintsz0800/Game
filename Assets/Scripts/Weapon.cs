using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Weapon : MonoBehaviour
{
    
    public Camera PlayerCam;
    public Text bullet;

    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;
    
    public float spreadIntensity;
    
    public GameObject bulletPrefab;
    public Transform bulletSpawn ;
    public float bulletVelocity = 30;
    public float bulletPrefabLifetime = 3f;
    public float currentBullet;
    public float maxBullet;
    public float reloadDelayPerBullet = 0.1f;
    
    public bool isReloading;

    void Start()
    {
        currentBullet = maxBullet;
        UpdateUI();
    }
    
    // Update is called once per frame
    void Update()
    {
        PlayerStatics statics = PlayerStatics.Instance;
        
        if (currentShootingMode == ShootingMode.Auto)
        { 
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if(currentShootingMode == ShootingMode.Burst || currentShootingMode == ShootingMode.Single)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting && currentBullet> 0)
        {
            burstBulletsLeft = bulletsPerBurst;
            currentBullet -= 1;
            FireWeapon();
            UpdateUI();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (currentBullet < maxBullet)
            {
                StartCoroutine(Reload());
                UpdateUI();
            }
        } 
    }
    

    void FireWeapon()
    {
        readyToShoot = false;
    
        Vector3 shootingDirection = CalculateDirectionAndSpreac().normalized;
    
        GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(shootingDirection));
    
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if(rb != null)
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


    public enum  ShootingMode
    {
        Single,
        Burst,
        Auto
    }                
        
    public  ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        
    }
    

    void UpdateUI()
    {
        bullet.text = Mathf.FloorToInt(currentBullet) + "/" + maxBullet;
    }
}
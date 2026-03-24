using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class rifle : MonoBehaviour
{ 
    //Reference to the animator
    private Animator animator;

    //Bullet Vars
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletLife = 3f;

    //Muzzle effect
    [Header("Muzzle")]
    public GameObject muzzleEffect;

   

    //Loading
    [Header("Loading")]
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    public bool isADS;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;



    //Shooting
    [Header("Shooting")]
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    [Header("Burst")]
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //Spread
    [Header("Spread")]
    public float spreadIntensity;
    public float hipSpreadIntensity;
    public float ADSSpreadIntensity;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;

        spreadIntensity = hipSpreadIntensity;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(1))
        {
            EnterADS();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ExitADS();
        }

        if (bulletsLeft == 0 && isShooting)
        {
            SoundManager.Instance.emptyMagazineSoundRifle.Play();
        }

        if (currentShootingMode == ShootingMode.Auto)
        {
            //Only works when holding lmb
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            //Clicking LMB
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Reload Check. Works if you push left click or R
        if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Mouse0)) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }

        if (readyToShoot && !isShooting  && !isReloading && bulletsLeft <= 0)
        {
            Reload();
        }

        if (readyToShoot && isShooting && bulletsLeft > 0)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }

        if (HUDManager.Instance.ammoDisplay != null)
        {
            HUDManager.Instance.ammoDisplay.text = $"{bulletsLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}";
        }
    }

    private void FireWeapon()
    {
        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();

        if (isADS)
        {
            //Recoil ADS
            animator.SetTrigger("RECOILADS");
        }
        else
        {
            //Regular recoil
            animator.SetTrigger("RECOIL");
        }
        

        SoundManager.Instance.shootingSoundRifle.Play();

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //Use our bullet prefab at bulletSpawn position with default rotation
        //Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //Pointing the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        //Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        //Destroy the bullet after x time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLife));

        //Checking if we are done shooting
        if(allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //Burst Mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft >1) //At this point we have already shot once so we don't check if its zero
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void Reload()
    {
        isReloading = true;
        Invoke("ReloadCompleted",reloadTime);
        animator.SetTrigger("RELOAD");
        SoundManager.Instance.reloadingSoundRifle.Play();
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {
        //Prevents multiple resets
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        //Shooting from the middle of our screen to check where we are pointing at
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            //Hitting Something
            targetPoint = hit.point;
        }
        else
        {
            //Shooting at the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        //Calculate Random Spread
        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        //Returning the shooting direction and spread
        return direction + new Vector3(0,y,z);

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    private void EnterADS()
    {
        animator.SetTrigger("enterADS");
        animator.SetBool("isADS", true);
        isADS = true;
        spreadIntensity = ADSSpreadIntensity;
    }

    private void ExitADS()
    {
        animator.SetTrigger("exitADS");
        animator.SetBool("isADS", false);
        isADS = false;
        spreadIntensity = hipSpreadIntensity;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{   
    [Header("Player")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] float xPadding = 0.1f;
    [SerializeField] float yPadding = 0.1f;
    [SerializeField] int health = 200;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] [Range(0,1)]private float deathSoundVolume = 0.7f;
    
    [Header("Projectile")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileFirePeriod = 0.1f;
    [SerializeField] private AudioClip playerLaserSound;
    [SerializeField] [Range(0,1)]private float playerLaserSoundVolume = 0.7f;
    private Coroutine firingCoroutine;
    
    
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpBoundries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }


    private void SetUpBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            // StopAllCoroutines();
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,projectileSpeed);
            AudioSource.PlayClipAtPoint(playerLaserSound,Camera.main.transform.position, playerLaserSoundVolume);
            yield return new WaitForSeconds(projectileFirePeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            this.health -= damageDealer.GetDamage();
            damageDealer.Hit();
            if (this.health <= 0f)
            {
                FindObjectOfType<Level>().LoadEndGame();
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(deathSound,Camera.main.transform.position, deathSoundVolume);
            }   
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
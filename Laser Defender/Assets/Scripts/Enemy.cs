using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{   [Header("Stats")]
    [SerializeField] private float health = 100;
    [SerializeField] private int scoreValue = 150;
    
    [Header("Shooting")]
    [SerializeField] private float shortCounter;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 0.3f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 0.2f;
    
    [Header("Sound Effects")]
    [SerializeField] private GameObject deathVfx;
    [SerializeField] private float durationOfExplosion = 1f;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] [Range(0,1)]private float deathSoundVolume = 0.7f;
    [SerializeField] private AudioClip enemyLaserSound;
    [SerializeField] [Range(0,1)]private float enemyLaserSoundVolume = 0.7f;

    private void Start()
    {
        shortCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shortCounter -= Time.deltaTime;
        if (shortCounter <= 0f)
        {
            Fire();
            shortCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            laserPrefab,
            transform.position,
            Quaternion.identity) as GameObject ;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectileSpeed);
        AudioSource.PlayClipAtPoint(enemyLaserSound,Camera.main.transform.position, enemyLaserSoundVolume);
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
                Destroy(gameObject);
                GameObject explosion = Instantiate(deathVfx, transform.position, transform.rotation);
                Destroy(explosion, durationOfExplosion);
                AudioSource.PlayClipAtPoint(deathSound,Camera.main.transform.position, deathSoundVolume);
                FindObjectOfType<GameSession>().AddScore(scoreValue);
            }
        }
    }
}
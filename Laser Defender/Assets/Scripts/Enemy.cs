using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private float shortCounter;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 0.3f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 0.2f;

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
                Destroy(gameObject); ;
            }
        }
    }
}
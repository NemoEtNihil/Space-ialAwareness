using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    public int gunDamage;
    public float fireRate;
    public float weaponRange;
    public float hitForce; //affects rigidbody
    public Transform gunEnd; //the line from the gun
    public BulletController bullet;
    public float bulletSpeed;
    private float nextFire;

    public void fire()
    {
        AudioSource enemyFire = GetComponent<AudioSource>();
        if (Time.time > nextFire)
        {
            enemyFire.Play();
            nextFire = Time.time + fireRate;
            BulletController newBullet = Instantiate(bullet, gunEnd.position, gunEnd.rotation) as BulletController;
            newBullet.gunDamage = gunDamage;
            newBullet.speed = bulletSpeed;
        }
    }

}

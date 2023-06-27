using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    public float Speed = 200;
    public float TTL = 2;
    public float Damage = 10;

    private float TTLLeft;

    public enum Shooter
    {
        Player,
        Opponent
    }
    public Shooter MyShooter;
    public GameObject HitEffect;
    private void Start()
    {
        TTLLeft = TTL;
    }


    void Update()
    {
        this.transform.position += this.transform.forward * Speed * Time.deltaTime;
        TTLLeft -= Time.deltaTime;

        if(TTLLeft < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (MyShooter == Shooter.Player)
        {
            var shipEnemy = collision.gameObject.GetComponentInParent<shipEnemy>();
            
            if (shipEnemy != null)
            {
                Instantiate(HitEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

        if (MyShooter == Shooter.Opponent)
        {
            var shipPlayer = collision.gameObject.GetComponentInParent<shipPlayer>();

            if (shipPlayer != null)
            {
                shipPlayer.DoDamage(Damage);

                Instantiate(HitEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}

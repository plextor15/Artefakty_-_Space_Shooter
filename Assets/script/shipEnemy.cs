using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipEnemy : MonoBehaviour
{
    public enum State
    {
        Charge,
        Fleeing
    }

    public float Speed = 10;
    public float TurnSpeed = 200;

    public float DistanceToCharge = 20.0f;
    public float DistanceToFlee = 10.0f;

    public Blaster Weapon;
    public float FireEvery = 0.8f;

    private State CurrentState;
    private shipPlayer ShipPlayer;
    private float azimuth;
    private float timeToFire;
    
    void Start()
    {
        ShipPlayer = FindObjectOfType<shipPlayer>();
        CurrentState = State.Charge;
        timeToFire = 0;
    }

    void Update()
    {
        this.transform.position += this.transform.forward * Speed * Time.deltaTime;
        var distanceToPlayer = ShipPlayer.transform.position - this.transform.position;
        
        if (CurrentState == State.Charge)
        {
            var anglee = Mathf.Rad2Deg * Mathf.Atan2(distanceToPlayer.x, distanceToPlayer.z);
            azimuth = Mathf.MoveTowardsAngle(azimuth, anglee, TurnSpeed * Time.deltaTime);

            timeToFire -= Time.deltaTime;
            if (timeToFire < 0)
            {
                timeToFire = FireEvery;
                Instantiate(Weapon, this.transform.position, this.transform.rotation);
            }
        }

        if (CurrentState == State.Fleeing)
        {
            var anglee = Mathf.Rad2Deg * Mathf.Atan2(-distanceToPlayer.x, -distanceToPlayer.z);
            anglee += 180; ////////
            azimuth = Mathf.MoveTowardsAngle(azimuth, anglee, TurnSpeed * Time.deltaTime);
        }

        if (distanceToPlayer.magnitude < DistanceToFlee)
        {
            CurrentState = State.Fleeing;
        }
        if (distanceToPlayer.magnitude > DistanceToCharge)
        {
            CurrentState = State.Charge;
        }

        this.transform.rotation = Quaternion.Euler(0, azimuth, 0);
    }
}

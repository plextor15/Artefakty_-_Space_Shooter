using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipPlayer : MonoBehaviour
{
    public float Acceleration = 5;
    public float Deacceleration = 2.5f;
    public float TurnRate = 150;
    public float MaxSpeed = 500;

    public float HpMax = 100;
    public float HpRegen = 10;
    public float HpLeft => hpcurrent / HpMax;
    private float hpcurrent;

    public Blaster FieldBlaster;

    public Transform LeftWing;
    public Transform RightWing;
    public Transform LeftFront;
    public Transform RightFront;

    private float azymut;
    private float speed;

    public void DoDamage (float dmg)
    {
        hpcurrent -= dmg;
        if(hpcurrent <= 0)
        {
            hpcurrent = 0;
        }
    }
    void Start()
    {
        azymut = 0;
        speed = 0;

        hpcurrent = HpMax;
    }

    // Update is called once per frame
    void Update()
    {
        hpcurrent += HpRegen * Time.deltaTime;
        if(hpcurrent > HpMax)
        {
            hpcurrent = HpMax;
        }

        if(Input.GetKey(KeyCode.W))
        {
            //this.transform.position += this.transform.forward * Speed * Time.deltaTime;
            speed += Acceleration * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed -= Acceleration * Time.deltaTime;
        }

        if(speed>MaxSpeed)
        {
            speed = MaxSpeed;
        }
        if (speed < -MaxSpeed)
        {
            speed = MaxSpeed;
        }

        this.transform.position += this.transform.forward * speed * Time.deltaTime;

        float speedRatio = Mathf.Abs(speed / MaxSpeed);

        if (Input.GetKey(KeyCode.A))
        {
            //azymut -= this.TurnRate * Time.deltaTime;
            azymut -= Mathf.Lerp(0.0f, TurnRate, speedRatio) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            azymut += Mathf.Lerp(0.0f, TurnRate, speedRatio) * Time.deltaTime;
        }
        this.transform.rotation = Quaternion.Euler(x:0, y:azymut, z:0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(FieldBlaster, this.transform.position, this.transform.rotation);

            Instantiate(FieldBlaster, LeftWing.transform.position, LeftWing.transform.rotation);
            Instantiate(FieldBlaster, RightWing.transform.position, RightWing.transform.rotation);
            Instantiate(FieldBlaster, LeftFront.transform.position, LeftFront.transform.rotation);
            Instantiate(FieldBlaster, RightFront.transform.position, RightFront.transform.rotation);
        }
    }
}

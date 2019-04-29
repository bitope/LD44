using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Walking,
    Pausing,
    Fleeing,
    Excited
}

[RequireComponent(typeof(Rigidbody))]
public class CivilianWalker : MonoBehaviour
{
    public LayerMask mask;

    Vector3 direction;
    float timer;
    float cooldown;
    float stamina;

    State state;

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.drag = 1;
        state = State.Idle;
        ChooseDirection();
    }

    
    void Update()
    {
    

    }

    private void FixedUpdate()
    {

        if (state == State.Idle)
        {
            ChooseDirection();
        }

        if (state == State.Walking)
        {
            stamina--;
            cooldown -= Time.fixedDeltaTime;

            rigidbody.AddForce(transform.forward * 7,ForceMode.Force);
            rigidbody.AddForce(transform.up);

            RaycastHit info;
            if (Physics.SphereCast(transform.position+transform.up*0.25f,1f, transform.forward , out info,2f,mask))
            {
                if (info.transform.name != "GeoSphere001" && info.transform.name != transform.name)
                {
                    Debug.Log(info.transform.name);
                    ChooseDirection();
                }
            }
            

            if (cooldown<0)
                ChooseDirection();
        }
    }

    private void ChooseDirection()
    {
        cooldown = 10;
        transform.Rotate(transform.up, UnityEngine.Random.Range(-90, 90));
        state = State.Walking;
        Debug.Log(direction);
    }
}

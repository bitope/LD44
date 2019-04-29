using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour, IGravityAttractor
{
    public float gravity = -10f;

    public virtual void Attract(Transform body)
    {
        var targetDir = (body.position - transform.position).normalized;
        var bodyUp = body.up;
        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        body.GetComponent<Rigidbody>().AddForce(targetDir * gravity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTest : MonoBehaviour
{
    public List<GameObject> cList;

    public List<GameObject> waypoints;
    public LayerMask mask;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[3].transform;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var character in cList)
        {

            //var v = (character.transform.position - target.transform.position).normalized;
            var x = (target.transform.position - character.transform.position).normalized;

            var hit = Physics2D.CircleCast(character.transform.position, 0.5f, x, .5f, mask);
            if (hit.collider != null)
            {
                x = ((Vector2)x + hit.normal * .25f + Random.insideUnitCircle * .75f) / 2f;
                Debug.DrawLine(character.transform.position, character.transform.position + x, Color.green);
                Debug.DrawLine(character.transform.position, character.transform.position + (Vector3)hit.normal, Color.cyan);
            }
            character.GetComponent<Rigidbody2D>().AddForce(x, ForceMode2D.Impulse);
        }
    }
}

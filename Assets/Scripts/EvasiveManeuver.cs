using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public Vector2 startWait;
    public Vector2 manueverTime;
    public Vector2 manueverWait;
    public Boundary boundary;
    public float tilt;
    public float dodge;
    public float smoothing;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
	}

    IEnumerator Evade ()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(manueverTime.x, manueverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(manueverWait.x, manueverWait.y));
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0, currentSpeed);
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
         );

        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);
    }
}

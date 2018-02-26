using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;

    private float nextFire;
    public float fireRate;
    public GameObject shot;
    public Transform shotSpawn;
    public SimpleTouchPad touchPad;
    public SimpleFireButton fireButton;

    private AudioSource audioSource;
    private Quaternion calibrationQuaternion;

    private void Start()
    {
        CalibrateAccellerometer();
    }


    // Update is called once per frame
    void Update () {
        if (fireButton.CanFire() && (Time.time > nextFire))
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }		
	}

    private void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 accelerationRaw = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(accelerationRaw);

        Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0, direction.y);
        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3(
            Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

    void CalibrateAccellerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0, 0, -1), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }
}

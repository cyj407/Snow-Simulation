using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pureFormula : MonoBehaviour {

    private float mass;
    public bool inWindZone = false;
    public bool stop = false;
    public GameObject windZone;
    private float xPos = 0.0f;
    private float yPos = 0.0f;
    private float zPos = 0.0f;
    private string obj_name;
    private float lifeTime = 0.0f;
    private float maxLifeTime = 5.0f;
    private float counter = 0.0f;
    
    private Vector3 acceleration;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;
        obj_name = gameObject.name;
        if (obj_name == "Sphere(Clone)")
        {
           rb.useGravity = true;
        }

        xPos = this.transform.position.x;
        yPos = this.transform.position.y;
        zPos = this.transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (transform.position.y <= -10)
        {
            Destruction();
        }
        if (lifeTime > maxLifeTime && name != "Sphere")
        {
            Destruction();
        }

        if (transform.position.y <= 0.3)
        {
            stop = true;
            //rb.AddForce(new Vector3(-xForce, 0.0f, -zForce));
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void FixedUpdate()
    {
        /*
        counter += Time.deltaTime;
        if(counter <= 2.0f)
        {
            return;
        }
        else
        {
            counter = 0.0f;
        }
        */

        if (obj_name != "Sphere" && !stop && inWindZone)
        {
            // get current velocity
            float curV_x = (gameObject.transform.position.x - xPos) / Time.deltaTime;
            float curV_y = (gameObject.transform.position.y - yPos) / Time.deltaTime;
            float curV_z = (gameObject.transform.position.z - zPos) / Time.deltaTime;
            // save the previous moment velocity
            xPos = gameObject.transform.position.x;
            yPos = gameObject.transform.position.y;
            zPos = gameObject.transform.position.z;
            Vector3 cur_v = new Vector3(curV_x, curV_y, curV_z);
            Vector3 Wind_direction = windZone.GetComponent<WindArea>().direction;
            float Wind_velocity = windZone.GetComponent<WindArea>().velocity;
            // calculate Fdrag
            Vector3 Ufluid = Wind_velocity * Wind_direction - cur_v;
            Vector3 force = (Ufluid / Ufluid.magnitude) * Ufluid.magnitude * Ufluid.magnitude * mass * -9.8f / 2.25f;

            //////////////////////
            Vector3 FdragAndgravity = new Vector3(force.x, -9.8f, force.z);
            // calculate acceleration = F/m
            acceleration = FdragAndgravity / mass;

            // calculate Vcircular --> Flift
            float Cvel = Ufluid.magnitude / cur_v.magnitude;
            float omega = Random.Range(Mathf.PI / 4, Mathf.PI / 3);
            Vector3 Urotation = new Vector3(-Mathf.Sin(omega * Time.unscaledTime), 0.0f, Mathf.Cos(omega * Time.unscaledTime));
            Vector3 Ucircular = Cvel * omega * 0.001f * Urotation;

            // calculate new position AND velocity
            Vector3 next_pos = this.transform.position + 
                (cur_v + Ucircular) * Time.deltaTime + 0.5f * acceleration * Time.deltaTime * Time.deltaTime;
            Vector3 next_vel = cur_v + acceleration * Time.deltaTime;

            if (next_pos.y <= 0.0f)
            {
                next_pos = new Vector3(next_pos.x, 0.0f, next_pos.z);
                this.transform.position = next_pos;
            }
            else
            {
                this.transform.position = next_pos;
            }

            ////////////////////

            // add this force to rigid body
            //rb.AddForce(force.x, -9.8f, force.z);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "windArea")
        {
            windZone = coll.gameObject;
            inWindZone = true;
        }
        else if (coll.gameObject.tag == "ground")
        {
            stop = true;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "windArea")
        {
            inWindZone = false;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "destroyer")
        {
            Destruction();
        }
    }

    void Destruction()
    {
        Destroy(this.gameObject);
    }
}

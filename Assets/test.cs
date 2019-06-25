using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
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
    private float time;

    private Vector3 velocity, acceleration;
    private float deltaTime = 0.1f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;
        obj_name = gameObject.name;
        if (obj_name != "Sphere")
            rb.useGravity = true;
        // if(obj_name == "Sphere")
        // {
        //   gameObject.transform.localScale = new Vector3(1, 1, 1);
        // }

        xPos = this.transform.position.x;
        yPos = this.transform.position.y;
        zPos = this.transform.position.z;
        time = Time.deltaTime;

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

    public Vector3 computeV(float deltaTime)
    {
        return velocity + acceleration * deltaTime;
    }

    public Vector3 computeA(float dt, Vector3 v)
    {
        Vector3 pos = this.transform.position + v * dt;
        const float g = 9.82f;
        Vector3 Fg = new Vector3(0.0f, -g * mass, 0.0f);
        const float viscosityConst = 7.5f;
        Vector3 Fv = -viscosityConst * mass * velocity;

        // use original formula to get wind force
        float Ufluid = windZone.GetComponent<WindArea>().velocity;
        Vector3 Fwind = windZone.GetComponent<WindArea>().direction * Ufluid * Ufluid * mass * -9.8f / 2.25f;

        Vector3 Ftotal = Fwind + Fv + Fg;
        return Ftotal / mass;
    }

    private void FixedUpdate()
    {
        if (obj_name != "Sphere" && !stop && inWindZone)
        {
            float curV_x = (gameObject.transform.position.x - xPos) / Time.deltaTime;
            float curV_y = (gameObject.transform.position.y - yPos) / Time.deltaTime;
            float curV_z = (gameObject.transform.position.z - zPos) / Time.deltaTime;
            xPos = gameObject.transform.position.x;
            yPos = gameObject.transform.position.y;
            zPos = gameObject.transform.position.z;
            Vector3 cur_v = new Vector3(curV_x, curV_y, curV_z);
            Vector3 Wind_direction = windZone.GetComponent<WindArea>().direction;
            Vector3 Ufluid = windZone.GetComponent<WindArea>().velocity * Wind_direction - cur_v;
            Vector3 force = (Ufluid / Ufluid.magnitude) * Ufluid.magnitude * Ufluid.magnitude * mass * -9.8f / 2.25f;
            rb.AddForce(force.x, -9.8f, force.z);
            //xForce = force.x;
            //zForce = force.z;



            //            print(gameObject.transform.position.y);

            // test
            /*
            Vector3 v1 = deltaTime * computeV(0);
            Vector3 v2 = deltaTime * computeV(0.5f* deltaTime);
            Vector3 v3 = deltaTime * computeV(0.5f* deltaTime);
            Vector3 v4 = deltaTime * computeV(deltaTime);
            this.transform.position = this.transform.position + (1.0f / 6.0f) * (v1 + 2.0f*v2 + 2.0f * v3 + v4);
            Vector3 a1 = deltaTime * computeA(0, velocity);
            Vector3 a2 = deltaTime * computeA(0.5f*deltaTime, velocity+0.5f*a1);
            Vector3 a3 = deltaTime * computeA(0.5f*deltaTime, velocity+0.5f*a2);
            Vector3 a4 = deltaTime * computeA(deltaTime, velocity + a3);
            Vector3 newV = velocity + (1.0f / 6.0f) * (a1 + 2.0f * a2 + 2.0f * a3 + a4);
            acceleration = (newV - velocity) / deltaTime;
            */

            //velocity = newV;
            //this.transform.position = position;
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
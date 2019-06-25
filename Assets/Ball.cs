using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
  private float mass;
  public bool inWindZone = false;
  public bool stop = false;
  public GameObject windZone;
  private float xForce = 0.0f;
  private float zForce = 0.0f;
  private string obj_name;
  private float lifeTime = 0.0f;
  private float maxLifeTime = 5.0f;
  System.Random rand = new System.Random();

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;
        obj_name = gameObject.name;
        if(obj_name != "Sphere")
          rb.useGravity = true;
        // if(obj_name == "Sphere")
        // {
        //   gameObject.transform.localScale = new Vector3(1, 1, 1);
        // }
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
       if(obj_name != "Sphere" && !stop && inWindZone) {
       		float Ufluid =  windZone.GetComponent<WindArea>().velocity;
            Vector3 force = windZone.GetComponent<WindArea>().direction * Ufluid * Ufluid * mass * -9.8f / 2.25f;
            rb.AddForce(force);
        }
    }

    void OnTriggerEnter(Collider coll) {
       if(coll.gameObject.tag == "windArea") {
           windZone = coll.gameObject;
           inWindZone = true;
       }
       else if(coll.gameObject.tag == "ground") {
           stop = true;
           rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
       }
    }

    void OnTriggerExit(Collider coll) {
       if(coll.gameObject.tag == "windArea") {
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
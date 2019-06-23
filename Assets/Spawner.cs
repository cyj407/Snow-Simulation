using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform snow;
    public int spawnAmount = 50;
    private float time;
    // Use this for initialization
    System.Random rand = new System.Random();
    int x, y, z;
    void Start ()
    {
        time = 0.0f;
        for (int i = 0; i < spawnAmount; i++)
        {
            x = rand.Next(-8, 8);
            y = rand.Next(8, 15);
            z = rand.Next(-8, 8);
            Transform d = Instantiate(snow);
            d.parent = transform;
            d.localPosition = new Vector3(x, y, z);
        }
    }
    
    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if(time > 0.2f)
        {
            time = 0.0f;
            for (int i = 0; i < spawnAmount; i++)
            {
                x = rand.Next(-8, 8);
                y = rand.Next(8, 15);
                z = rand.Next(-8, 8);
                Transform d = Instantiate(snow);
                d.parent = transform;
                d.localPosition = new Vector3(x, y, z);
            }
        }
    }
}

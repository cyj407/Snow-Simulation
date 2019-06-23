using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform snow;
    public int spawnAmount = 100;
    private float time;
    // Use this for initialization
    System.Random rand = new System.Random();
    int x, y, z;
    float opacity = 0.5f;
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
            GameObject house1;
            GameObject house2;
            GameObject house3;
            GameObject tree1;
            house1 = GameObject.Find("/SanFranciscoHouseYellow3/polySurface2052");
            house2 = GameObject.Find("/SanFranciscoHouseYellow2/polySurface2052");
            house3 = GameObject.Find("/SanFranciscoHouseBlue/polySurface2052");
            tree1 = GameObject.Find("/Birch_1");

            opacity += 0.01f;
            if (opacity <= 2)
            {
                house1.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_2649093F", opacity);
                house2.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_2649093F", opacity);
                house3.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_2649093F", opacity);
                tree1.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_2649093F", opacity);
                print(tree1.GetComponent<Renderer>().sharedMaterial.GetPassName(1));
            }
        }
    }
}

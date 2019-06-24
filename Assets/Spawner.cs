using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform snow;
    public int spawnAmount = 200;

    private float delta = 0.005f;
    private float time;
    // Use this for initialization
    System.Random rand = new System.Random();
    private int x, y, z;
    private float opacity = 0.5f;
    private float ground_opacity = 0.0f;


    // used to save tree
    Material tree_material;

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

        Material[] materialsArray;
        GameObject tree1;
        tree1 = GameObject.Find("/Birch_1");

        materialsArray = tree1.GetComponent<Renderer>().materials;
        foreach (Material material in materialsArray)
        {
            if (material.name == "trees_atp_alder_branch_sm2_dead (Instance)")
            {
                tree_material = material;
            }
        }

        print(tree_material.ToString());
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
            GameObject car;
            GameObject ground;
            house1 = GameObject.Find("/SanFranciscoHouseYellow3/polySurface2052");
            house2 = GameObject.Find("/SanFranciscoHouseYellow2/polySurface2052");
            house3 = GameObject.Find("/SanFranciscoHouseBlue/polySurface2052");
            tree1 = GameObject.Find("/Birch_1");
            car = GameObject.Find("/GAZ-66/Gaz-66");
            ground = GameObject.Find("/Ground");

            if(ground_opacity <= 0.75)
            {
                ground.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_2649093F", ground_opacity);
                ground_opacity += delta;
            }
            
            if (opacity <= 2)
            {
                house1.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_2649093F", opacity);
                house2.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_2649093F", opacity);
                house3.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_2649093F", opacity);
                tree_material.SetFloat("Vector1_2649093F", opacity);
                car.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_2649093F", opacity);
                opacity += delta;
            }
        }
    }
}

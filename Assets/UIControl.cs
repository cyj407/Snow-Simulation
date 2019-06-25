using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour {

    public GameObject WindArea;

    public void Wind_Changed(float newValue)
    {
        WindArea.GetComponent<WindArea>().velocity = newValue;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

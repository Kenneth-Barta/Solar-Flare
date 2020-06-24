using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    public float orbitalSpeed;
    public static float speedMultiplier;
	// Use this for initialization
	void Start () {
        speedMultiplier = 1;
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.up, orbitalSpeed * speedMultiplier * Time.deltaTime);
	}
}

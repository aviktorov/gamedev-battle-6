﻿using UnityEngine;
using System.Collections;

/*
 */
public class GravityObject : MonoBehaviour {
	
	/*
	 */
	public bool canMove = true;
	
	/*
	 */
	private Rigidbody2D cachedBody;
	private Transform cachedTransform;
	private GameObject[] gravitationalObjects;
	
	/*
	 */
	public void Launch(Vector2 velocity) {
		if(cachedBody == null) return;
		
		canMove = true;
		cachedBody.velocity = velocity;
	}
	
	/*
	 */
	private void Awake() {
		cachedBody = GetComponent<Rigidbody2D>();
		cachedTransform = GetComponent<Transform>();
	}
	
	/*
	 */
	private void Start() {
		gravitationalObjects = GameObject.FindGameObjectsWithTag("Gravitational");
	}
	
	/*
	 */
	private void Update() {
		if(canMove == false) return;
		if(gravitationalObjects == null) return;
		
		foreach(GameObject gravObj in gravitationalObjects) {
			if(gravObj.rigidbody2D == null) continue;
			if(gravObj == gameObject) continue;
			
			Vector2 p1 = new Vector2(cachedTransform.position.x,cachedTransform.position.y);
			Vector2 p2 = new Vector2(gravObj.transform.position.x,gravObj.transform.position.y);
			Vector2 d = (p2 - p1).normalized;
			
			float distanceSqr = (p2 - p1).sqrMagnitude;
			
			cachedBody.AddForce(d * gravObj.rigidbody2D.mass * cachedBody.mass / distanceSqr);
		}
	}
}

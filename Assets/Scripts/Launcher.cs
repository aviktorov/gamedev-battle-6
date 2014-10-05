using UnityEngine;
using System.Collections;

/*
 */
[RequireComponent(typeof(GravityObject))]
public class Launcher : MonoBehaviour {
	
	/*
	 */
	public float maxVelocity = 10.0f;
	public float maxDistance = 5.0f;
	public Transform helper = null;
	
	/*
	 */
	private GravityObject cachedGravity;
	private Transform cachedTransform;
	
	/*
	 */
	private void Awake() {
		cachedGravity = GetComponent<GravityObject>();
		cachedTransform = GetComponent<Transform>();
	}
	
	/*
	 */
	private void Start() {
		if(cachedGravity) {
			cachedGravity.canMove = false;
		}
		
		if(helper) {
			helper.gameObject.SetActive(false);
		}
	}
	
	/*
	 */
	private void Update() {
		if(cachedGravity == null) return;
		if(helper == null) return;
		
		Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition) - cachedTransform.position;
		d.z = 0.0f;
		d = (d.sqrMagnitude > maxDistance * maxDistance) ? d.normalized * maxDistance : d;
		
		helper.position = cachedTransform.position + d;
	}
	
	private void OnMouseUp() {
		if(helper == null) return;
		if(cachedGravity == null) return;
		
		helper.gameObject.SetActive(false);
		Vector3 d = cachedTransform.position - helper.position;
		Vector2 velocity = new Vector2(d.x,d.y);
		
		if(velocity.sqrMagnitude > 0.0f) {
			float k = Mathf.Clamp01(velocity.magnitude / maxDistance);
			cachedGravity.Launch(velocity.normalized * maxVelocity * k);
		}
	}
	
	private void OnMouseDown() {
		if(helper == null) return;
		
		helper.gameObject.SetActive(true);
	}
}

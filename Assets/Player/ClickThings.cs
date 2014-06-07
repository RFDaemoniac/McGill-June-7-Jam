using UnityEngine;
using System.Collections;

public class ClickThings : MonoBehaviour {

	public float maxForce = 1000f;
	public float minForce = 200f;
	public float clickDistance = 20f;

	public float chargeTime = 1f;

	private float forceStrength;

	public GameObject force;

	// Use this for initialization
	void Start () {
		forceStrength = minForce;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);	
		RaycastHit hit;
		bool success = Physics.Raycast (ray, out hit, clickDistance);
		if (Input.GetMouseButton(0)) {
			forceStrength += (maxForce - minForce) * Time.deltaTime / chargeTime;
			forceStrength = Mathf.Min(forceStrength, maxForce);
		}
		if (success) {
			force.renderer.enabled = true;
			float zScale = 0.1f + (forceStrength - minForce) / (maxForce - minForce);
			force.transform.position = hit.point + hit.normal * zScale / 2f;
			force.transform.LookAt(hit.point + hit.normal);
			force.transform.localScale = new Vector3(0.1f,0.1f, zScale);
		} else {
			force.renderer.enabled = false;
		}
		if (Input.GetMouseButtonUp(0)) {
			forceStrength = minForce;
			if (success && hit.rigidbody != null) {
				force.renderer.enabled = false;
				Vector3 dir = -hit.normal;
				hit.rigidbody.AddForceAtPosition(dir * forceStrength, hit.point);
			}
		}

	}
	
}

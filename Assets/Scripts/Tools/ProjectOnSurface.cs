using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class ProjectOnSurface : MonoBehaviour
{
	private Transform planet;
	[SerializeField] private LayerMask includeLayer;
	[SerializeField] private float offset;
	private GameObject wrapper;
	void Awake() {
		planet = GameObject.FindWithTag("Planet").transform;
		wrapper = new GameObject(gameObject.name + "Wrapper");
		wrapper.transform.parent = this.transform.parent;
		this.transform.parent = wrapper.transform;
	}
	void Start() {
		RaycastHit hit;
		Physics.Linecast(transform.position, planet.position, out hit, includeLayer);
		wrapper.transform.up = hit.normal;
		Vector3 heightOffset = transform.up * transform.lossyScale.y/2;
		transform.position = hit.point + heightOffset + transform.up * offset;
	}
}

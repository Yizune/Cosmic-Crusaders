using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnInput : MonoBehaviour
{
	public string input;
	public float speed;
	public bool invert;
	private int directionModifier;
	void FixedUpdate() {
		if(invert)directionModifier = -1;
		else directionModifier = 1;
		transform.RotateAround(transform.position, directionModifier * transform.up * Input.GetAxisRaw(input), Time.deltaTime * speed);
	}
}

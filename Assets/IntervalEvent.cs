using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class IntervalEvent : MonoBehaviour
{
	public float interval;
	public UnityEvent onInterval;
	void Start() {
		StartCoroutine("Interval");
	}

	IEnumerator Interval () {
		yield return new WaitForSeconds(interval);
		onInterval.Invoke();
		StartCoroutine("Interval");
	}
}

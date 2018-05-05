using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour {
	Vector3 distanceWhenStart;

	public GameObject target;
	public float followSpeed;

	void Start() {
		distanceWhenStart = target.transform.position - transform.position;
	}
	
	// Update関数の直後に呼ばれる
	void LateUpdate() {
		// Vector3.Lerp は線形補間関数。第一引数と第二引数間で、第三引数の割合に相当する位置を返す
		transform.position = Vector3.Lerp(
			transform.position,
			target.transform.position - distanceWhenStart,
			Time.deltaTime * followSpeed
		);
	}
}

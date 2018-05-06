using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Title {
	public class NejikoController : MonoBehaviour {

		Quaternion initialRotation;
		public float rotateRange;
		public float rotateCycleTime;

		// Use this for initialization
		void Start() {
			initialRotation = transform.rotation;
		}
		
		// Update is called once per frame
		void Update() {
			float angle = rotateRange * Mathf.Sin(rotateCycleTime * Time.time);
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
			transform.rotation = initialRotation * q;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour {

	public GameObject[] lifeIcons;

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	// 残りライフに応じてスプライトの表示を決める
	public void UpdateLife(int life) {
		for(int i = 0; i < lifeIcons.Length; ++i) {
			if(i < life) {
				lifeIcons[i].SetActive(true);
			} else {
				lifeIcons[i].SetActive(false);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public NejikoController nejiko;
	public Text scoreLabel;
	public LifePanel lifePanel;

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		int score = CalcScore();
		scoreLabel.text = "Score: " + score + "m";

		lifePanel.UpdateLife(nejiko.Life());

		// ライフが 0 になったらゲームオーバー
		if(nejiko.Life() <= 0) {
			// Update処理を止める
			enabled = false;

			// 2秒待ち、タイトルへ戻す。 Invoke関数により遅延実行
			Invoke("ReturnToTitle", 2.0f);
		}
	}

	int CalcScore() {
		// 0地点からの走行距離をスコアとする
		return (int)nejiko.transform.position.z;
	}

	void ReturnToTitle() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
	}
}

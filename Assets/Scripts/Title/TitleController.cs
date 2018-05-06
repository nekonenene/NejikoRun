using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Title {
	public class TitleController : MonoBehaviour {

		public Text highScoreLabel;

		public void Start() {
			highScoreLabel.text = "High Score: " + PlayerPrefs.GetInt("HighScore") + "m";
		}

		public void OnStartButtonClicked() {
			// Application.LoadLevel は deprecated になったので代わりに以下
			UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
		}
	}
}

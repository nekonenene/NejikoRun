using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour {
	public void OnStartButtonClicked() {
		// Application.LoadLevel は deprecated になったので代わりに以下
		UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
	}
}

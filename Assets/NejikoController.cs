using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour {
	CharacterController controller;
	Animator animator;

	Vector3 moveDirection = Vector3.zero;

	public float gravity;
	public float speedZ;
	public float speedJump;

	// Use this for initialization
	void Start() {
		// 必要なコンポーネントを自動取得
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update() {
		// 地上にいる場合のみ操作を受け付ける
		if(controller.isGrounded) {
			// Input（タテ）を検知して前へ
			if(Input.GetAxis("Vertical") > 0.0f) {
				moveDirection.z = Input.GetAxis("Vertical") * speedZ;
			} else {
				moveDirection.z = 0;
			}

			// Input（横）に応じて方向転換
			transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

			// ジャンプ
			if(Input.GetButton("Jump")) {
				moveDirection.y = speedJump;
				animator.SetTrigger("jump"); // アニメーションクリップの切り替え
			}
		}

		// 重力を常に与える
		// Time.deltaTime : 前フレームからの経過時間
		// これをかけることによって、異なるフレームレートのデバイスでも一定の割合の値を加えられるようになる（らしい……）
		moveDirection.y -= gravity * Time.deltaTime;

		// 移動実行
		Vector3 globalDirection = transform.TransformDirection(moveDirection);
		controller.Move(globalDirection * Time.deltaTime);

		// 移動後接地しているなら、Y方向の速度（重力）はリセット
		if(controller.isGrounded) {
			moveDirection.y = 0;
		}

		// 速度が 0 以上なら、アニメーションクリップは run に
		animator.SetBool("run", moveDirection.z > 0.0f);
	}
}

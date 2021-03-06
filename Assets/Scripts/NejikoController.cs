﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour {
	const int MinLane = -2;
	const int MaxLane = 2;
	const float LaneWidth = 1.0f;

	const int DefaultLife = 3;
	const float StunDurationSec = 0.5f; // 気絶時間の秒数

	CharacterController controller;
	Animator animator;

	Vector3 moveDirection = Vector3.zero;
	int targetLane;
	int life = DefaultLife;
	float remainingStunTime = 0.0f;

	public float gravity;
	public float speedX; // 横方向の移動スピード
	public float speedZ; // 奥への移動スピード
	public float accelerationZ; // 奥方向への加速度
	public float speedJump;

	// Use this for initialization
	void Start() {
		// 必要なコンポーネントを自動取得
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetKeyDown("left")) MoveLane(toRight: false);
		if(Input.GetKeyDown("right")) MoveLane(toRight: true);
		if(Input.GetKeyDown("space")) Jump();

		if(IsStun()) {
			// 動きを止めつつ、気絶状態からの復帰カウントを進める
			moveDirection.x = 0.0f;
			moveDirection.z = 0.0f;
			remainingStunTime -= Time.deltaTime;
		} else {
			// ターゲットレーンに向かってX座標移動
			float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth; // 現在地とターゲットが離れてるほど値を大きく
			moveDirection.x = ratioX * speedX;

			// 奥向きへ、一定速度になるまで加速させる
			float ratioZ = moveDirection.z + (accelerationZ * Time.deltaTime);
			// Mathf.Clamp : 第一引数の値が、第二引数と第三引数の間を出ないように調整してくれる関数
			moveDirection.z = Mathf.Clamp(ratioZ, 0, speedZ);
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

	public int Life() {
		return life;
	}

	// スタン中か、Lifeが0になってるときtrue
	public bool IsStun() {
		return remainingStunTime > 0.0f || life <= 0;
	}

	// レーン移動
	public void MoveLane(bool toRight = true) {
		if(IsStun()) {
			return;
		}

		// 地上にいる場合のみ操作を受け付ける
		if(controller.isGrounded) {
			if(toRight && targetLane < MaxLane) {
				++targetLane;
			} else if(!toRight && MinLane < targetLane) {
				--targetLane;
			}
		}
	}

	public void Jump() {
		if(IsStun()) {
			return;
		}

		// 地上にいる場合のみ操作を受け付ける
		if(controller.isGrounded) {
			moveDirection.y = speedJump;
			animator.SetTrigger("jump"); // アニメーションクリップの切り替え
		}
	}

	// CharacterController との衝突が発生した際の処理
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(IsStun()) {
			return;
		}

		if(hit.gameObject.tag == "Obstacle") {
			// ライフを減らして気絶状態に移行
			--life;
			remainingStunTime = StunDurationSec;

			// ダメージ時のアニメーションクリップに移行
			animator.SetTrigger("damage");

			// 衝突した障害物オブジェクトは削除する
			Destroy(hit.gameObject);
		}
	}
}

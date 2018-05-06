using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePosition : MonoBehaviour {

	public GameObject obstaclePrefab; // 実体となる障害物オブジェクト

	// Use this for initialization
	void Start() {
		// 障害物を、呼び出し元オブジェクトと同位置に生成
		GameObject obstacle = (GameObject)Instantiate(
			                      obstaclePrefab,
			                      Vector3.zero,
			                      Quaternion.identity
		                      );

		// 障害物が一緒に削除されるよう、子オブジェクトとして設定
		obstacle.transform.SetParent(transform, false);
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	// 調節しやすいように
	void OnDrawGizmos() {
		// ギズモの底辺が地面と同じ高さになるよう、位置調整をおこなうベクトルを設定
		Vector3 offset = new Vector3(0, 0.5f, 0);
		Vector3 gizmosPosition = transform.position + offset;

		// 位置を示すための球を表示
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawSphere(gizmosPosition, 0.5f);

		// プレファブ名のアイコンを表示
		if(obstaclePrefab != null) {
			Gizmos.DrawIcon(gizmosPosition, obstaclePrefab.name, true);
		}
	}
}

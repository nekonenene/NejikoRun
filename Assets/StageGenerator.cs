using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour {

	const int MinStageTips = 2; // 最低でも作られているべきステージチップ数

	int currentTipIndex; // 現在作られているステージチップの先頭のインデックスナンバー

	public int stageTipSize = 30; // ステージ1個あたりのZ軸の長さ
	public Transform character;
	public GameObject[] stageTips;
	public int startTipIndex; // スタート地点のインデックスナンバー
	public int preInstantiate; // 先読みして生成する個数
	public GameObject parentObject;
	public List<GameObject> generatedStageList = new List<GameObject>(); // 生成したステージを管理するリスト

	// Use this for initialization
	void Start() {
		currentTipIndex = startTipIndex;
		UpdateStage(preInstantiate);
	}
	
	// Update is called once per frame
	void Update() {
		// キャラクターのz軸における位置から、現在のステージチップのインデックスを計算
		int charaPositionIndex = (int)(character.position.z / stageTipSize);

		int expectedTipIndex = charaPositionIndex + preInstantiate;
		if(expectedTipIndex > currentTipIndex) {
			UpdateStage(expectedTipIndex);
		}
	}

	// 指定インデックスまでのステージチップを生成し、管理下に置く
	void UpdateStage(int toTipIndex) {
		if(toTipIndex <= currentTipIndex) {
			return;
		}

		// 指定のステージチップまでを作成
		for(int i = currentTipIndex + 1; i <= toTipIndex; ++i) {
			GameObject stageObject = GenerateStage(i);

			// 管理リスト generatedStageList に追加
			generatedStageList.Add(stageObject);
		}

		// 必要以上に作られているなら削除
		while(generatedStageList.Count > preInstantiate + MinStageTips) {
			DestroyOldestStage();
		}

		currentTipIndex = toTipIndex;
	}

	// 指定インデックス位置にStageオブジェクトを生成
	GameObject GenerateStage(int tipIndex) {
		// ステージをランダム選択
		int nextStageTipIndex = Random.Range(0, stageTips.Length);
		GameObject nextStageTip = stageTips[nextStageTipIndex];

		GameObject stageObject = (GameObject)Instantiate(
			nextStageTip,
			new Vector3(0, 0, tipIndex * stageTipSize),
			Quaternion.identity
		);

		stageObject.transform.parent = parentObject.transform;

		return stageObject;
	}

	// 一番古いステージを削除
	void DestroyOldestStage() {
		GameObject oldestStage = generatedStageList[0];
		generatedStageList.RemoveAt(0);
		Destroy(oldestStage);
	}
}

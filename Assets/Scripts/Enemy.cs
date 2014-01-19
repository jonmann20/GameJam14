﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {
	protected int hitsLeft;

	private GameObject enemyDeath = Resources.Load<GameObject>("EnemyDeath");

	public abstract void TakeHit (Vector2 hitDirection);
	public abstract void TakeChainHit (Vector2 chainDirection, int chainType);
	public abstract void SetHitsLeft (int hitsLeft);

	string[] powerups = new string[]{ "ChainPowerup", "ShieldPowerup", "SpeedPowerup", "SprayPowerup"};

	public void die(bool countDeath = true){
		Debug.Log("Die called");
		if (countDeath) {
			++Game.numKilled;
			if(Game.numKilled % 15 == 1 && Game.numPowerups <= 3) {
				string powerup_name = powerups[Random.Range(0, powerups.Length)];
				Object powerup = Instantiate (Resources.Load(powerup_name), this.transform.position, this.transform.rotation);
				powerup.name = powerup_name;
				Game.numPowerups++;
			}
		}

		--GameLogic.EnemyCount;
		
		GameObject explosion = Instantiate(enemyDeath, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy (gameObject);
		Destroy(explosion, 1f);
	}

	public void Split(GameObject newType){
		
		Vector3 distanceVec = Player.entity[0].transform.position - Player.entity[1].transform.position;
		Vector3 directionVec = Vector3.Cross(distanceVec, new Vector3(0, 0, 1)).normalized;

		GameObject firstEnemy = Instantiate (newType, gameObject.transform.position + directionVec, Quaternion.identity) as GameObject;
		GameObject secondEnemy = Instantiate (newType, gameObject.transform.position - directionVec, Quaternion.identity) as GameObject;
		
		firstEnemy.GetComponent<Enemy>().SetHitsLeft(hitsLeft);
		secondEnemy.GetComponent<Enemy>().SetHitsLeft(hitsLeft);

		firstEnemy.transform.rigidbody2D.velocity = directionVec * 40.0f;;
		secondEnemy.transform.rigidbody2D.velocity = -directionVec * 40.0f;
		
		firstEnemy.GetComponent<Follow>().SetDelay(0.05f);
		secondEnemy.GetComponent<Follow>().SetDelay(0.05f);
		
		++GameLogic.EnemyCount;
		Destroy (gameObject);
		
		GameAudio.that.playEnemySliced(gameObject.transform.position);
	}
}

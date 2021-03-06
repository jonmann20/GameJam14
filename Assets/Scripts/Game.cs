﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public static GameObject pfPlayerPrimary, pfPlayerSecondary;
	public static int numKilled;
	public static int numPowerups;

	void Awake(){
		pfPlayerPrimary = Resources.Load("PlayerPrimary") as GameObject;
		pfPlayerSecondary = Resources.Load("PlayerSecondary") as GameObject;
	}
	
	void Start(){
		numKilled = 0;
		numPowerups = 0;

		Player.entity[0] = Instantiate(pfPlayerPrimary) as GameObject;
		Player.entity[1] = Instantiate(pfPlayerSecondary) as GameObject;
		
		Player.entity[0].GetComponent("CreateChain").SendMessage("ChainSetEnd", Player.entity[1]);
		
		Player.entity[1].SetActive(false);
	}
	
}

﻿using UnityEngine;
using System.Collections.Generic;
using Game;
using System;

public class GameLogic : MonoBehaviour {
	public float PlayTime { get; private set; }

	public int EnemiesSurvived { get; private set; }

	public int EnemiesKilled { get; private set; }

	public int Lives { get; private set; }
	
	public float Mana {
		get { 
			if (_spellManager == null)
				return 0f;
			return _spellManager.Mana; 
		}
	}

	public float GlobalGrowFactor {
		get {
			return _spellManager.GrowthFactor;
		}
	}

	public bool AreEnemiesFrozen {
		get {
			return _spellManager.AreEnemiesFrozen;
		}
	}
	
	public bool IsPoisonSpellActive {
		get {
			return _spellManager.IsPoisonSpellActive;
		}
	}

	public void DamageAllEnemies (float damage) {
		waveManager.CurrentWave.DamageAllEnemies(damage);
	}

	public GameObject enemyPrefab;
	public Transform enemyParent;
	public Checkpoint[] checkpoints;
	private WaveManager waveManager;
	private GameState _gameState;

	public Maze Maze { get; private set; }

	public GameObject firstTreePosition;
	public GameObject treePrefab;
	public Transform treeParent;
	public GameObject[] Carrots;
	private SpellManager _spellManager;
	
	public int RemainingWaves {
		get {
			return waveManager.RemainingWaves;
		}
	}
	
	public static GameLogic I {
		get {
			return FindObjectOfType<GameLogic> ();
		}
	}

	void Start () {
		_gameState = GameState.Running;
		Lives = 10;

		InitFirstTree ();

		Maze = new Maze (gameObject, checkpoints);
		_spellManager = new SpellManager (this);

		waveManager = new WaveManager (this, enemyPrefab, Maze, enemyParent);
		waveManager.StartNextWave ();
	}

	void Update () {
		PlayTime += TimeManager.GetDeltaTime ();
		waveManager.Update ();
		_spellManager.Update ();
	}

	void InitFirstTree () {
		GameObject treeObject = (GameObject)Instantiate (treePrefab, firstTreePosition.transform.position, Quaternion.identity);	
		treeObject.transform.parent = treeParent;
		treeObject.name = "FirstTree";
		Treee tree = treeObject.GetComponent<Treee> ();
		tree.Root = Branch.InstantiateRoot (tree, null, tree.transform); 
	}

	public void Finished (Enemy enemy) {
		if (enemy.Dead)
			Killed (enemy);
		else if (enemy.Survived)
			Survived (enemy);
		else
			throw new ApplicationException ("Invalid enemy state: " + enemy.State);
	}

	public void Survived (Enemy enemy) {
		EnemiesSurvived += 1;
		Lives -= 1;
		if (Lives <= 0) {
			GameLost ();
			return;
		}
		Destroy (Carrots [Lives]);
	}

	public void Killed (Enemy enemy) {
		EnemiesKilled += 1;
	}

	private void GameLost () {
		if (_gameState == GameState.Running)
			_gameState = GameState.Lost;
	}

	public void AllWavesFinished () {
		if (_gameState == GameState.Running) {
			_gameState = GameState.Won;
			LevelSelection.UnlockNextLevel ();
		}
	}

	private bool showHelp = false;

	public void OnGUI () {
		int btnWidth = 200;
		int btnHeight = 50;

		if (GUI.Button (new Rect (Screen.width - 160, 10, 32, 30), "▮▮"))
			TimeManager.Pause ();
		if (GUI.Button (new Rect (Screen.width - 120, 10, 32, 30), "▶"))
			TimeManager.Speed1 ();
		if (GUI.Button (new Rect (Screen.width - 80, 10, 32, 30), "▶▶"))
			TimeManager.Speed2 ();
		if (GUI.Button (new Rect (Screen.width - 40, 10, 32, 30), "▶▶▶"))
			TimeManager.Speed3 ();

		SpellButtons ();

		if (GUI.Button (new Rect (230, 10, 100, 30), "Help"))
			showHelp = !showHelp;

		if (GUI.Button (new Rect (10, 10, 100, 30), "Back to Menu"))
			new LevelSelection ().LoadLevels ();
		
		if (GUI.Button (new Rect (120, 10, 100, 30), "Restart Level"))
			new LevelSelection ().ReplayLevel ();

		if (showHelp) {
			GUI.TextArea (new Rect ((Screen.width / 2) - 150, (Screen.height / 2) - 100, 300, 220), "HOW TO PLAY\n\n" +
				"Break of branches to plant new trees:\n" +
				"- Select branch\n" +
				"- Click white ring\n" +
				"- Click on terrain to place the new tree\n\n" +
				"Scroll Wheel = Zoom\n" +
				"Shift + Scroll Wheel = Rotate\n" +
				"Two fingers = Drag map\n" +
				"Three fingers = Rotate map\n"
			);
		}

		if (_gameState == GameState.Won) {
			GUI.Box (new Rect ((Screen.width / 2) - 110, (Screen.height / 2) - 60, 220, 140), "You won, congratulations!");

			GUI.BeginGroup (new Rect ((Screen.width / 2) - btnWidth / 2, (Screen.height / 2) - 60, btnWidth, 130));

			if (GUI.Button (new Rect (0, 25, btnWidth, btnHeight), "Continue to next Level!"))
				new LevelSelection ().LoadNextLevel ();

			if (GUI.Button (new Rect (0, 80, btnWidth, btnHeight), "Back"))
				new LevelSelection ().LoadLevels ();
            
			GUI.EndGroup ();
		} else if (_gameState == GameState.Lost) {
			GUI.Box (new Rect ((Screen.width / 2) - 110, (Screen.height / 2) - 60, 220, 140), "Sorry, you lost :(");
			
			GUI.BeginGroup (new Rect ((Screen.width / 2) - btnWidth / 2, (Screen.height / 2) - 60, btnWidth, 130));
			
			if (GUI.Button (new Rect (0, 25, btnWidth, btnHeight), "Play Again!"))
				new LevelSelection ().ReplayLevel ();
			
			if (GUI.Button (new Rect (0, 80, btnWidth, btnHeight), "Back"))
				new LevelSelection ().LoadLevels ();
			
			GUI.EndGroup ();
		}
	}

	void SpellButtons () {
		if (_spellManager.CanUseGrowth ()) {
			GUI.enabled = _spellManager.EnoughGrowthMana ();
			if (GUI.Button (new Rect (10, Screen.height - 40, 62, 30), "Growth"))
				_spellManager.Growth ();
		}
		if (_spellManager.CanUseBurn ()) {
			GUI.enabled = _spellManager.EnoughBurnMana ();
			if (GUI.Button (new Rect (10, Screen.height - 80, 62, 30), "Burn"))
				_spellManager.Burn ();
		}
		if (_spellManager.CanUsePoison ()) {
			GUI.enabled = _spellManager.EnoughPoisonMana ();
			if (GUI.Button (new Rect (10, Screen.height - 120, 62, 30), "Poison"))
				_spellManager.Poison ();
		}
		if (_spellManager.CanUseFreeze ()) {
			GUI.enabled = _spellManager.EnoughFreezeMana ();
			if (GUI.Button (new Rect (10, Screen.height - 160, 62, 30), "Freeze"))
				_spellManager.Freeze ();
		}
		GUI.enabled = true;
	}
}
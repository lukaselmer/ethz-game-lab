using UnityEngine;
using System.Collections.Generic;
using Game;
using System;

public class GameLogic : MonoBehaviour {
    public float PlayTime { get; private set; }

    public int EnemiesSurvived { get; private set; }

    public int EnemiesKilled { get; private set; }

    public int Lives { get; private set; }

    public GameObject enemyPrefab;
    public Transform enemyParent;
    public Checkpoint[] checkpoints;

    private WaveManager waveManager;
    private GameState _gameState;

    public Maze Maze { get; private set; }

	public GameObject firstTreePosition;
	public GameObject treePrefab;
	public Transform treeParent;

    public static GameLogic I {
        get {
            return FindObjectOfType<GameLogic>();
        }
    }

    void Start() {
        _gameState = GameState.Running;
        Lives = 10;

		InitFirstTree ();

        Maze = new Maze(gameObject, checkpoints);

        waveManager = new WaveManager(this, enemyPrefab, Maze, enemyParent);
        waveManager.StartNextWave();
    }

    void Update() {
        PlayTime += Time.deltaTime;
        waveManager.Update();
    }

	void InitFirstTree() {
		
		GameObject treeObject = (GameObject)Instantiate (treePrefab, firstTreePosition.transform.position, Quaternion.identity);	
		treeObject.transform.parent = treeParent;
		treeObject.name = "FirstTree";
		Treee tree = treeObject.GetComponent<Treee> ();
		tree.Root = Branch.InstantiateRoot (tree, null, tree.transform); 
	}

    public void Finished(Enemy enemy) {
        if (enemy.Dead)
            Killed(enemy);
        else if (enemy.Survived)
            Survived(enemy);
        else
            throw new ApplicationException("Invalid enemy state: " + enemy.State);
    }

    public void Survived(Enemy enemy) {
        EnemiesSurvived += 1;
        Lives -= 1;
        if (Lives < 0)
            GameLost();
    }

    public void Killed(Enemy enemy) {
        EnemiesKilled += 1;
    }

    private void GameLost() {
        if (_gameState == GameState.Running)
            _gameState = GameState.Lost;
    }

    public void AllWavesFinished() {
        if (_gameState == GameState.Running)
            _gameState = GameState.Won;
    }

    public void OnGUI() {
        if (_gameState == GameState.Won) {
            GUI.BeginGroup(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 60, 100, 120));

            GUI.Label(new Rect(0, 0, 100, 20), "You won, congratulations!");

            if (GUI.Button(new Rect(0, 20, 100, 50), "Continue to next Level!"))
                Application.LoadLevel("Levels");
            
            GUI.EndGroup();
        }
        else if (_gameState == GameState.Lost) {
            GUI.BeginGroup(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 60, 100, 120));

            GUI.Label(new Rect(0, 0, 100, 20), "Sorry, you lost :(");

            if (GUI.Button(new Rect(0, 20, 100, 50), "Play Again"))
                Application.LoadLevel(Application.loadedLevelName);

            if (GUI.Button(new Rect(0, 70, 100, 50), "Quit"))
                Application.LoadLevel("Levels");

            GUI.EndGroup();
        }
    }


}
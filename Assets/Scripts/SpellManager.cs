using UnityEngine;
using System.Collections.Generic;
using Game;
using System;

class SpellManager {
	private GameLogic _gameLogic;

	private float _growthSpellDuration = 0;

	public float Mana { get; private set; }

	public float GrowthFactor {
		get {
			return _growthSpellDuration > 0 ? 5 : 1;
		}
	}
	
	public SpellManager (GameLogic gameLogic) {
		_gameLogic = gameLogic;
		Mana = 50.0f;
	}

	public void Update () {
		Mana += TimeManager.GetDeltaTime () * 3f;
		_growthSpellDuration = Mathf.Max(_growthSpellDuration - TimeManager.GetDeltaTime(), 0f);
	}
	
	public bool CanUseGrowth () {
		return LevelSelection.UnlockedLevelID >= 1;
	}
	
	public bool CanUseBurn () {
		return LevelSelection.UnlockedLevelID >= 2;
	}
	
	public bool CanUsePoison () {
		return LevelSelection.UnlockedLevelID >= 3;
	}
	
	public bool CanUseFreeze () {
		return LevelSelection.UnlockedLevelID >= 4;
	}

	public bool EnoughGrowthMana () {
		return Mana > 25;
	}

	public bool EnoughBurnMana () {
		return Mana > 100;
	}

	public bool EnoughPoisonMana () {
		return Mana > 75;
	}

	public bool EnoughFreezeMana () {
		return Mana > 50;
	}

	public void Growth () {
		Mana -= 50;
		_growthSpellDuration = 10;
	}

	public void Burn () {
		throw new NotImplementedException ();
	}

	public void Poison () {
		throw new NotImplementedException ();
	}

	public void Freeze () {
		throw new NotImplementedException ();
	}
}

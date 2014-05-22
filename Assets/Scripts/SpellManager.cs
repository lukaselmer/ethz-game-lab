using UnityEngine;
using System.Collections.Generic;
using Game;
using System;

class SpellManager {
	const int FREEZE_MANA = 50;
	const int POISON_MANA = 75;
	const int BURN_MANA = 50;
	const int GROWTH_MANA = 50;

	private GameLogic _gameLogic;
	private float _growthSpellDuration = 0;
	private float _freezeSpellDuration = 0;
	private float _poisonSpellDuration = 0;

	public float Mana { get; private set; }

	public float GrowthFactor {
		get {
			return _growthSpellDuration > 0 ? 5 : 1;
		}
	}
	
	public bool AreEnemiesFrozen {
		get {
			return _freezeSpellDuration > 0;
		}
	}
	
	public bool IsPoisonSpellActive {
		get {
			return _poisonSpellDuration > 0;
		}
	}
	
	public SpellManager (GameLogic gameLogic) {
		_gameLogic = gameLogic;
		Mana = 50.0f;
	}

	public void Update () {
		Mana += TimeManager.GetDeltaTime () * 3f;
		_growthSpellDuration = Mathf.Max (_growthSpellDuration - TimeManager.GetDeltaTime (), 0f);
		_freezeSpellDuration = Mathf.Max (_freezeSpellDuration - TimeManager.GetDeltaTime (), 0f);
		_poisonSpellDuration = Mathf.Max (_poisonSpellDuration - TimeManager.GetDeltaTime (), 0f);
	}
	
	public bool CanUseGrowth () {
		return LevelSelection.UnlockedLevelID >= 1 && _growthSpellDuration <= 0;
	}
	
	public bool CanUseBurn () {
		return LevelSelection.UnlockedLevelID >= 2;
	}
	
	public bool CanUsePoison () {
		return LevelSelection.UnlockedLevelID >= 3 && !IsPoisonSpellActive;
	}
	
	public bool CanUseFreeze () {
		return LevelSelection.UnlockedLevelID >= 4 && !AreEnemiesFrozen;
	}

	public bool EnoughGrowthMana () {
		return Mana > GROWTH_MANA;
	}

	public bool EnoughBurnMana () {
		return Mana > BURN_MANA;
	}

	public bool EnoughPoisonMana () {
		return Mana > POISON_MANA;
	}

	public bool EnoughFreezeMana () {
		return Mana > FREEZE_MANA;
	}

	public void Growth () {
		Mana -= GROWTH_MANA;
		_growthSpellDuration = 10;
	}

	public void Burn () {
		Mana -= BURN_MANA;
		_gameLogic.DamageAllEnemies(5.0f);
	}

	public void Poison () {
		Mana -= POISON_MANA;
		_poisonSpellDuration = 5;
	}

	public void Freeze () {
		Mana -= FREEZE_MANA;
		_freezeSpellDuration = 5;
	}
}

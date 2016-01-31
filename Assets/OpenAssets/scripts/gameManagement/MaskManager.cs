using UnityEngine;
using System.Collections;

public class MaskManager : PersistentSingleton<MaskManager>, IPlayerRespawnListener {
	public int MaskChangePrice = 0;
	// Zero is when the character is without masks.
	// This is the mask that the character is using.
	public int MaskInUse = 0;

	// This is the selected mask.
	public int _selectedMask = 0;

	// Number of masks in the game.
	protected int _numberOfMasks = 3;

	// A byte indicating which masks the player possesses.
	public int _possessedMasks = 0;

	/// <summary>
	/// this method resets the whole game manager
	/// </summary>
	public virtual void Reset()
	{
		_possessedMasks = 0;
		_selectedMask = 0;
		MaskInUse = 0;
	}

	protected bool HasMask(int i)
	{
		if (i == 0)
			return false;
		
		return (_possessedMasks & (1 << i - 1)) != 0;
	}

	public bool TryToWearMask()
	{
		if (_selectedMask != 0) 
		{
			bool hasSelectedMask = HasMask(_selectedMask);
			Debug.Log ("At time " + Time.time + " player has mask " + _selectedMask + " : " + hasSelectedMask);

			if (hasSelectedMask && GameManager.Instance.Points >= MaskChangePrice && _selectedMask != MaskInUse) 
			{
				GameManager.Instance.AddPoints (-1 * MaskChangePrice);
				MaskInUse = _selectedMask;
				return true;
			}

			return false;
		} 
		else 
		{
			MaskInUse = 0;
			return true;
		}
	}

	public void GetMask(int i)
	{
		if (i < 1)
			return;
		// Each bit indicates which mask the player has.
		_possessedMasks = _possessedMasks | (1 << (i-1));
	}

	public void ChangeMask()
	{
		// Round-robin selection
		do {
			_selectedMask++;
			if (_selectedMask > _numberOfMasks) {
				_selectedMask = 0;
			}
		} while(_selectedMask != 0 && !HasMask (_selectedMask));

		Debug.Log ("Mask selected: " + _selectedMask);
	}

	/// <summary>
	/// When the player respawns, we reinstate the object
	/// </summary>
	/// <param name="checkpoint">Checkpoint.</param>
	/// <param name="player">Player.</param>
	public virtual void onPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, CharacterBehavior player)
	{
		Reset ();
	}
}

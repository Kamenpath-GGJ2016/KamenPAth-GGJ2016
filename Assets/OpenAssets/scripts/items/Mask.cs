using UnityEngine;
using System.Collections;

public class Mask : MonoBehaviour {
	/// The effect to instantiate when the mask is hit
	public GameObject Effect;
	/// The type of mask to be obtained
	public int MaskNumber = 1;

	/// <summary>
	/// Triggered when something collides with the coin
	/// </summary>
	/// <param name="collider">Other.</param>
	public virtual void OnTriggerEnter2D (Collider2D collider) 
	{
		// if what's colliding with the mask ain't a characterBehavior, we do nothing and exit
		if (collider.GetComponent<CharacterBehavior>() == null)
			return;

		// We pass the specified amount of points to the game manager
		MaskManager.Instance.GetMask(MaskNumber);
		// adds an instance of the effect at the coin's position
		Instantiate(Effect,transform.position,transform.rotation);
		// we desactivate the gameobject
		gameObject.SetActive(false);
	}
	/// <summary>
	/// When the player respawns, we reinstate the object
	/// </summary>
	/// <param name="checkpoint">Checkpoint.</param>
	/// <param name="player">Player.</param>
	public virtual void onPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, CharacterBehavior player)
	{
		gameObject.SetActive(true);
	}
}

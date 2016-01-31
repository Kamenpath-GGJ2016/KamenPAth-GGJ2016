using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class MaskController : MonoBehaviour 
{
	public GameObject[] MaskChooseEffects;

	protected Transform _playerTransform;

	void Start () {
		// we get the player from its tag
		_playerTransform = GameManager.Instance.Player.transform;
		if (MaskChooseEffects.Length != 4) 
		{
			Debug.LogError ("Initialize all Mask controller variables.");
			GameObject.Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown ("Melee")) 
		{
			MaskManager.Instance.TryToWearMask ();
		}

		if(CrossPlatformInputManager.GetButtonDown ("Fire"))
		{
			MaskManager.Instance.ChangeMask ();
			ShowMaskEffect ();
		}
	}

	protected void ShowMaskEffect()
	{
		Vector3 maskEffectPosition = new Vector3 (_playerTransform.position.x, 
			_playerTransform.position.y + 2, 
			_playerTransform.position.z);
		GameObject.Instantiate (MaskChooseEffects [MaskManager.Instance._selectedMask],
			maskEffectPosition, _playerTransform.rotation);
		// GameObject go = GameObject.Instantiate (MaskChooseEffects [MaskManager.Instance._selectedMask],
//			maskEffectPosition, _playerTransform.rotation) as GameObject;
		// go.transform.SetParent (_playerTransform);
	}
}
			
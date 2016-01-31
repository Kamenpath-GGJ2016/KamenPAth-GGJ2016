using UnityEngine;
using System.Collections;

public class MaskPlacing : MonoBehaviour {
	protected Transform _playerTransform;
	public float deltaX = 0;
	public float deltaY = 3;
	// Use this for initialization
	void Start () {
		// we get the player from its tag
		_playerTransform = GameManager.Instance.Player.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3 (_playerTransform.position.x + deltaX,
			_playerTransform.position.y + deltaY, 
			_playerTransform.position.z);
		transform.localScale = _playerTransform.localScale;
	}
}
			 
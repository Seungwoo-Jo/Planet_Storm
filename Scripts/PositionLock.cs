using UnityEngine;
using System.Collections;

public class PositionLock : MonoBehaviour {

    private Vector3 _LockedPosition;

	// Use this for initialization
	void Start () {
        _LockedPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = _LockedPosition;
	}
}

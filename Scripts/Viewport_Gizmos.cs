using UnityEngine;
using System.Collections;

public class Viewport_Gizmos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnDrawGizmos()
    {
        Vector3 _size = GetComponent<BoxCollider2D>().size;
        _size.x *= transform.localScale.x;
        _size.y *= transform.localScale.y;
        Gizmos.DrawWireCube(Vector3.zero, _size);
    }

	// Update is called once per frame
	void Update () {
	
	}
}

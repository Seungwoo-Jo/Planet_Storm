﻿using UnityEngine;
using System.Collections;

public class DestoryEffect : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(animator.GetBool("End")) {
            Destroy(this.gameObject);
        }
	}
}

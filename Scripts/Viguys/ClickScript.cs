using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour {

    // Use this for initialization
    public GameObject attached;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown() {
        transform.parent = null;
        Destroy(gameObject);
        if (attached != null) Destroy(attached);
    }
}

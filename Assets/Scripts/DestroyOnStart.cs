using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnStart : MonoBehaviour {

	void Start () {

	}

	void Update () {

            if (gc.GameActive)
                Destroy(this.gameObject);
	}
}

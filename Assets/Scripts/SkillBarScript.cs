using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarScript : MonoBehaviour {

    private Slider _skillbar;
    private bool _isfull;

    public bool Isfull
    {
        get
        {
            return _isfull;
        }

        set
        {
            _isfull = value;
        }
    }

    // Use this for initialization
    void Start () {
        _skillbar = GameObject.Find("SkillBar").GetComponent<Slider>();
        _skillbar.value = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!_isfull)
        {
            _skillbar.value+=0.10f;
            if (_skillbar.value == _skillbar.maxValue)
            {
                _isfull = true;
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarScript : MonoBehaviour {

    private Slider _skillbar;
    private bool _isfull;
    private float _staminaAmount;
    private GameController _gameController;

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

    public float StaminaAmount
    {
        get
        {
            return _staminaAmount;
        }

        set
        {
            _staminaAmount = value;
        }
    }

    // Use this for initialization
    void Start () {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _skillbar = GameObject.Find("SkillBar").GetComponent<Slider>();
        _skillbar.value = 0.001f;
	}
	
	// Update is called once per frame
	void Update () {
        if (_gameController.GameActive)
        {
            if (!_isfull)
            {
                _staminaAmount += 0.050f;
            }
            _skillbar.value = _staminaAmount;
            if (_skillbar.value == _skillbar.maxValue)
            {
                _isfull = true;
            }
            else
            {
                _isfull = false;
            }
        }
    }
}

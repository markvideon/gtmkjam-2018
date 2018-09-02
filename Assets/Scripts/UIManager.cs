using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    private int score;
    private float gametime = 0f;
    private TextMeshProUGUI textField;

    // Use this for initialization
    void Start () {
        textField = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void LateUpdate () {

        gametime += Time.deltaTime;
	}

    public void IncrementScore(int points) {
        score += points;
        textField.text = "Score: " + score + " Time: " + gametime;
    }
    

    public float GetTime() {
        return gametime;
    }
}

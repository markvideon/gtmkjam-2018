using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComboManager : MonoBehaviour {

    private int currentCombo = 0;
    private int keyPressed = 0;

    private float comboDuration = 0f;

    [SerializeField]
    private float comboTimeout = 4.0f;


	public void LateUpdate () {

        /* Tick happens here. Condition is true if timer is currently running.
         * Timer starts running after first key press in an attempted combo. */
        if (comboDuration > 0f) {
            comboDuration += Time.deltaTime;
        }

        // Mapping input sequences to integers. Up = 1, Left = 2...
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            keyPressed = 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            keyPressed = 2;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            keyPressed = 3;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            keyPressed = 4;
        }

        // If a key press detected
        if (keyPressed > 0) {
            IncrementInput(keyPressed);
            keyPressed = 0;

            // If the timer is at rest, start running
            if (Mathf.Approximately(comboDuration, Mathf.Epsilon)) {
                comboDuration += Time.deltaTime;
            }
        }
        

        // If four keys have been pressed.
        if (currentCombo > 1000) {
            PerformControl();
            comboDuration = 0f;
           
        } // If time limit has been exceeded.
        else if (comboDuration > comboTimeout) {
            comboDuration = 0f;
            currentCombo = 0;
        }
    }

    /*  Combinations are plainly readable left-to-right 
        as decimal numbers.
    */
    private void IncrementInput(int nextInput) {
        currentCombo *= 10;
        currentCombo += nextInput;
    }

    // Requires hardcoded combos at present.
    private void PerformControl() {

        if (currentCombo == 1331) {
            Debug.Log("Up Down Down Up");
        }

        // Reset combo after performance.
        currentCombo = 0;
    }
     
}

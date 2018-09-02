using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComboManager : MonoBehaviour {

    private int currentCombo = 0;
    private int keyPressed = 0;

    private float comboDuration = 0f;

    [SerializeField] private float comboTimeout = 4.0f;


	public void LateUpdate () {

        /* Scrapped.
         * Tick happens here. Condition is true if timer is currently running.
         * Timer starts running after first key press in an attempted combo. 
        if (comboDuration > 0f) {
            comboDuration += Time.deltaTime;
        }
        */

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
            //IncrementInput(keyPressed);
            currentCombo = keyPressed;
            keyPressed = 0;

            // If the timer is at rest, start running
            if (Mathf.Approximately(comboDuration, Mathf.Epsilon)) {
                comboDuration += Time.deltaTime;
            }
        }
        

          
    }

    /*  Combinations are plainly readable left-to-right 
        as decimal numbers. Store three most recent presses.
    */
    private void IncrementInput(int nextInput) {
        currentCombo = currentCombo % 100;
        currentCombo *= 10;
        currentCombo += nextInput;
    }
     
    public int supplyCombo() {
        return currentCombo;
    }

    public void resetCombo() {
        currentCombo = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeController : MonoBehaviour
{

    [SerializeField] Transform[] spawnTransforms = new Transform[4];
    [SerializeField] private float speedFactor = 1f;
    private ComboManager gameComboManager;
    private UIManager gameUIManager;

    private int nextSpawnIndex;
    private Vector3 movementDirection;
    private int killCombo;
    private int scoreIncrement = 10;


    public void Start()
    {
        gameComboManager = FindObjectOfType<ComboManager>();
        gameUIManager = FindObjectOfType<UIManager>();
        OnDeath();
    }

    public void LateUpdate()
    {
        if (gameUIManager.GetTime()< 60f) {

        
            // If the foe is moving downward from left or right of player
            if (nextSpawnIndex == 0 || nextSpawnIndex == 1)
            {
                movementDirection.x = 0f;
                movementDirection.y = -1f;
                movementDirection.z = 0f;
            }
            // If the foe is moving rightward above or below the player
            else
            {
                movementDirection.x = 1f;
                movementDirection.y = 0f;
                movementDirection.z = 0f;

            }

        }

        transform.position += movementDirection * speedFactor * Time.deltaTime;
    }

    // If out of bounds, re-pool. 
    public void OnBecameInvisible()
    {
        OnDeath();
        scoreIncrement--;
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (killCombo == gameComboManager.supplyCombo() && 
            collider.name == "Gate")
        {
            /* If Combo manager is not reset, and the user does not press
             * additional keys, problems will arise...*/
            gameComboManager.resetCombo();

            speedFactor += 0.04f;
            scoreIncrement+= 7;
            gameUIManager.IncrementScore(scoreIncrement);
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // Each foe resets its position on death
        nextSpawnIndex = Random.Range(0, spawnTransforms.Length);

        // Set the kill combo based on nextSpawnIndex
        switch (nextSpawnIndex)
        {
            case 0:
                killCombo = 2; // top left
                break;
            case 1:
                killCombo = 4; // top right
                break;
            case 2:
                killCombo = 1; // left top
                break;
            case 3:
                killCombo = 3; // left bottom
                break;
        }


        if (spawnTransforms[nextSpawnIndex])
        {
            this.transform.position = spawnTransforms[nextSpawnIndex].position;
        }
        else
        {
            Debug.Log("Cannot spawn object as chosen index corresponds to" +
                      " undefined element. See FoeController.cs, and project " +
                      "execution order.");
        }
    }


}

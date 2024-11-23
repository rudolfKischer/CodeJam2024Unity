using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public NewFighter player; // Reference to the NewFighter (player) object

    public NewFighter opponent; // Reference to the Opponent object
    // Start is called before the first frame update
    public InputData poseInput;

    public float currentTime = 0.0f;

    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        player.poseInput = new InputData();

        if (currentTime > 0.5f)
        {
            player.poseInput.direction = Random.Range(4, 7);
            player.poseInput.bPressed = Random.Range(0, 2) == 1;
            player.poseInput.jumpPressed = Random.Range(0, 2) == 1;
            
        }
        if (currentTime > 2.0f)
        {
            currentTime = 0.0f;
        }

        // Update time
        currentTime += Time.deltaTime;
    }
}

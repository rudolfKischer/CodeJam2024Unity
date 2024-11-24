using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAction
{
    public string actionName;
    public float minActivationTime;
    public float maxActivationTime;
    public float minCooldown;
    public float maxCooldown;
    public float weight;

    public AIAction(string actionName, float minActivationTime, float maxActivationTime, float minCooldown, float maxCooldown)
    {
        this.actionName = actionName;
        this.minActivationTime = minActivationTime;
        this.maxActivationTime = maxActivationTime;
        this.minCooldown = minCooldown;
        this.maxCooldown = maxCooldown;

    }
}

public class AIPlayer : MonoBehaviour
{
    public NewFighter player; // Reference to the NewFighter (player) object

    public NewFighter opponent; // Reference to the Opponent object
    // Start is called before the first frame update
    public InputData poseInput;

    public float cooldown = 0.0f;
    public float activationTime = 0.0f;

    public float currentTime = 0.0f;
    public float walkTimer = 0.0f;


    public int walkDirection = 0;

    public AIAction currentAction = null;

    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        player.poseInput = new InputData();

        // // Options
        // // 1. randomly pick a direction to walk in
        // // 2. attack b
        // // 3. attack c
        // // 4. jump

        // // define a list of actions

        // // AIAction jumpAction = new AIAction("jump", 0.0f, 0.05f, 0.1f, 0.5f);
        // // AIAction walkLeftAction = new AIAction("walkLeft", 0.0f, 0.2f, 0.1f, 0.5f);
        // // AIAction walkRightAction = new AIAction("walkRight", 0.0f, 0.5f, 0.1f, 0.5f);
        // AIAction attackAAction = new AIAction("attackA", 0.0f, 0.5f, 0.1f, 0.5f);
        // AIAction attackBAction = new AIAction("attackB", 0.0f, 0.05f, 0.1f, 0.5f);
        // AIAction attackCAction = new AIAction("attackC", 0.0f, 0.05f, 0.1f, 0.5f);

        // List<AIAction> actions = new List<AIAction>();
        // // actions.Add(jumpAction);
        // // actions.Add(walkLeftAction);
        // // actions.Add(walkRightAction);
        // actions.Add(attackAAction);
        // actions.Add(attackBAction);
        // actions.Add(attackCAction);

        // // use cumulative probability to pick an action
        // float totalWeight = 0.0f;
        // foreach (AIAction action in actions)
        // {
        //     totalWeight += action.weight;
        // }

        // // float randomValue = Random.Range(0.0f, totalWeight);
        // // AIAction selectedAction = null;
        // // float cumulativeWeight = 0.0f;
        // // foreach (AIAction action in actions)
        // // {
        // //     cumulativeWeight += action.weight;
        // //     if (randomValue <= cumulativeWeight)
        // //     {
        // //         selectedAction = action;
        // //         break;
        // //     }
        // // }


        // // just pick a random action of equal prob from the list
        // AIAction selectedAction = actions[Random.Range(0, actions.Count)];

        // // if the cooldown is 0 and the activation time is 0
        // // then we pick a random action

        // // then we we set the activation time to a random value between the min and max activation time
        // // then if activation time is 


        // // we pick a random one of these options
        // // then we pick a time to activate it for 
        // // and set the current action to that action
        // //then while the current action is not null, we decrement the activation time
        // // and activate the action

        // selectedAction = attackBAction;
        // currentAction = selectedAction;


        // if (activationTime <= 0.0f && cooldown <= 0.0f)
        // {

        //     currentAction = selectedAction;
        //     activationTime = Random.Range(currentAction.minActivationTime, currentAction.maxActivationTime);
        //     cooldown = Random.Range(currentAction.minCooldown, currentAction.maxCooldown);
        // }

        // if (activationTime > 0.0f)
        // {
        //     activationTime -= Time.deltaTime;
        // }

        // if (activationTime <= 0.0f && cooldown > 0.0f)
        // {
        //     cooldown -= Time.deltaTime;
        // }

        // if (true)
        // {
        //     if (currentAction.actionName == "jump")
        //     {
        //         player.poseInput.jumpPressed = true;
        //     }
        //     else if (currentAction.actionName == "walkLeft")
        //     {
        //         player.poseInput.direction = 4;
        //     }
        //     else if (currentAction.actionName == "walkRight")
        //     {
        //         player.poseInput.direction = 6;
        //     }
        //     else if (currentAction.actionName == "attackA")
        //     {
        //         player.poseInput.aPressed = true;
        //     }
        //     else if (currentAction.actionName == "attackB")
        //     {
        //         player.poseInput.bPressed = true;
        //     }
        //     else if (currentAction.actionName == "attackC")
        //     {
        //         player.poseInput.cPressed = true;
        //     }
        // }

        // player.poseInput.bPressed = true;

        if (currentTime > 1.5f && currentTime < 1.8f) {

            player.poseInput.jumpPressed = Random.Range(0, 2) == 1;   
            // player.poseInput.direction = Random.Range(4, 7);
            player.poseInput.bPressed = Random.Range(0, 2) == 1;
            player.poseInput.cPressed = Random.Range(0, 2) == 1;
            player.poseInput.aPressed = Random.Range(0, 2) == 1;
        } 

        if (currentTime > 3.0f)
        {
            currentTime = 0.0f;
        }

        // Update time
        currentTime += Time.deltaTime;
        if (walkTimer >= 0.0f)
        {
            walkTimer -= Time.deltaTime;
        }
        else
        {
            walkTimer = Random.Range(0.5f, 0.7f);
            walkDirection = Random.Range(4, 7);
        }

        player.poseInput.direction = walkDirection;

    }
}

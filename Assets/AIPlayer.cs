using System.Collections;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public NewFighter player;
    public NewFighter opponent;
    public InputData poseInput;

    public float currentTime = 0.0f;

    [SerializeField]
    private float punchDistance = 1.0f;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float backwardStepDistance = 0.5f;
    [SerializeField]
    private float movementDecisionDuration = 1.0f;

    private Vector3 currentDirection;
    private float decisionTimer = 0.0f;
    private bool isMovingBackward = false;

    private float EstimateDistance()
    {
        if (player == null || opponent == null)
        {
            Debug.LogError("Player or Opponent reference is missing!");
            return float.MaxValue;
        }

        Vector3 playerPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 opponentPosition = new Vector3(opponent.transform.position.x, 0, opponent.transform.position.z);
        return Vector3.Distance(playerPosition, opponentPosition);
    }

    void Start()
    {
        ChooseMovementDirection();
    }

    void Update()
    {
        if (player == null || opponent == null)
        {
            Debug.LogError("Player or Opponent reference is missing!");
            return;
        }

        float distanceToOpponent = EstimateDistance();
        Debug.Log($"Distance to opponent: {distanceToOpponent}");

        if (distanceToOpponent > punchDistance)
        {
            MoveTowardsOpponent();
        }
        else
        {
            TriggerPunch();
        }

        decisionTimer += Time.deltaTime;
        if (decisionTimer >= movementDecisionDuration)
        {
            decisionTimer = 0.0f;
            ChooseMovementDirection();
        }
    }

    private void ChooseMovementDirection()
    {
        if (Random.value > 0.4f)
        {
            Vector3 opponentPosition = new Vector3(opponent.transform.position.x, 0, opponent.transform.position.z);
            Vector3 playerPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            currentDirection = (opponentPosition - playerPosition).normalized;
            isMovingBackward = false;
            Debug.Log("Decided to move towards opponent.");
        }
        else
        {
            currentDirection = Vector3.zero;
            isMovingBackward = true;
            Debug.Log("Decided to move slightly away from opponent.");
        }
    }

    private void MoveTowardsOpponent()
    {
        if (isMovingBackward)
        {
            Vector3 playerPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            Vector3 opponentPosition = new Vector3(opponent.transform.position.x, 0, opponent.transform.position.z);
            Vector3 stepBack = (playerPosition - opponentPosition).normalized * backwardStepDistance;
            player.transform.position += new Vector3(stepBack.x, 0, stepBack.z);
            isMovingBackward = false;
        }
        else
        {
            player.transform.position += new Vector3(currentDirection.x, 0, currentDirection.z) * moveSpeed * Time.deltaTime;
        }
    }

    private void TriggerPunch()
    {
        player.poseInput.bPressed = true;
        Debug.Log("Punching!");
        Invoke(nameof(ResetPunch), 0.2f);
    }

    private void ResetPunch()
    {
        player.poseInput.bPressed = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MessageHandler : MonoBehaviour
{
    public NewFighter player; // Reference to the NewFighter (player) object
    public NewFighter opponent; // Reference to the Opponent object
    public float jumpThreshold = 1.0f; // Threshold for vertical velocity to trigger jump

    private Vector3 previousHeadPosition; // To calculate velocity of the head
    private bool initialized = false; // To check if the previous position is set
    private Vector3 previousRightHandPosition; // To calculate velocity of the right hand
    private bool handInitialized = false; // To check if the previous position is set
    public float punchThreshold = 3.5f; // Threshold for hand velocity to detect a punch

    // Method to receive pose data from React
    public void ReceivePoseData(string poseDataJson)
    {
        // Parse the incoming JSON string into a list of pose landmarks
        List<PoseLandmark> poseLandmarks = PoseLandmark.FromJson(poseDataJson);

        if (poseLandmarks != null && poseLandmarks.Count > 0)
        {
            // Assuming head position is the first landmark in the list (update as needed)
            PoseLandmark headLandmark = poseLandmarks[0];
            PoseLandmark rightHandLandmark = poseLandmarks[16];

            Debug.Log($"Head position: {headLandmark.x}, {headLandmark.y}, {headLandmark.z}");

            Vector3 currentHeadPosition = new Vector3(headLandmark.x, headLandmark.y, headLandmark.z);

            if (initialized)
            {
                // Calculate velocity along the y-axis
                float headVelocityY = -(currentHeadPosition.y - previousHeadPosition.y) / Time.deltaTime;

                // Check if velocity exceeds threshold and player is on the ground
                if (headVelocityY > jumpThreshold && player.onGround)
                {
                    // Calculate jump velocity
                    Vector3 jumpVelocity = new Vector3(0f, player.verticalJumpSpeed, 0f);
                    Debug.Log("Jumping");

                    // Trigger the Jumping state
                    player.SwitchState(new Jumping(player, jumpVelocity));
                }
            }

            if (headLandmark.x < 0.33)
            {
                player.poseInput.direction = 4;
            }
            else if (headLandmark.x > 0.66)
            {
                player.poseInput.direction = 6;
            }
            else
            {
                player.poseInput.direction = 5;
            }

            // Update the previous head position for the next frame
            previousHeadPosition = currentHeadPosition;
            initialized = true;

            Vector3 currentRightHandPosition = new Vector3(rightHandLandmark.x, rightHandLandmark.y, rightHandLandmark.z);

            if (handInitialized)
            {
                // Calculate velocity along the x-axis or z-axis for punch detection
                float handVelocityX = (currentRightHandPosition.x - previousRightHandPosition.x) / Time.deltaTime;
                // Debug.Log(handVelocityX);
                // float handVelocityZ = (currentRightHandPosition.z - previousRightHandPosition.z) / Time.deltaTime;
                // Debug.Log(handVelocityZ);

                // Check if the velocity exceeds the threshold
                if (Mathf.Abs(handVelocityX) > punchThreshold)
                {   
                    // InputData currentInput = new InputData();
                    // currentInput.bPressed = true;
                    player.poseInput.bPressed = true;
                    // Debug.Log("Punching");
                    // player.SwitchState(new Attacking(player));
                }
            }

            // Update the previous hand position for the next frame
            previousRightHandPosition = currentRightHandPosition;
            handInitialized = true;
        }

        // Optionally visualize the landmarks (e.g., for debugging)
        // VisualizePose(poseLandmarks);

    }

    // Visualize the pose landmarks in Unity
    private void VisualizePose(List<PoseLandmark> landmarks)
    {
        foreach (var landmark in landmarks)
        {
            // Convert normalized landmark coordinates (0-1) to Unity world space
            Vector3 position = new Vector3(
                landmark.x * Screen.width, 
                landmark.y * Screen.height, 
                landmark.z
            );

            // Create or move a GameObject at the landmark position
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            marker.transform.position = position;
            marker.transform.localScale = Vector3.one * 0.1f; // Adjust size
        }
    }
}
// Helper class to represent a pose landmark
[System.Serializable]
public class PoseLandmark
{
    public float x;
    public float y;
    public float z;

    // Deserialize JSON into a list of PoseLandmark objects
    public static List<PoseLandmark> FromJson(string json)
    {
        return JsonUtility.FromJson<PoseLandmarksWrapper>("{\"landmarks\":" + json + "}").landmarks;
    }
}

// Wrapper class for JSON deserialization
[System.Serializable]
public class PoseLandmarksWrapper
{
    public List<PoseLandmark> landmarks;
}
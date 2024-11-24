using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageReceiver : MonoBehaviour
{
    public Renderer targetRenderer;
    public string unityImageName = "SmallGuyTextureElie";  // Name of the image in the Resources folder (without extension)

    // Start is called before the first frame update

    // Coroutine that waits for a specified amount of time before loading the texture
    private IEnumerator LoadTextureAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay time

        LoadTextureFromResources();  // Load the texture after the delay
    }

    // Method to be called by the JavaScript side with the image data
    public void SetTexture(string imageData)
    {
        // Decode the Base64 image data into a Texture2D
        byte[] imageBytes = System.Convert.FromBase64String(imageData);
        Texture2D texture = new Texture2D(1024, 1024);
        if (texture.LoadImage(imageBytes)) // Load the image from the base64 data
        {
            ApplyTexture(texture);
        }
        else
        {
            Debug.LogError("Failed to load texture from the provided image data.");
        }
    }

    // Method to load texture from Unity Resources folder
    public void LoadTextureFromResources()
    {
        Texture2D texture = Resources.Load<Texture2D>("SmallGuyTextureElie");
        if (texture != null)
        {
            ApplyTexture(texture);
        }
        else
        {
            Debug.LogError("Texture not found in Resources folder!");
        }
    }

    // Method to load texture from a file path
    public void LoadTextureFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            byte[] imageBytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(1024, 1024);
            if (texture.LoadImage(imageBytes))
            {
                ApplyTexture(texture);
            }
            else
            {
                Debug.LogError("Failed to load texture from the file.");
            }
        }
        else
        {
            Debug.LogError("File not found at path: " + filePath);
        }
    }

    // Helper method to apply the texture to the material
    private void ApplyTexture(Texture2D texture)
    {
        if (targetRenderer != null && targetRenderer.material != null)
        {   
            targetRenderer.material.mainTexture = texture;
            // targetRenderer.material.SetTexture("_MainTex", texture);
            Debug.Log("Texture successfully applied to the material.");
        }
        else
        {
            Debug.LogError("Target renderer or material is not assigned!");
        }
    }
}

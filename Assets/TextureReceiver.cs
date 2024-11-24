using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureReceiver : MonoBehaviour
{
    public Renderer targetRenderer;

    public void SetTexture(string imageData)
    {
        byte[] imageBytes = System.Convert.FromBase64String(imageData);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes); // Load the image into the texture

        if (targetRenderer != null)
        {
            targetRenderer.material.mainTexture = texture;
        }
        else
        {
            Debug.LogError("No target renderer assigned to apply the texture!");
        }
    }
}

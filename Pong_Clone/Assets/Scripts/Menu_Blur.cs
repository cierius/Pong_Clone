using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Blur : MonoBehaviour
{
    
    [SerializeField]
    private Material effect;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, effect);
    }
}

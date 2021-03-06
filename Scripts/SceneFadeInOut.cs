﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
    private bool sceneStarting = true;      // Whether or not the scene is still fading in.


    void Awake()
    {
        // Set the texture so that it is the the size of the screen and covers it.
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }


    void Update()
    {
        // If the scene is starting...
        if(sceneStarting) { 
            // ... call the StartScene function.
            StartScene();
            PlayerManager.Instance.m_NumOfDead = 0;
        }
    }


    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToWhite()
    {
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.white, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if(GetComponent<GUITexture>().color.a <= 0.0001f) {
            // ... set the colour to clear and disable the GUITexture.
            GetComponent<GUITexture>().color = Color.clear;
            GetComponent<GUITexture>().enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }


    public void EndScene(string nextScene)
    {
        // Make sure the texture is enabled.
        GetComponent<GUITexture>().enabled = true;

        // Start fading towards black.
        FadeToWhite();

        // If the screen is almost black...
        if(GetComponent<GUITexture>().color.r >= 0.999f)
            // ... reload the level.
            //Application.LoadLevel(nextScene);
            SceneManager.LoadScene(nextScene);
    }
}
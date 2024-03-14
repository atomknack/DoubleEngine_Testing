using System;
using System.Collections;
using System.Collections.Generic;
using UKnack;
using UKnack.Common;
using UnityEngine;

public class SwitchFullscreenAndResizeWindowEveryFrame: MonoBehaviour, ICommand
{
    public Vector2Int minResolution;
    public bool FullscreenToNative = true;

    private bool fullscreenButtonPressed = false;

    private string _description = nameof(SwitchFullscreenAndResizeWindowEveryFrame);
    public string Description => _description;

    private void Awake()
    {
        _description = $"{CommonStatic.GetFullPath_Recursive(gameObject)}/{nameof(SwitchFullscreenAndResizeWindowEveryFrame)}";
    }

    public void Execute()
    {
        FullScreenPressed();
    }

    public void FullScreenPressed() 
    { 
        fullscreenButtonPressed = true; 
    }

    private void Update()
    {
        var fullscreen = Screen.fullScreen;
        if (fullscreenButtonPressed)
        {
            fullscreenButtonPressed = false;

            fullscreen = !fullscreen;
            if (fullscreen == false)
                Screen.fullScreen = fullscreen;
            else
            {
                if (FullscreenToNative)
                {
                    int index = Camera.main.targetDisplay;
                    var d = Display.displays[index];
                    //Debug.LogError($"{index} {d.systemWidth} {d.renderingHeight}");
                    Screen.SetResolution(d.systemWidth, d.systemHeight, fullscreen);
                }
                else
                {
                    Screen.fullScreen = fullscreen;
                }
            }

        }

        if (minResolution.x > 0 && minResolution.y > 0 && fullscreen == false)
        {
            (int width, int height) = (Screen.width, Screen.height);
            if (minResolution.x > width || minResolution.y > height)
            {
                width = Math.Max(minResolution.x, width);
                height = Math.Max(minResolution.y, height);
                Screen.SetResolution(width, height, Screen.fullScreen);
            }

        }
    }
}

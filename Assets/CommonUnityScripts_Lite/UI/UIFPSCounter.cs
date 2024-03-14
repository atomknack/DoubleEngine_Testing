using System;
using UnityEngine;
public class UIFPSCounter : MonoBehaviour
{
    public Rect boxRect = new Rect(1, 1, 200, 40);
    public int fontSize = 20;
    public bool showFps = true;
    private GUIStyle style = new GUIStyle();

    private int frameCount;
    private float elapsedTime;
    private float frameRate;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        style.fontSize = fontSize;
        style.normal.textColor = Color.white;

    }

    private void Update()
    {
        frameCount++;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.5f)
        {
            frameRate = MathF.Round(frameCount / elapsedTime, 2);
            frameCount = 0;
            elapsedTime = 0;
        }
    }

    private void OnGUI()
    {
        if (showFps == false)
            return;
        //GUI.Box(boxRect, "");
        GUI.Label(boxRect, " " + frameRate + "fps", style);
    }
}
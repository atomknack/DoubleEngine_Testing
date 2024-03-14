using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UBuildCommand
{

public abstract class BuildSettings
{
    bool _applied = false;

    public abstract string Name { get; }
    protected abstract void ValidateSettingsPrepareAndThrowIfWrong();
    protected abstract void ApplySettings();

    protected abstract void RestoreSettings();

    public void Apply() 
    {
        if (_applied) 
            return;
        ValidateSettingsPrepareAndThrowIfWrong();
        ApplySettings();
        _applied= true;
    }

    public void Restore()
    {
        if (_applied==false) 
            return;
        RestoreSettings();
        _applied= false;
    }

    public static bool TryApplySettings(IEnumerable<BuildSettings> allSettings)
    {
        foreach (var settings in allSettings)
        {
            try
            {
                settings.Apply();
            } 
            catch(Exception e)
            {
                Debug.LogError($"Custom Build cannot Apply Settings {settings.Name}, exception: {e}");
                return false;
            }
        }
        return true;
    }

    public static void RestoreSettings(IEnumerable<BuildSettings> allSettings)
    {
        foreach(var settings in allSettings.Reverse()) 
        {
            settings.Restore();
        }
    }
}

}
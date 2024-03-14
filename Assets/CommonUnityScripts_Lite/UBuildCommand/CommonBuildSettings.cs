using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace UBuildCommand
{

    public class CommonBuildSettings : BuildSettings
    {
        NamedBuildTarget _buildTarget;
        string _name;
        public override string Name => _name;

        private string prevCommonBuildVersion;
        private bool prevCommonIsDevelopmentBuild;
        private string prevCommonAdditionalDefineSymbols;

        private string buildVersion;
        private bool isDevelopment;
        private string addDefineSymbols;

        public CommonBuildSettings(NamedBuildTarget buildTarget, string name = "CommonBuildSettings")
        {
            _buildTarget = buildTarget;
            _name = name;
        }

        protected override void ApplySettings()
        {
            prevCommonIsDevelopmentBuild = EditorUserBuildSettings.development;
            Debug.Log($"{prevCommonIsDevelopmentBuild}---{isDevelopment}");
            EditorUserBuildSettings.development = isDevelopment;

            prevCommonBuildVersion = PlayerSettings.bundleVersion;
            Debug.Log($"{prevCommonBuildVersion}---{buildVersion}");
            PlayerSettings.bundleVersion = buildVersion;

            prevCommonAdditionalDefineSymbols = PlayerSettings.GetScriptingDefineSymbols(_buildTarget);
            //Debug.Log($"{prevCommonAdditionalDefineSymbols}---{addDefineSymbols}");
            PlayerSettings.SetScriptingDefineSymbols(_buildTarget, prevCommonAdditionalDefineSymbols +';'+ addDefineSymbols);
            Debug.Log($"{prevCommonAdditionalDefineSymbols}---{PlayerSettings.GetScriptingDefineSymbols(_buildTarget)}");
        }

        protected override void RestoreSettings()
        {
            EditorUserBuildSettings.development = prevCommonIsDevelopmentBuild;
            PlayerSettings.bundleVersion = prevCommonBuildVersion;
            PlayerSettings.SetScriptingDefineSymbols(_buildTarget, prevCommonAdditionalDefineSymbols);

        }

        protected override void ValidateSettingsPrepareAndThrowIfWrong()
        {
            isDevelopment = EnvironmentVariables.GetVariableBool(EnvironmentVariables.commonIsDevelopmentBuild);
            buildVersion = EnvironmentVariables.GetVariable(EnvironmentVariables.commonBuildVersion);
            addDefineSymbols = EnvironmentVariables.GetVariable(EnvironmentVariables.commonAddDefineSymbols);
        }
    }
}
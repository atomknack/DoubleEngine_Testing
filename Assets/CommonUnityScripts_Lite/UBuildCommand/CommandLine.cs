#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UBuildCommand;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace UBuildCommand
{
    public class CommandLine
    {
        private static string[] ScenePaths => EditorBuildSettings.scenes.Select(x => x.path).ToArray();
        public static void WindowsStandard()
        {
            string outputDestination = EnvironmentVariables.GetVariable(EnvironmentVariables.outputDestination);

            var namedTarget = NamedBuildTarget.Standalone;
            var target = BuildTarget.StandaloneWindows64;

            List<BuildSettings> settings = new List<BuildSettings>();
            settings.Add(new CommonBuildSettings(namedTarget));
            BuildSettings.TryApplySettings(settings);
            try
            {
                var report = BuildPipeline.BuildPlayer(ScenePaths, outputDestination, target, BuildOptions.None);
                BuildSummary summary = report.summary;
                if (summary.result == BuildResult.Succeeded)
                {
                    Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
                }
                if (summary.result == BuildResult.Failed)
                {
                    Debug.Log("Build failed");
                }
            }
            finally
            {
                BuildSettings.RestoreSettings(settings);
            }

        }
    }
}
#endif
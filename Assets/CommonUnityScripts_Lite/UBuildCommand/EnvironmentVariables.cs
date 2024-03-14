#if UNITY_EDITOR
using System;
using UnityEngine;

namespace UBuildCommand
{
    public static class EnvironmentVariables
    {
        public static readonly string outputDestination = "outputDestination";

        public static readonly string commonBuildVersion = "commonBuildVersion";
        public static readonly string commonIsDevelopmentBuild = "commonIsDevelopmentBuild";
        public static readonly string commonAddDefineSymbols = "commonAddDefineSymbols";

        public static bool TryGetVariableLong(string name, out long variable)
        {
            if (TryGetVariable(name, out string strVariable) && long.TryParse(strVariable, out variable))
                return true;
            variable = 0;
            return false;
        }

        public static bool TryGetVariableBool(string name, out bool variable)
        {
            if (TryGetVariable(name, out string strVariable) && bool.TryParse(strVariable, out variable))
                return true;
            variable = false;
            return false;
        }

        public static long GetVariableLong(string name)
        {
            if (TryGetVariableLong(name, out long result))
                return result;
            throw new System.Exception($"{name} environment variable not defined");
        }
        public static bool GetVariableBool(string name)
        {
            if (TryGetVariableBool(name, out bool result))
                return result;
            throw new System.Exception($"{name} environment variable not defined");
        }
        public static bool TryGetVariable(string name, out string variable)
        {
            try
            {
                variable = GetVariable(name);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log($"When trying to get environment variable {name} was {e}");
            }
            variable = null;
            return false;
        }
        public static string GetVariable(string name)
        {
            string result = Environment.GetEnvironmentVariable(name);
            if (result == null)
                throw new System.Exception($"{name} environment variable not found");
            return result;
        }

    }
}
#endif
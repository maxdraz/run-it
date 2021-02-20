using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RunIt.Console
{
    public static class ConsoleCommands
    {
        public static void ClearConsole()
        {
           // if (Application.isEditor)
           // {
               // var assembly = Assembly.GetAssembly(typeof(SceneView));
              //  var type = assembly.GetType("UnityEditor.LogEntries");
             //   var method = type.GetMethod("Clear");
             //   method.Invoke(new object(), null);
           // }
        }
    }
}
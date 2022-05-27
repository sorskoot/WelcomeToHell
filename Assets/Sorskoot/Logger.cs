using System;
using UnityEngine;

namespace Sorskoot.Ioc
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
#if UNITY_EDITOR            
            Debug.Log(message);
#endif            
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Sorskoot.Ioc;
using UnityEngine;
using ILogger = UnityEngine.ILogger;
using Logger = UnityEngine.Logger;

namespace WelcomeToHell
{
    public class GameManager : MonoBehaviour
    {
        public GameManager()
        {
            SnapIoc.Container.Register<Sorskoot.Ioc.ILogger, Sorskoot.Ioc.Logger>();
            SnapIoc.Container.Register<IGameState, GameState>();
        }

        private void Start()
        {
            SnapIoc.Container.Resolve<IGameState>();
            SnapIoc.Container.Resolve<IGameState>();
        }
    }
}

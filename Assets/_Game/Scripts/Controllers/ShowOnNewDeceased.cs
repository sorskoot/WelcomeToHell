using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace WelcomeToHell.Controllers
{
    [RequireComponent(typeof(Canvas))]
    public class ShowOnNewDeceased : GameStateBehavior
    {
        private void Start()
        {
            gameState.CurrentSin.Where(c=>c.HasValue).Subscribe(OnNewDeceased).AddTo(this);
            GetComponent<Canvas>().enabled = false;
        }

        private void OnNewDeceased(Sins? _)
        {
            GetComponent<Canvas>().enabled = true;
        }
    }
}
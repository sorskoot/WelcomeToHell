using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using WelcomeToHell.Controllers;

namespace WelcomeToHell.Controllers
{
    public class ShowFailure : GameStateBehavior
    {
        // Start is called before the first frame update
        void Start()
        {
            gameState.Failures.Where(f => f is < 3 and > 0).Subscribe(OnFailure).AddTo(this);
            GetComponent<Canvas>().enabled = false;
        }

        // Update is called once per frame
        void OnFailure(int failure)
        {
            GetComponent<Canvas>().enabled = true;
            Scheduler.MainThread.Schedule(3000f, () => GetComponent<Canvas>().enabled = false);
        }
    }
}
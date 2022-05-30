using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using WelcomeToHell;
using WelcomeToHell.Controllers;

namespace WelcomeToHell.Controllers
{
    public class ShowGameOver : GameStateBehavior
    {
        [SerializeField] private TMP_Text text;
        // Start is called before the first frame update
        void Start()
        {
            gameState.Failures.Where(f => f >= 3).Subscribe(OnFailure).AddTo(this);
            GetComponent<Canvas>().enabled = false;
        }

        // Update is called once per frame
        void OnFailure(int failure)
        {
            GetComponent<Canvas>().enabled = true;
            text.text = "Too many mistakes! You are fired!!! Score:" + gameState.Score.Value;
            Scheduler.MainThread.Schedule(3000f, () => GetComponent<Canvas>().enabled = false);
        }
    } 
}
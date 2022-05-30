using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace WelcomeToHell.Controllers
{
    [RequireComponent(typeof(Canvas))]
    public class ShowOnNewDeceased : MonoBehaviour
    {
        private void Start()
        {
            MessageBroker.Default.Receive<NewDeceased>().Subscribe(OnNewDeceased).AddTo(this);
            GetComponent<Canvas>().enabled = false;
        }

        private void OnNewDeceased(NewDeceased _)
        {
            GetComponent<Canvas>().enabled = true;
        }
    }
}
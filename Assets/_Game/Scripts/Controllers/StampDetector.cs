using System;
using System.Collections;
using System.Collections.Generic;
using Sorskoot.Ioc;
using UnityEngine;
using UnityEngine.Events;

namespace WelcomeToHell.Controllers
{
    public class StampDetector : MonoBehaviour
    {
        private SnapDependency<IGameState> gameState;
        [SerializeField] private UnityEvent stampHit;
        
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Stamp"))
            {
                gameState.Value.Stamp(collision.gameObject.name);
                stampHit.Invoke();
            }
        }
    }
}
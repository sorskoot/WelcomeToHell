using System;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace WelcomeToHell.Controllers
{
    [Serializable]
    public class SinDescriptor
    {
        [SerializeField]public Sins sin;
        [SerializeField][TextArea(3, 10)]public string message;
    }

    public class MessageGenerator : GameStateBehavior
    {
        [SerializeField] private Sprite[] photos;
        [SerializeField] private SinDescriptor[] sins;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image image;

        private void Start()
        {
            gameState.CurrentSin.Where(c=>c.HasValue).Subscribe(OnNewDeceased).AddTo(this);
        }

        private void OnNewDeceased(Sins? sin)
        {
            Sprite randomSprite = photos[Random.Range(0, photos.Length)];
            image.sprite = randomSprite;
            var possibleSins = sins.ToList().Where(s => s.sin == sin).ToArray();
            SinDescriptor sinDescriptor = possibleSins[Random.Range(0, possibleSins.Length)];
            text.text = sinDescriptor.message;
        }
    }
}

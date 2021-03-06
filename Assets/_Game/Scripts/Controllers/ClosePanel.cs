using UnityEngine;

namespace WelcomeToHell.Controllers
{
    public class ClosePanel : MonoBehaviour
    {
        [SerializeField] private GameObject Panel;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Hand"))
            {
                Panel.SetActive(false);
            }

        }

        public void HandleClick()
        {
            enabled = false;
            transform.position.Set(0, 0, 100);
        }
    }
}
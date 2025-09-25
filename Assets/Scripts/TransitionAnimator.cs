using UnityEngine;

namespace Assets.Scripts
{
    public class TransitionAnimator : MonoBehaviour
    {
        public Animator fadeAnimator;

        private void Start()
        {
            fadeAnimator.enabled = false;
        }

        public void StartFadeOut()
        {
            fadeAnimator.enabled = true;
            fadeAnimator.SetTrigger("FadeOutTrigger");
        }
    }
}

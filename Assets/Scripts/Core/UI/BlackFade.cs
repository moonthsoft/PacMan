using UnityEngine;

namespace Moonthsoft.Core.UI
{
    public class BlackFade : MonoBehaviour
    {
        private const float TIME_FADE = 0.2f;

        [SerializeField] private Animator _animator;

        private WaitForSecondsRealtime _waitFade;

        public WaitForSecondsRealtime Active(bool _in)
        {
            _animator.SetBool("active", _in);

            return _waitFade;
        }

        private void Awake()
        {
            _waitFade = new WaitForSecondsRealtime(TIME_FADE);
        }
    }
}

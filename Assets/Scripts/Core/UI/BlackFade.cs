using UnityEngine;

namespace Moonthsoft.Core.UI
{
    /// <summary>
    /// Class responsible for making fades to black.
    /// It is used, for example, so that the change between scenes is not so abrupt.
    /// </summary>
    public class BlackFade : MonoBehaviour
    {
        private const float TIME_FADE = 0.2f;

        private WaitForSecondsRealtime _waitFade;

        [SerializeField] private Animator _animator;


        private void Awake()
        {
            _waitFade = new WaitForSecondsRealtime(TIME_FADE);
        }

        public WaitForSecondsRealtime Active(bool _in)
        {
            _animator.SetBool("active", _in);

            return _waitFade;
        }
    }
}

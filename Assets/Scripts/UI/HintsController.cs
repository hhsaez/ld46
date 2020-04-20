using UnityEngine;
using UnityEngine.UI;
using ld46.Data;

namespace ld46.UI {

    public class HintsController : MonoBehaviour {

        [ SerializeField ] protected Hint[] m_hints;
        [ SerializeField ] protected Image m_image;
        [ SerializeField ] protected Text m_text;
        [ SerializeField ] protected Image []m_dots;

        private int m_currentHint = 0;

        private void Start() 
        {
            UpdateHintUI();
        }

        private void UpdateHintUI()
        {
            m_dots.ForEach((dot, idx) => dot.color = (idx == m_currentHint ? Color.white : Color.gray));
            Hint hint = m_hints[ m_currentHint ];
            m_image.sprite = hint.image;
            m_text.text = hint.text;
        }

        public void NextHint()
        {
            m_currentHint = ( m_currentHint + 1 ) % m_hints.Length;
            UpdateHintUI();
        }

        public void PrevHint()
        {
            m_currentHint = ( m_currentHint + m_hints.Length - 1 ) % m_hints.Length;
            UpdateHintUI();
        }

    }

}

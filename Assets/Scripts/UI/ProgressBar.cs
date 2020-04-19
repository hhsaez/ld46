using UnityEngine;
using UnityEngine.UI;
using ld46.Data;

namespace ld46.UI {

    public class ProgressBar : MonoBehaviour {

        [ SerializeField ] protected Slider m_slider;
        [ SerializeField ] protected FloatValue m_observedValue;
        [ SerializeField ] protected FloatValue m_maxValue;

        public float Progress 
        {
            get 
            {
                return m_slider.value;
            }
            set
            {
                m_slider.value = value;
            }
        }

        private void Start() 
        {
            this.Progress = 1.0f;
        }

        private void OnEnable() 
        {
            if ( m_observedValue != null ) {
                m_observedValue.OnValueChanged += this.OnValueChange;
            }
            UpdateProgress();
        }

        private void OnDisable() 
        {
            if ( m_observedValue != null ) {
                m_observedValue.OnValueChanged -= this.OnValueChange;
            }
        }

        private void OnValueChange( FloatValue value )
        {
            UpdateProgress();
        }

        private void UpdateProgress() 
        {
            if ( m_observedValue == null || m_maxValue.Value == 0 ) {
                this.Progress = 0;
                return;
            }

            this.Progress = ( float )( m_observedValue.Value / m_maxValue.Value );
        }

    }

}


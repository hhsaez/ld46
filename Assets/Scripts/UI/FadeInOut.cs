using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ld46.Data;
using ld46.Components;

namespace ld46.UI {

    [
        RequireComponent( typeof( Image ) )
    ]
    public class FadeInOut : MonoBehaviour {

        [ SerializeField ] protected IntValue m_currentTimelinePhase;

        private Image m_image;

        private void Awake() 
        {
            m_image = GetComponent< Image >();
        }

        private void OnEnable() 
        {
            if ( m_currentTimelinePhase != null ) m_currentTimelinePhase.OnValueChanged += OnTimelinePhaseChanged;
        }

        private void OnDisable() 
        {
            if ( m_currentTimelinePhase != null ) m_currentTimelinePhase.OnValueChanged -= OnTimelinePhaseChanged;
        }

        private void OnTimelinePhaseChanged( IntValue phase )
        {
            switch ( ( Timeline.Phases ) phase.Value ) {
                case Timeline.Phases.TITLE:
                    FadeIn();
                    break;

                case Timeline.Phases.COMBAT_BEGIN:
                    FadeIn( false );
                    break;

                default:
                    FadeOut( false );
                    break;
            }
        }

        public void FadeIn( bool animated = true ) 
        {
            if ( !animated ) {
                m_image.enabled = false;
                return;
            }

            StartCoroutine( "FadeInImpl" );
        }

        private IEnumerator FadeInImpl()
        {
            m_image.enabled = true;
            float dt = 0.01f;
            for ( float i = 0; i <= 1.0f; i += dt ) {
                m_image.color = new Color( 0.0f, 0.0f, 0.0f, 1.0f - i );
                yield return new WaitForSecondsRealtime( dt );
            }
            m_image.enabled = false;
        }

        public void FadeOut( bool animated = true )
        {
            if ( !animated ) {
                m_image.enabled = true;
                m_image.color = Color.black;
                return;
            }

            StartCoroutine( "FadeOutImpl" );
        }

        private IEnumerator FadeOutImpl()
        {
            m_image.enabled = true;
            float dt = 0.01f;
            for ( float i = 0; i <= 1.0f; i += dt ) {
                m_image.color = new Color(0.0f, 0.0f, 0.0f, i);
                yield return new WaitForSecondsRealtime( dt );
            }
            m_image.color = Color.black;
        }

    }

}


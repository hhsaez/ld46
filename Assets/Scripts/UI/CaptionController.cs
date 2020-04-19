using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ld46.Components;
using ld46.Data;

namespace ld46.UI {

    public class CaptionController : MonoBehaviour {

        [ SerializeField ] protected IntValue m_currentTimelinePhase;
        [ SerializeField ] protected Text m_captions;

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
                case Timeline.Phases.INTRO_1:
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "I was just a kid when I first saw HIM...";
                    break;

                case Timeline.Phases.INTRO_2:
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "That was such a majestic sight";
                    break;

                case Timeline.Phases.INTRO_3:
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "When Evil came down from Heaven to exterminate us ...";
                    break;

                case Timeline.Phases.INTRO_4:
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "He kept our hopes alive.";
                    break;

                default:
                    m_captions.gameObject.SetActive( false );
                    break;
            }
        }

    }

}


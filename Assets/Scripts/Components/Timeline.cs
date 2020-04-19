using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ld46.Data;

namespace ld46.Components {

    public class Timeline : MonoBehaviour
    {
        public enum Phases : int {
            START = 0,
            INTRO_BEGIN,
            INTRO_1,
            INTRO_2,
            INTRO_3,
            INTRO_4,
            INTRO_END,
            TITLE,
            COMBAT_BEGIN,
            COMBAT_PHASE_1,
            COMBAT_PHASE_2,
            COMBAT_PHASE_3,
            COMBAT_END,
            OUTRO,
            END,
        }

        [ SerializeField ] protected IntValue m_currentTimelinePhase;

        [ SerializeField ] protected GameObject m_gameUI;
        [ SerializeField ] protected GameObject m_introUI;
        [ SerializeField ] protected GameObject m_titleUI;        

        private void Start() 
        {
            m_currentTimelinePhase.Value = ( int ) Phases.START;
            NextPhase();
        }

        public void NextPhase()
        {
            StartCoroutine( "NextPhaseImpl" );
        }

        private IEnumerator NextPhaseImpl() 
        {
            m_currentTimelinePhase.Value++;

            Debug.Log("Next phase: " + ( Phases ) m_currentTimelinePhase.Value );

            m_gameUI.gameObject.SetActive( false );
            m_introUI.gameObject.SetActive( false );
            m_titleUI.gameObject.SetActive( false );

            switch ( ( Phases ) m_currentTimelinePhase.Value ) {
                case Phases.INTRO_BEGIN:
                    m_introUI.gameObject.SetActive( true );
                    yield return new WaitForSecondsRealtime( 1.0f );
                    NextPhase();
                    break;

                case Phases.INTRO_END:
                    yield return new WaitForSecondsRealtime( 1.0f );
                    NextPhase();
                    break;

                case Phases.INTRO_1:
                case Phases.INTRO_2:
                case Phases.INTRO_3:
                case Phases.INTRO_4:
                    m_introUI.gameObject.SetActive( true );
                    yield return new WaitForSecondsRealtime( 3.0f );
                    NextPhase();
                    break;

                case Phases.TITLE:
                    m_titleUI.gameObject.SetActive( true );
                    while ( !Input.GetButton( "Fire1" ) ) {
                        yield return new WaitForSecondsRealtime( 0.1f );
                    }
                    yield return new WaitForSecondsRealtime( 0.1f );
                    NextPhase();
                    break;

                case Phases.COMBAT_BEGIN:
                    m_gameUI.gameObject.SetActive( true );
                    break;

                default:
                    break;
            }
        }
    }

}


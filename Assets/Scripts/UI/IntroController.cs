using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ld46.UI {

    public class IntroController : MonoBehaviour {

        private enum Phases : int {
            INTRO_BEGIN,
            INTRO_1,
            INTRO_2,
            INTRO_3,
            INTRO_4,
            INTRO_END,
        }

        [ SerializeField ] protected FadeInOut m_fadeInOut;
        [ SerializeField ] protected Text m_captions;

        private int m_currentPhase = ( int ) Phases.INTRO_BEGIN;

        private void Start() 
        {
            StartCoroutine( "ResolveCurrentPhase" );
        }

        private void NextPhase() 
        {
            ++m_currentPhase;
            StartCoroutine( "ResolveCurrentPhase" );
        }

        public void GoToTitleScreen()
        {
            UnitySceneManager.LoadScene( "Title" );
        }

        private IEnumerator ResolveCurrentPhase()
        {
            Debug.Log( "Resolving Phase " + ( Phases ) m_currentPhase );

            switch ( ( Phases ) m_currentPhase ) {
                case Phases.INTRO_BEGIN:
                    m_fadeInOut.FadeOut( false );
                    m_captions.gameObject.SetActive( false );
                    yield return new WaitForSecondsRealtime( 1.0f );
                    NextPhase();
                    break;

                case Phases.INTRO_1:
                    // m_fadeInOut.FadeOut();
                    yield return new WaitForSecondsRealtime( 1.0f );
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "I was just a kid when I first saw HIM...";
                    yield return new WaitForSecondsRealtime( 3.0f );
                    NextPhase();
                    break;

                case Phases.INTRO_2:
                    // m_fadeInOut.FadeOut();
                    yield return new WaitForSecondsRealtime( 1.0f );
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "That was such a MAJESTIC sight.";
                    yield return new WaitForSecondsRealtime( 3.0f );
                    NextPhase();
                    break;

                case Phases.INTRO_3:
                    // m_fadeInOut.FadeOut();
                    yield return new WaitForSecondsRealtime( 1.0f );
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "When EVIL came down from HEAVEN\nto exterminate us...";
                    yield return new WaitForSecondsRealtime( 3.0f );
                    NextPhase();
                    break;

                case Phases.INTRO_4:
                    // m_fadeInOut.FadeOut();
                    yield return new WaitForSecondsRealtime( 1.0f );
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "He kept our hope ALIVE.";
                    yield return new WaitForSecondsRealtime( 3.0f );
                    NextPhase();
                    break;

                case Phases.INTRO_END:
                    // m_fadeInOut.FadeOut();
                    m_captions.gameObject.SetActive( false );
                    yield return new WaitForSecondsRealtime( 1.0f );
                    GoToTitleScreen();
                    break;

                default:
                    break;
            }
        }

    }
}


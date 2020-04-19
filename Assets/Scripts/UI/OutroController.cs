using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ld46.UI {

    public class OutroController : MonoBehaviour {

        private enum Phases : int {
            OUTRO_BEGIN,
            OUTRO_1,
            OUTRO_2,
            OUTRO_3,
            OUTRO_END,
        }

        [ SerializeField ] protected FadeInOut m_fadeInOut;
        [ SerializeField ] protected Text m_captions;

        private int m_currentPhase = ( int ) Phases.OUTRO_BEGIN;

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
                case Phases.OUTRO_BEGIN:
                    m_fadeInOut.FadeOut( false );
                    m_captions.gameObject.SetActive( false );
                    yield return new WaitForSecondsRealtime( 1.0f );
                    NextPhase();
                    break;

                case Phases.OUTRO_1:
                    yield return new WaitForSecondsRealtime( 1.0f );
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "That was our first victory.";
                    yield return new WaitForSecondsRealtime( 3.0f );
                    NextPhase();
                    break;

                case Phases.OUTRO_2:
                    yield return new WaitForSecondsRealtime( 1.0f );
                    m_captions.gameObject.SetActive( false );
                    yield return new WaitForSecondsRealtime( 3.0f );
                    NextPhase();
                    break;

                case Phases.OUTRO_3:
                    yield return new WaitForSecondsRealtime( 1.0f );
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "Thanks for playing.";
                    yield return new WaitForSecondsRealtime( 3.0f );
                    NextPhase();
                    break;

                case Phases.OUTRO_END:
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


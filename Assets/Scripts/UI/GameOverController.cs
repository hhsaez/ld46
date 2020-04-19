using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ld46.UI {

    public class GameOverController : MonoBehaviour {

        private enum Phases : int {
            GAME_OVER_BEGIN,
            GAME_OVER_1,
            GAME_OVER_END,
        }

        [ SerializeField ] protected FadeInOut m_fadeInOut;
        [ SerializeField ] protected Text m_captions;

        private int m_currentPhase = ( int ) Phases.GAME_OVER_BEGIN;

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
                case Phases.GAME_OVER_BEGIN:
                    m_fadeInOut.FadeOut( false );
                    m_captions.gameObject.SetActive( false );
                    yield return new WaitForSecondsRealtime( 1.0f );
                    NextPhase();
                    break;

                case Phases.GAME_OVER_1:
                    yield return new WaitForSecondsRealtime( 1.0f );
                    m_captions.gameObject.SetActive( true );
                    m_captions.text = "That's not how it happened...";
                    yield return new WaitForSecondsRealtime( 3.0f );
                    NextPhase();
                    break;

                case Phases.GAME_OVER_END:
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


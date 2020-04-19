using System.Collections;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ld46.UI {

    public class TitleScreenController : MonoBehaviour {

        [ SerializeField ] protected FadeInOut m_fadeInOut;

        private void Start() 
        {
            m_fadeInOut.FadeIn();
        }

        private void Update()
        {
            if ( Input.GetButton( "Fire1" ) ) {
                this.enabled = false;
                StartCoroutine( "StartGame" );
            }
        }

        private IEnumerator StartGame() 
        {
            m_fadeInOut.FadeOut();
            yield return new WaitForSecondsRealtime( 2.0f );
            UnitySceneManager.LoadScene( "Game" );
        }
    }
}


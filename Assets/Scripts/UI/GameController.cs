using System.Collections;
using UnityEngine;
using ld46.Data;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ld46.UI {

    public class GameController : MonoBehaviour {

        [ SerializeField ] protected FloatValue m_playerHealth;
        [ SerializeField ] protected FloatValue m_enemyHealth;
        [ SerializeField ] protected FadeInOut m_fadeInOut;
        [ SerializeField ] protected GameObject m_pauseUI;

        private void Start()
        {
            m_fadeInOut.FadeIn();
            PauseGame();
        }

        private void Update()
        {
            if ( Input.GetKeyDown( KeyCode.Escape ) ) {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            m_pauseUI.gameObject.SetActive( true );
        }

        public void ResumeGame()
        {
            m_pauseUI.gameObject.SetActive( false );
            Time.timeScale = 1;
        }

        private void OnEnable() 
        {
            if ( m_playerHealth != null ) {
                m_playerHealth.OnValueChanged += this.OnPlayerHealthChanged;
            }
            if ( m_enemyHealth != null ) {
                m_enemyHealth.OnValueChanged += this.OnEnemyHealthChanged;
            }
        }

        private void OnDisable() 
        {
            if ( m_playerHealth != null ) {
                m_playerHealth.OnValueChanged -= this.OnPlayerHealthChanged;
            }
            if ( m_enemyHealth != null ) {
                m_enemyHealth.OnValueChanged -= this.OnEnemyHealthChanged;
            }
        }

        private void OnPlayerHealthChanged( FloatValue _ )
        {
            if ( m_playerHealth.Value <= 0 ) {
                StartCoroutine( "LoadScene", "Game Over" );
            }
        }

        private void OnEnemyHealthChanged( FloatValue _ )
        {
            if ( m_enemyHealth.Value <= 0 ) {
                StartCoroutine( "LoadScene", "Outro" );
            }
        }

        private IEnumerator LoadScene( string sceneName )
        {
            yield return new WaitForSecondsRealtime( 1.0f );
            m_fadeInOut.FadeOut();
            yield return new WaitForSecondsRealtime( 3.0f );
            UnitySceneManager.LoadScene( sceneName );
        }

    }

}


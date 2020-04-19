using UnityEngine;
using ld46.Data;

namespace ld46.Components {

    [
        RequireComponent(
            typeof( Animator )
        )
    ]
    public class CameraController : MonoBehaviour {

        [ SerializeField ] protected FloatValue m_playerHealth;
        [ SerializeField ] protected FloatValue m_enemyHealth;
        [ SerializeField ] protected FloatValue m_enemyMaxHealth;

        private Animator m_animator;

        private void Start() 
        {
            m_animator = GetComponent< Animator >();
            m_animator.SetInteger( "phase", 1 );
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
            m_animator.SetTrigger( "shake" );
        }

        private void OnEnemyHealthChanged( FloatValue _ )
        {
            m_animator.SetTrigger( "shake" );
            float phaseProgress = 1.0f - m_enemyHealth.Value / m_enemyMaxHealth.Value;
            if ( phaseProgress >= 0.6f ) {
                m_animator.SetInteger( "phase", 3 );
            }
            else if ( phaseProgress >= 0.3f ) {
                m_animator.SetInteger( "phase", 2 );
            }
            else {
                m_animator.SetInteger( "phase", 1 );
            }
        }

    }

}


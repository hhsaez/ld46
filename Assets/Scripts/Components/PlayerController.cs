using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ld46.Data;

namespace ld46.Components {

    [ 
        RequireComponent( typeof( Actor ) ),
        RequireComponent( typeof( Animator ) ),
    ]
    public class PlayerController : MonoBehaviour
    {
        [ SerializeField ] protected FloatValue m_enemyHealth;
        [ SerializeField ] protected IntValue m_currentTimelinePhase;

        private Animator m_animator;

        void Start()
        {
            m_animator = GetComponent< Animator >();    
        }

        void Update() 
        {
            if ( Time.timeScale == 0.0f ) {
                return;
            }

            if ( m_animator.GetCurrentAnimatorStateInfo( 0 ).IsName( "Idle" ) ) {
                if ( Input.GetButton( "Fire1" ) || Input.GetAxis( "Vertical" ) > 0 ) {
                    m_animator.SetTrigger( "isAttacking" );
                    return;
                }
            }

            m_animator.SetBool( "isDodgingLeft", Input.GetAxis( "Horizontal" ) < 0 );
            m_animator.SetBool( "isDodgingRight", Input.GetAxis( "Horizontal" ) > 0 );
            m_animator.SetBool( "isBlocking", Input.GetButton( "Fire2" ) || Input.GetAxis( "Vertical" ) < 0 );
        }

    }

}


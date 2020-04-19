using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ld46.Components {

    public class PlayerController : BaseComponent
    {
        [ SerializeField ] protected Animator m_animator;

        void Start()
        {
            m_animator = GetComponent< Animator >();
        }

        void Update() 
        {
            if ( m_animator.GetCurrentAnimatorStateInfo( 0 ).IsName( "Idle" ) ) {
                if ( Input.GetButton( "Fire1" ) ) {
                    m_animator.SetTrigger( "isAttacking" );
                    return;
                }
            }

            m_animator.SetBool( "isDodgingLeft", Input.GetAxis( "Horizontal" ) < 0 );
            m_animator.SetBool( "isDodgingRight", Input.GetAxis( "Horizontal" ) > 0 );
            m_animator.SetBool( "isBlocking", Input.GetButton( "Fire2" ) );
       }

    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ld46.Components {

    public class EnemyController : BaseComponent
    {
        private Animator m_animator;

        void Start()
        {
            m_animator = GetComponent< Animator >();

            StartCoroutine( DoSomething() );
        }

        private IEnumerator DoSomething() 
        {
            int action = Random.Range( 0, 4 );
            switch ( action ) {
                case 0:
                    m_animator.SetTrigger( "isAttackingLeft" );
                    break;
                case 1:
                    m_animator.SetTrigger( "isAttackingRight" );
                    break;
                case 2:
                    m_animator.SetTrigger( "isAttackingCenter" );
                    break;
                case 3:
                    m_animator.SetBool( "isBlocking", true );
                    yield return new WaitForSecondsRealtime( 1.0f );
                    m_animator.SetBool( "isBlocking", false );
                    break;
                default:
                    m_animator.SetTrigger( "isIdle" );
                    break;
            }

            yield return new WaitForSecondsRealtime( 3.0f );
            StartCoroutine( DoSomething() );
        }
    }

}


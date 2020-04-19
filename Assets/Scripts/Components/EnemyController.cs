using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ld46.Components {

    public class EnemyController : BaseComponent
    {
        private Animator m_animator;

        private List<string> m_attacks = new List< string >();
        private List<string> m_attackQueue = new List< string >();

        void Start()
        {
            m_animator = GetComponent< Animator >();

            m_attacks = new List<string>() {
                "isAttackingLeft",
                "isAttackingRight",
                "isAttackingCenter",
            };

            StartCoroutine( DoSomething() );
        }

        private IEnumerator DoSomething() 
        {
            if ( m_attackQueue.Count == 0 ) {
                m_attackQueue = new List< string >( m_attacks.Randomize() );
            }
            string nextAttack = m_attackQueue[ 0 ];
            m_attackQueue.RemoveAt( 0 );
            m_animator.SetTrigger( nextAttack );
            yield return new WaitForSecondsRealtime( 3.0f );
            StartCoroutine( DoSomething() );
        }
    }

}


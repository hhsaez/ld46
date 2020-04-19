using UnityEngine;

namespace ld46.Managers {

    [
        CreateAssetMenu(
            fileName = "Combat Manager", 
            menuName = "ld46/Managers/Combat Manager"
        )
    ]
    public class CombatManager : ScriptableObject {

        private void Start() 
        {
            // TODO    
        }

        public void PlayerAttack( float damage )
        {
            Debug.Log("Player Attack" + damage);
        }

    }

}


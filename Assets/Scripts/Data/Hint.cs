using UnityEngine;

namespace ld46.Data {

    [
        CreateAssetMenu(
            fileName = "Attack Info", 
            menuName = "ld46/Data/Hint"
        )
    ]
    public class Hint : ScriptableObject {

        public string text;
        public Sprite image;

    }

}


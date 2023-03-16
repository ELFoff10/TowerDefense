using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int GoldCost;

        public TurretProperties TurretProperties;

        public Sprite GUISprite;

        public Sprite Sprite;

    }
}


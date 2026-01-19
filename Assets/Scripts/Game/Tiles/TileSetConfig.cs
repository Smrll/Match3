using UnityEngine;
using System.Collections.Generic;

namespace Game.Tiles
{
    [CreateAssetMenu(fileName = "TileSetConfig", menuName = "Configs/TileSetConfig")]
    public class TileSetConfig : ScriptableObject
    {
        [SerializeField] private List<TileConfig> _set = new List<TileConfig>();
        public List<TileConfig> Set => _set;
    }
}
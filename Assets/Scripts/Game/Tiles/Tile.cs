using UnityEngine;

namespace Game.Tiles

{
    [RequireComponent(typeof(SpriteRenderer))]
    
    public class Tile : MonoBehaviour
    {
        public TileConfig TileConfig { get; private set;}//делаем прайвет чтобы извне не менять
        public bool IsInteractable {get; private set;}
        public bool IsMatched {get; private set;}

        public void SetTileConfig(TileConfig tileConfig) //при монобехейворе мы не можем пользоваться конструктором - делаем эквивалент
        {
            TileConfig = tileConfig;
            IsInteractable = tileConfig.IsInteractable;
            IsMatched = false;
            GetComponent<SpriteRenderer>().sprite = tileConfig.Sprite;

        } 

        public bool SetMatch(bool value) => IsMatched = value;
    }
}
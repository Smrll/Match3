using UnityEngine;

namespace Game.Utils
{
    public class SetupCamera
    {
        private bool _isVertical; //для горизонтального и вертикального развертывания игры

        public void SetCamera(int width, int height, bool isVertical) //настройка камеры
        {
            _isVertical = isVertical;
            var xPos = width / 2f - 0.5f; //width / 2f - половина ширины; - 0.5f корректировка чтобы все выгляжело ровнее
            var yPos = height / 2f + 0.5f; // + - сетка должна прижаться к низу, так как всерху будет интерфейс
            Camera.main.gameObject.transform.position = new Vector3(xPos, yPos, -10f);
            Camera.main.orthographicSize = GetOrthoSize(width, height);
        }
        private float GetOrthoSize(int width, int height)
        {
            return _isVertical
                ? (width + 2f) * Screen.height / Screen.width * 0.5f//2f - офсет от бортов/ если вертик
                : (height + 2f) * Screen.height / Screen.width;//если гориз
        }
    }
}
using UnityEngine;

namespace Kitsuma.UI
{
    public class FollowWithinScreen : PointerBehaviour
    {
        [SerializeField] protected Camera uiCamera;
        [SerializeField] protected float buffer = 100f;

        private void Update()
        {
            if (!IsTargetOffScreen()) return;
            Vector3 targetScreenPoint = Cam.WorldToScreenPoint(target.position);
            Vector3 pos = targetScreenPoint;
            if (pos.x <= buffer) pos.x = buffer;
            if (pos.x >= Screen.width - buffer) pos.x = Screen.width - buffer;
            if (pos.y <= buffer) pos.y = buffer;
            if (pos.y >= Screen.height - buffer) pos.y = Screen.height - buffer;
            Vector3 worldPos = uiCamera.ScreenToWorldPoint(pos);
            T.position = worldPos;
            Vector3 localPosition = T.localPosition;
            localPosition = new Vector3(localPosition.x, localPosition.y, 0f);
            T.localPosition = localPosition;
        }

        private bool IsTargetOffScreen()
        {
            Vector3 pos = Cam.WorldToScreenPoint(target.position);
            return pos.x <= buffer ||
                   pos.x >= Screen.width - buffer ||
                   pos.y <= buffer ||
                   pos.y >= Screen.height - buffer;
        }
    }
}
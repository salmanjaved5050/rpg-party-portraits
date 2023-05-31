using UnityEngine;

namespace RpgPortraits.Utility
{
    public static class GameUtils 
    {
        /// <summary>
        /// Checks if two rect transforms overlap with each other.
        /// </summary>
        /// <param name="rectTransform1"></param>
        /// <param name="rectTransform2"></param>
        /// <returns></returns>
        public static bool Overlaps(RectTransform rectTransform1, RectTransform rectTransform2)
        {
            bool overlaps = false;
            
            Vector3[] corners = new Vector3[4];
            rectTransform1.GetWorldCorners(corners);
            Rect rect1 = new Rect(corners[0].x,corners[0].y,corners[2].x-corners[0].x,corners[2].y-corners[0].y);

            rectTransform2.GetWorldCorners(corners);
            Rect rect2 = new Rect(corners[0].x, corners[0].y,corners[2].x-corners[0].x,corners[2].y-corners[0].y);
            
            if (rect1.Overlaps(rect2))
            {
                overlaps = true;
            }

            return overlaps;
        }
    }
}

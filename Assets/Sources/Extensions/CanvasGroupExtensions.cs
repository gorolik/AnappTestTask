using UnityEngine;

namespace Sources.Extensions
{
    public static class CanvasGroupExtensions
    {
        public static void HideGroup(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        
        public static void ShowGroup(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
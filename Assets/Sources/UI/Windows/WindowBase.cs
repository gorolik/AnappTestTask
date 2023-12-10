using UnityEngine;

namespace Sources.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        private void Start()
        {
            Init();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        protected void Close() => 
            Destroy(gameObject);

        protected virtual void Init() {}
        protected virtual void SubscribeUpdates() {}
        protected virtual void Cleanup() {}
    }
}
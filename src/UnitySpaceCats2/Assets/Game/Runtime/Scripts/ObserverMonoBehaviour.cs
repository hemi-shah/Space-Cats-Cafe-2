using Game399.Shared.Diagnostics;
using UnityEngine;

namespace Game.Runtime
{
    public abstract class ObserverMonoBehaviour : MonoBehaviour
    {
        protected static IGameLogger logger => ServiceResolver.Resolve<IGameLogger>();
        
        private bool _didCallStart;
        private bool _didSubscribe;

        protected virtual void Awake()
        {
            _didCallStart = false;
            _didSubscribe = false;
        }

        protected virtual void Start()
        {
            _didCallStart = true;
            TrySubscribe();
        }

        protected virtual void OnEnable()
        {
            TrySubscribe();
        }

        protected virtual void OnDisable()
        {
            TryUnsubscribe();
        }

        private void TrySubscribe()
        {
            if (!_didSubscribe && _didCallStart)
            {
                _didSubscribe = true;
                logger.Log(GetType().Name + "." + nameof(TrySubscribe));
                Subscribe();
            }
        }

        private void TryUnsubscribe()
        {
            if (_didSubscribe)
            {
                _didSubscribe = false;
                logger.Log(GetType().Name + "." + nameof(TryUnsubscribe));
                Unsubscribe();
            }
        }

        protected abstract void Subscribe();

        protected abstract void Unsubscribe();
    }
}
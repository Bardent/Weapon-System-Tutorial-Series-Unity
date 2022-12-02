using UnityEngine;

namespace Bardent.Core.CoreComponents
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected global::Core core;

        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<global::Core>();

            if(core == null) { Debug.LogError("There is no Core on the parent"); }
            core.AddComponent(this);
        }

        public virtual void LogicUpdate() { }

    }
}

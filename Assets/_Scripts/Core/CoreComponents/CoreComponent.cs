using UnityEngine;

namespace Bardent.CoreSystem.CoreComponents
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected global::Bardent.CoreSystem.Core core;

        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<global::Bardent.CoreSystem.Core>();

            if(core == null) { Debug.LogError("There is no Core on the parent"); }
            core.AddComponent(this);
        }

        public virtual void LogicUpdate() { }

    }
}

using UnityEditor;
using UnityEngine;

namespace Bardent.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("This is a button"))
            {
                Debug.Log("TEST!");
            }
            
        }
    }
}
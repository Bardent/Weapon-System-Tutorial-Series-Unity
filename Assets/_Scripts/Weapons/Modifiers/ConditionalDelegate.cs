using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons.Modifiers
{
    public delegate bool ConditionalDelegate(Transform source, out DirectionalInformation directionalInformation);
}
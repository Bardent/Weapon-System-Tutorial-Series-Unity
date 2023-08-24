using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons.Modifiers.BlockModifiers
{
    public delegate bool BlockConditionDelegate(Transform source, out DirectionalInformation directionalInformation);
}
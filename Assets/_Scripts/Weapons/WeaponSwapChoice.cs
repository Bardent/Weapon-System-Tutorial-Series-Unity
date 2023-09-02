using System;

namespace Bardent.Weapons
{
    public class WeaponSwapChoiceRequest
    {
        public WeaponSwapChoice[] Choices { get; }
        public WeaponDataSO NewWeaponData { get; }
        public Action<WeaponSwapChoice> Callback;

        public WeaponSwapChoiceRequest(
            Action<WeaponSwapChoice> callback,
            WeaponSwapChoice[] choices,
            WeaponDataSO newWeaponData
        )
        {
            Callback = callback;
            Choices = choices;
            NewWeaponData = newWeaponData;
        }
    }

    public class WeaponSwapChoice
    {
        public WeaponDataSO WeaponData { get; }
        public int Index { get; }

        public WeaponSwapChoice(WeaponDataSO weaponData, int index)
        {
            WeaponData = weaponData;
            Index = index;
        }
    }
}
namespace Bardent.ProjectileSystem.DataPackages
{
    /*
    * Projectile component data is not always set in stone on the prefab, and instead need to come from the weapon that is spawning the projectile
    * For example, many bows can use the same arrow prefab but they all do different amounts of damage. We therefore need a way for the weapon
    * to send that damage amount to the projectile when it spawns it.
    *
    * This class acts as the bases class for this information. Using a function of the base Projectile class, the weapon will be able to send specific pieces of
    * data through to all the projectile components. These components can then take the pieces of data that apply to them.
    */
    public abstract class ProjectileDataPackage
    {
        
    }
}
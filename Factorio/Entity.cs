using Terraria.ModLoader;

namespace FactorioMod.Factorio
{
    public class Entity : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            return false;
        }

    }
}

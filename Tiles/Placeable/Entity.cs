using Terraria.ModLoader;

namespace FactorioMod.Tiles.Placeable
{
    public abstract class Entity : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            base.Autoload(ref name, ref texture);
            return false;
        }
    }
}

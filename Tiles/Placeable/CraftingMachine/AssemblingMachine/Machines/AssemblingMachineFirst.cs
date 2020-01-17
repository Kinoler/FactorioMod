using Terraria;
using static Terraria.ModLoader.ModContent;

namespace FactorioMod.Tiles.Placeable.CraftingMachine.AssemblingMachine.Machines
{
    public class AssemblingMachineFirst : AssemblingMachine
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            BaseCraftingSpeed = 2;
            IngridientCountLimit = 1;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            base.Autoload(ref name, ref texture);
            return true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 64, 32, ItemType<Items.Placeable.TestMachine>());
            base.KillMultiTile(i, j, frameX, frameY);
        }
    }
}

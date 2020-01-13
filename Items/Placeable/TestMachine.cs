using FactorioMod.Tiles.Placeable.CraftingMachine.AssemblingMachine.Machines;
using Terraria.ModLoader;

namespace FactorioMod.Items.Placeable
{
    public class TestMachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Test machine.");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 20;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 2000;
            item.createTile = ModContent.TileType<AssemblingMachineFirst>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

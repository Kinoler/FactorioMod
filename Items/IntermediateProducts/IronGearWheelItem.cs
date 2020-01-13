using Terraria.ID;
using Terraria.ModLoader;

namespace FactorioMod.Items.IntermediateProducts
{
    public class IronGearWheelItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Iron plate");
            Tooltip.SetDefault("The iron plate is a material that can be made by smelting iron ore in a furnace.");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.rare = 2;
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.TimeItem>(), 500);
            recipe.AddIngredient(ModContent.ItemType<Items.IntermediateProducts.IronPlate>(), 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

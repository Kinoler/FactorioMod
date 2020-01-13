using Terraria.ID;
using Terraria.ModLoader;

namespace FactorioMod.Items
{
    public class TimeItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Craft time");
            Tooltip.SetDefault("This is the amount of time needed to craft the item in seconds at crafting speed 1.");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.rare = 2;
            item.maxStack = 99999;
        }
    }
}

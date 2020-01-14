using FactorioMod.Factorio.Models;
using Terraria;
using Terraria.ModLoader;

namespace FactorioMod.Factorio.Helpers
{
    public class RecipeHelper
    {
        public static void CreateFactorioRecipe(Mod mod, ModItem craftedItem, int timeInMiliseconds, params SimpleItemRepresentation[] ingredients)
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(SimpleItemRepresentation.GetItemId<Items.TimeItem>(), timeInMiliseconds);
            foreach (var ingredient in ingredients)
            {
                recipe.AddIngredient(ingredient.Id, ingredient.Stack);
            }
            recipe.SetResult(craftedItem);
            recipe.AddRecipe();
        }
    }
}

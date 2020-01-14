using System.Linq;
using FactorioMod.Items;
using Terraria;
using Terraria.ModLoader;

namespace FactorioMod.Factorio.Models
{
    public class FactorioRecipe
    {
        public string Category { get; set; }

        public Recipe Recipe { get; set; }

        public Item CreateItem => Recipe?.createItem;

        public Item[] RecipeIngredients => Recipe?.requiredItem.Where(el => el.type != 0).ToArray();

        public Item[] Ingredients => RecipeIngredients?.Where(el => el.type != ModContent.ItemType<TimeItem>()).ToArray();

        public double EnergyRequired => ((double)(RecipeIngredients?.FirstOrDefault(el => el.type == ModContent.ItemType<TimeItem>())?.stack ?? 1000)) / 1000;

        public bool IsRecipeSet => Recipe != null;

        public void SetRecipe(Recipe recipe)
        {
            if (IsRecipeSet)
                ResetRecipe();

            Recipe = recipe;
        }

        public void ResetRecipe()
        {
            Recipe = null;
        }
    }
}

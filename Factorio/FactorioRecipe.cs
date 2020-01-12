using System.Collections.Generic;
using System.Linq;
using FactorioMod.Factorio.Interfaces;
using Terraria;
using Terraria.ModLoader;

namespace FactorioMod.Factorio
{
    public class FactorioRecipe
    {
        public string Category { get; set; }

        public Recipe Recipe { get; set; }

        public Item CreateItem => Recipe?.createItem;

        public Item[] RequiredItems => Recipe?.requiredItem;

        public double EnergyRequired => RequiredItems?.First().stack / 100 ?? 1;

        public void SetRecipe(int recipeId)
        {
            if (Recipe != null)
                ResetRecipe();

            Recipe = Main.recipe[recipeId];
        }

        public void ResetRecipe()
        {
            Recipe = null;
        }
    }
}

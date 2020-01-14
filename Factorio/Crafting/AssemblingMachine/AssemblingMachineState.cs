using System.Collections.Generic;
using System.Linq;
using FactorioMod.Items;
using Terraria;
using Terraria.ModLoader;

namespace FactorioMod.Factorio.Crafting.AssemblingMachine
{
    public class AssemblingMachineState : CraftingMachineState
    {
        private readonly byte _ingredientsCountLimit;

        public AssemblingMachineState(double craftingSpeed, byte ingridientCountLimit)
            : base(craftingSpeed)
        {
            _ingredientsCountLimit = ingridientCountLimit;
        }

        public void SelectRecipe(Recipe recipe)
        {
            Recipe.SetRecipe(recipe);
            Ingredients.CloneFrom(Recipe.Ingredients);

            CreatedItem = Recipe.CreateItem.Clone();
            CreatedItem.stack = 0;
        }

        public void SelectRecipe(int recipeId)
        {
            SelectRecipe(Main.recipe[recipeId]);
        }

        public void ResetRecipe(int recipeId)
        {
            Recipe.ResetRecipe();
            Ingredients.Clear();
            CreatedItem = null;
        }

        //Находим все рецепты количество ингридиентов в каторых меньше _ingredientsCountLimit
        protected override Recipe[] GetAvailableRecipes()
        {
            return Main.recipe
                .Where(recipe =>
                    recipe.requiredItem.Any(item => item.type != ModContent.ItemType<TimeItem>()) &&
                    recipe.requiredItem.Count() < _ingredientsCountLimit + 1)
                .ToArray();
        }
    }
}

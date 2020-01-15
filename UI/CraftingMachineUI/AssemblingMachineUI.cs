using System.Linq;
using FactorioMod.Factorio.Crafting;
using FactorioMod.Factorio.Crafting.AssemblingMachine;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FactorioMod.UI.CraftingMachineUI
{
    public class AssemblingMachineUI : CraftingMachineUI<AssemblingMachineState>
    {
        public AssemblingMachineUI(AssemblingMachineState machine)
            : base(machine)
        {
            if (_machine.Recipe.IsRecipeEmpty)
            {
                RecipeFinder finder = new RecipeFinder();
                finder.SetResult(ModContent.ItemType<Items.IntermediateProducts.IronGearWheelItem>());
                Recipe recipe2 = finder.SearchRecipes().First();
                if (recipe2 != null)
                {
                    _machine.SelectRecipe(recipe2);
                }
            }
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            for (int i = 0; i < _reqItemSlot.Length; i++)
            {
                CreateIngredientsSlot(50 * i, 10, i);
            }

            CreateResultSlot(50 * _reqItemSlot.Length, 30);
        }
    }
}

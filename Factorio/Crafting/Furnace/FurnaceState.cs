using Terraria;

namespace FactorioMod.Factorio.Crafting.Furnace
{
    public class FurnaceState : CraftingMachineState
    {
        public FurnaceState(double power)
            : base(power)
        { }

        protected override Recipe[] GetAvailableRecipes()
        {
            return new Recipe[] { };
        }
    }
}

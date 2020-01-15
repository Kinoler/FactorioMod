using FactorioMod.Factorio.Crafting;
using FactorioMod.Factorio.Crafting.AssemblingMachine;
using FactorioMod.Factorio.Crafting.Furnace;
using FactorioMod.Tiles.Placeable.CraftingMachine;
using FactorioMod.Tiles.Placeable.CraftingMachine.Furnace;
using FactorioMod.Tiles.Placeable.CraftingMachine.AssemblingMachine;

namespace FactorioMod.Factorio.Helpers
{
    public static class FactoryHelper
    {
        public static CraftingMachineState CraftingMachineStateFactory(CraftingMachine machine)
        {
            switch (machine)
            {
                case AssemblingMachine assemblingMachine:
                    return new AssemblingMachineState(assemblingMachine.BaseCraftingSpeed, assemblingMachine.IngridientCountLimit);
                case Furnace furnace:
                    return new FurnaceState(furnace.BaseCraftingSpeed);
                default:
                    return null;
            }
        }
    }
}

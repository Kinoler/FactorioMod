using FactorioMod.Factorio.Crafting;
using FactorioMod.Factorio.Crafting.AssemblingMachine;
using FactorioMod.Factorio.Crafting.Furnace;
using FactorioMod.Tiles.Placeable.CraftingMachine.Furnace;
using FactorioMod.Tiles.Placeable.CraftingMachine.AssemblingMachine;

namespace FactorioMod.Factorio.Helpers
{
    public static class FactoryHelper
    {
        public static CraftingMachineState CraftingMachineStateFactory(dynamic machine)
        {
            return GetInstanceCraftingMachineState(machine);
        }

        private static CraftingMachineState GetInstanceCraftingMachineState(AssemblingMachine machine) 
            => new AssemblingMachineState(machine.BaseCraftingSpeed);

        private static CraftingMachineState GetInstanceCraftingMachineState(Furnace machine) 
            => new FurnaceState(machine.BaseCraftingSpeed);
    }
}

using FactorioMod.Factorio;
using FactorioMod.Factorio.Helpers;
using Terraria.ModLoader;

namespace FactorioMod
{
    public class FactorioWorld : ModWorld
    {
        public override void PostUpdate()
        {
            base.PostUpdate();
            FactorioTimer.Tick();
        }
    }
}

using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace FactorioMod
{
    public class FactorioPlayer : ModPlayer
    {
        private FactorioMod _factorioMod = GetInstance<FactorioMod>();

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (FactorioMod.OpenHotKey.JustPressed)
            {
                _factorioMod.SwichMyUI();
            }
        }
    }
}

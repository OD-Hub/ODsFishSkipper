using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace ODs_Fish_Skipper
{
    /// <summary>The mod configuration.</summary>
    public class ModConfig
    {
        /// <summary>The key to press to unlock all fish.</summary>
        public KeybindList UnlockKey { get; set; } = KeybindList.ForSingle(SButton.F9);
    }
}

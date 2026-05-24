using System;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace ODs_Fish_Skipper
{
    /// <summary>The API for Generic Mod Config Menu (optional dependency).</summary>
    /// <remarks>Signature must match GMCM exactly or SMAPI's proxy will fail.</remarks>
    public interface IGenericModConfigMenuApi
    {
        void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);

        void AddKeybindList(
            IManifest mod,
            Func<KeybindList> getValue,
            Action<KeybindList> setValue,
            Func<string> name,
            Func<string>? tooltip = null,
            string? fieldId = null
        );

        void AddBoolOption(
            IManifest mod,
            Func<bool> getValue,
            Action<bool> setValue,
            Func<string> name,
            Func<string>? tooltip = null,
            string? fieldId = null
        );
    }
}

using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ODs_Fish_Skipper
{
    public class ModEntry : Mod
    {
        private ModConfig Config = null!;

        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();

            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;

            this.Monitor.Log("OD's Fish Skipper loaded! Press the configured key to unlock all fish.", LogLevel.Info);
        }

        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new ModConfig(),
                save: () => this.Helper.WriteConfig(this.Config)
            );

            configMenu.AddKeybindList(
                mod: this.ModManifest,
                getValue: () => this.Config.UnlockKey,
                setValue: value => this.Config.UnlockKey = value,
                name: () => "Unlock Key",
                tooltip: () => "The keybind to press to mark all fish as caught and unlock the Master Angler achievement."
            );
        }

        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            if (this.Config.UnlockKey.JustPressed())
                this.UnlockAllFish();
        }

        private void UnlockAllFish()
        {
            int count = 0;

            var fishData = Game1.content.Load<Dictionary<string, string>>("Data/Fish");
            foreach (string id in fishData.Keys)
            {
                if (!Game1.player.fishCaught.ContainsKey(id))
                {
                    Game1.player.fishCaught[id] = new[] { 1, 0 };
                    count++;
                }
            }

            Game1.stats.checkForFishingAchievements();

            string message = count > 0
                ? $"Marked {count} fish as caught! Master Angler achievement unlocked."
                : "You've already caught all fish!";

            Game1.addHUDMessage(new HUDMessage(message, HUDMessage.achievement_type));
            this.Monitor.Log($"Unlocked {count} new fish records.", LogLevel.Info);
        }
    }
}

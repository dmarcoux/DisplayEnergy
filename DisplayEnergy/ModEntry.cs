using Microsoft.Xna.Framework;

using StardewModdingAPI;
using StardewModdingAPI.Events;

using StardewValley;
using StardewValley.Extensions;

namespace dmarcoux.Stardew.DisplayEnergy
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Properties
        *********/
        /// <summary>The mod configuration from the player.</summary>
        private ModConfig Config;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            helper.Events.GameLoop.GameLaunched += this.RegisterForGenericModConfigMenu;
            helper.Events.Display.RenderingHud += this.DisplayStamina;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Display the player's stamina</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void DisplayStamina(object sender, RenderingHudEventArgs e)
        {
            Vector2 energyTextOffset = this.Config.EnergyTextOffset;
            if (Game1.showingHealth)
            {
                float x = energyTextOffset.X;
                energyTextOffset = energyTextOffset with { X = x + 50 };
            }

            if (!Game1.eventUp)
            {
                Game1.spriteBatch.DrawString(Game1.dialogueFont, $"{(int)Game1.player.Stamina}/{Game1.player.maxStamina}", new Vector2(Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea().Right - energyTextOffset.X, Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea().Bottom - energyTextOffset.Y), Color.White);
            }
        }

        /// <summary>
        /// Raised after the game is launched, right before the first update tick.
        /// This happens once per game session (unrelated to loading saves).
        /// All mods are loaded and initialised at this point, so this is a good time to set up mod integrations.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event data</param>
        private void RegisterForGenericModConfigMenu(object sender, GameLaunchedEventArgs e)
        {
            // Get Generic Mod Config Menu's API (if it's installed)
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
            {
                return;
            }

            // Register this mod
            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new ModConfig(),
                save: () => this.Helper.WriteConfig(this.Config)
            );

            // Add config options
            configMenu.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Horizontal offset from right",
                tooltip: () => "Sets how far the energy text is offset from the right side of the screen",
                getValue: () => (int)this.Config.EnergyTextOffset.X,
                setValue: value =>
                {
                    this.Config.EnergyTextOffset =
                        this.Config.EnergyTextOffset with { X = value };
                },
                min: 0,
                max: Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea().Right);

            configMenu.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Vertical offset from bottom",
                tooltip: () => "Sets how far the energy text is offset from the bottom side of the screen",
                getValue: () => (int)this.Config.EnergyTextOffset.Y,
                setValue: value =>
                {
                    this.Config.EnergyTextOffset =
                        this.Config.EnergyTextOffset with { Y = value };
                },
                min: 0,
                max: Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea().Bottom);
        }
    }
}
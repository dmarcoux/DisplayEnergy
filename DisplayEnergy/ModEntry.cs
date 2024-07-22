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
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
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
            int textRightPosition = (Game1.showingHealth) ? 265 : 215;

            if (!Game1.eventUp)
            {
                Game1.spriteBatch.DrawString(Game1.dialogueFont, $"{(int)Game1.player.Stamina}/{Game1.player.maxStamina}", new Vector2(Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea().Right - textRightPosition, Game1.graphics.GraphicsDevice.Viewport.GetTitleSafeArea().Bottom - 60), Color.White);
            }
        }
    }
}
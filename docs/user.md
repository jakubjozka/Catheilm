# User documentation

## Installation

1. Install **Visual Studio** with the **.NET Desktop Development** workload.
2. Download or copy the `/src/` folder to your computer.
3. Open the solution file (`.sln`) inside the `/src/` folder in Visual Studio.
4. Restore the required NuGet packages by right clicking on the Solution, choosing "Manage NuGet Packages for Solution..." and installing the following:
   - `Newtonsoft.Json`
   - `D20Tek.Common.Net47`
   - `D20Tek.DiceNotation.Net47`
5. Build and run the project.

> **Note:** Restoring the above packages will also automatically install any additional dependencies required by the project.

## Gameplay Guide
The entire game can be played using only the mouse or a touchpad, as all interactions are handled through buttons and selection boxes.

When the player launches the game, the startup screen appears with three options:
- **Start New Game** – Opens the character creation screen.
- **Load Save Game** – Opens the save file directory. Selecting a save file loads the game from the point where it was last saved.
- **Exit** – Closes the game.

### Character Creation
In the character creation screen, the player can:
- Choose a **name**.
- Select a **race**.
- Roll for new attributes using **Roll New Player**.
- When satisfied, click **Use This Player** to start the main game.

### Main Game Interface
After starting the game, introductory text appears in the main text field.  
The interface is arranged as follows:
- **Middle:** Main text field
- **Left side:** Player stats (name, hit points, gold, XP, level, attributes).
- **Bottom left:** Inventory, Quests, and Recipes sections.
- **Bottom middle:** Weapon selection for combat and consumable usage.
- **Bottom right:** Directional movement buttons (**North**, **West**, **East**, **South**).
- **Right side:** Current location and monsters (if present).
- **Top left (File menu):**
  - **Start New Game** – Returns to the character creation screen.
  - **Save Game** – Opens the save directory to create or overwrite a save file.
  - **Exit** – Prompts to save before closing the game.

### Gameplay Actions
- **Movement:** Use directional buttons to move between locations.
- **Quests:** Some locations offer quests requiring the player to defeat certain enemies for rewards.
- **Trading:** Certain locations allow trading, opening a trade screen to buy and sell items between the player and an NPC.
- **Combat:** In locations with monsters, the player can attack using the equipped weapon, gain rewards for defeating them, or be defeated.
- **Inventory Management:** Equip weapons, use consumables, and craft by certain recipes.
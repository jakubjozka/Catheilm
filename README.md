# Zápočtový program: Catheilm

## Specification

Catheilm is a fantasy RPG game built as a WPF application using the MVVM architecture. It includes systems for world exploration, turn-based combat, inventory management, crafting, quest tracking, trading, and saving/loading game progress.

The player can create a character by choosing a race and rolling for attributes, move between locations, fight monsters, collect and equip items, craft new items, trade with NPCs, complete quests, gain experience, and level up.

## Installation

1. Install **Visual Studio** with the **.NET Desktop Development** workload.
2. Download or copy the `/src/` folder to your computer.
3. Open the solution file (`.sln`) inside the `/src/` folder in Visual Studio.
4. Restore the required NuGet packages:
   - `Newtonsoft.Json`
   - `D20Tek.Common.Net47`
   - `D20Tek.DiceNotation.Net47`
5. Build and run the project.

> **Note:** Restoring the above packages will also automatically install any additional dependencies required by the project.

## Documentation

* [Uživatelská dokumentace](docs/user.md)
* [Programátorská dokumentace](docs/programmer.md)

# Development notes

This was one of my biggest projects and I am very happy with how it turned out code-wise, as I mainly wanted to practice what we did in *Programování 2 (NPRG031)* and also tried out things I hadn’t implemented in my previous games, like loading and saving progress and working with game data. 
One of my future goals is to complete everything I listed in the *“What could be added”* section, and since the UI isn’t looking the best, I also want to try updating it, even though my previous attempts caused so many issues that I had to completely revert to the last version. 
In contrast to my last game, where the visuals were really pretty but the code was buggy, this time it’s the opposite, I’m proud of the code, but not the visuals.

## Progression of work

I built a factory system for my game, responsible for loading all entities and items.
In the first version, I simply instantiated all the classes directly inside the factory class itself, but that quickly became messy and hard to maintain.
To improve this, I switched to loading game data from files.
I chose .xml as the format, not because it was the best choice, but because my UI used .xaml files, and the names sounded similar.
Luckily, XML turned out to be straightforward and easy to learn.
Later, when I wanted to add player attributes and races, I wanted to try out another game data file.
So I decided to experiment with .json, since I’d heard it was widely used.
Both formats are simple, and once you understand one, picking up the other is easy.
To avoid duplicating code, I wrote parsers for both XML and JSON, which I placed in `ExtensionMethods.cs`.

When I first started working on the combat system, all of the logic was contained in the `GameSession.cs` file. 
My initial idea was to implement an `AttackWithWeapon` action and then categorize items (e.g., weapon, consumable) so they could subscribe to specific actions such as `Heal` or `AttackWithWeapon`. 
This approach was more flexible and allowed me to plan for future actions like `Poison`, `Burn`, and others. 
However, since `AttackWithWeapon` still contained a lot of code, I decided to refactor it by creating a `CombatService` to handle the damage calculations and a `Battle` class to manage most of the text output. 
I also revised the way randomness was handled. 
At first, I used a custom `RandomNumberGenerator` service, based on code I found online. 
But since the game leans toward an RPG style, I wanted to make it feel more like *Dungeons & Dragons*, so I replaced it with a dice-rolling system using the **D20Tek** NuGet package, which now handles all random number generation.


Since the game doesn’t yet have a fully developed story, I plan to expand on that in the future. 
For now, I’ve created a small city called *Catheilm*, home to all cats. 
The game currently features three quests, each unlocking a new location with a stronger monster than the last. 
In the final stage, the player faces a powerful boss, and defeating it marks the completion of the game.

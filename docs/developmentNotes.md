# Progression of work

Here I want to list some of the things that I have changed through the making of this game.

## Loading gamedata

I built a factory system for my game, responsible for loading all entities and items.
In the first version, I simply instantiated all the classes directly inside the factory class itself, but that quickly became messy and hard to maintain.
To improve this, I switched to loading game data from files.
I chose .xml as the format, not because it was the best choice, but because my UI used .xaml files, and the names sounded similar.
Luckily, XML turned out to be straightforward and easy to learn.
Later, when I wanted to add player attributes and races, I wanted to try out another game data file.
So I decided to experiment with .json, since I’d heard it was widely used.
Both formats are simple, and once you understand one, picking up the other is easy.
To avoid duplicating code, I wrote parsers for both XML and JSON, which I placed in ExtensionMethods.cs.


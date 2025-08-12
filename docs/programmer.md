# Programming Documentation
Catheilm is built as a Windows Presentation Foundation (WPF) application using the Model–View–ViewModel (MVVM) architectural pattern. This design separates the UI layer from the game logic and data models. The game is data-driven, meaning most gameplay elements (items, monsters, quests, locations) are loaded from external data files instead of being hardcoded. This makes it possible to add new content by editing data files rather than recompiling the program. The program uses NuGet packages to handle JSON serialization (Newtonsoft.Json) and to provide dice notation parsing and random number utilities (D20Tek.Common.Net47, D20Tek.DiceNotation.Net47). The program follows object-oriented programming principles, with a clear hierarchy and shared base classes to reduce code duplication.

The program is split into two main projects — **Engine** and **WPFUI**. The Engine project contains all the core game logic, data models, and services. The WPFUI project contains the user interface, written in XAML, and is responsible for presenting the game state to the player. 

---

## **Engine**  
The Engine project contains the core game logic and data definitions.  

### **Actions**  
Defines actions that can be performed in combat.  

- **BaseAction**: Stores the item in use, handles events for action results, and provides a `ReportResult` method.  
- **IAction**: Requires an `Execute()` method and `OnActionPerformed` event.  
- **AttackWithWeapon**: Inherits from `BaseAction` and `IAction`. Executes an attack action, reporting attacker, target, and damage dealt.  
- **Heal**: Inherits from `BaseAction` and `IAction`. Executes a healing action, reporting healer, target, and amount healed.  

### **EventArgs**  
Event argument classes for event-based communication.  

- **CombatVictoryEventArgs**: Singleton for combat victory events.  
- **GameMessageEventArgs**: Holds a string message for in-game notifications.  

### **Factories**  
Load game data from XML and create model instances.  

- **ItemFactory**: Reads `GameItems.xml` and creates `GameItem` objects.  
- **MonsterFactory**: Reads `Monsters.xml` and returns monsters by ID.  
- **QuestFactory**: Reads `Quests.xml` and returns quests by ID.  
- **RecipeFactory**: Reads `Recipes.xml` and returns recipes by ID.  
- **TraderFactory**: Reads `Traders.xml` and returns traders by ID.  
- **WorldFactory**: Reads `Locations.xml` and creates the game world, adding monsters, quests, and traders to locations.  

### **GameData**  
XML and JSON files defining game content like items, monsters, locations, quests, recipes, traders, races, and game metadata.  

### **Images**  
PNG assets for locations and monsters.  

### **Models**  
Core data structures representing game entities and supporting classes. Here are the most important ones: 
- **Battle**: Manages turn-based combat.  
- **GameDetails**: Stores general game metadata.  
- **GameItem**: Represents items in the game.  
- **GroupedInventoryItem**: Represents grouped items in inventory.  
- **LivingEntity**: **Base class** for all living entities (Player, Monster, Trader). Contains HP, attributes, inventory, etc.  
- **Player**: Inherits from `LivingEntity`, adds XP, level, and player-specific methods.  
- **Monster**: Inherits from `LivingEntity`, adds loot and monster-specific behavior.   
- **Location**: Contains all information about a certain Location.  
- **Quest**, **Recipe**, **World**: Handle quests, crafting, and the overall game world.  

### **Services**  
Game logic services and utilities.  

- **CombatService**: Determines first attacker and hit/miss results.  
- **DiceService / IDiceService**: Dice rolling using the external D20Tek library.  
- **GameDetailService**: Loads `GameDetails` from JSON.  
- **MessageBroker**: Singleton event/message handler.  
- **SaveGameService**: Handles saving/loading the game state to/from JSON.  

### **Shared**  
- **ExtensionMethods**: XML and JSON parsing utilities.  

### **ViewModels**  
- **CharacterCreationModel**: Handles character creation logic.  
- **GameSession**: Manages the current game state — player, location, battle, movement, quests, and crafting.  
- **BaseNotificationClass**: Implements `INotifyPropertyChanged` for UI updates.  

---

## **WPFUI**  
The WPFUI project contains the presentation layer (XAML views and converters).  

### **Windows**  
- **Startup.xaml**: Startup menu (new game, load game, exit).  
- **CharacterCreation.xaml**: UI for name, race, and attribute selection.  
- **MainWindow.xaml**: Main gameplay interface.  
- **TradeScreen.xaml**: Trading interface.  
- **YesNoWindow.xaml**: Confirmation dialog.  

### **CustomConverters**  
- **FileToBitmapConverter**: Converts file paths to image bitmaps for UI.  

### **Fonts**  
- Custom pixel-style font *(Pixel Game.otf)*.  
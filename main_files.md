# Main Files Overview

Here is an explanation of the primary files that drive the Dungeon Architect application:

## UI and Rendering
* **`Forms/MainForm.cs`**: The primary user interface and entry point. It sets up the main window, the drawing canvas, and the tool buttons. It handles mouse/keyboard events (like dragging elements or moving the hero in Playtest mode) and delegates actions to the `CommandManager` or `DungeonScene`.
* **`DungeonRenderer.cs`**: Responsible for the visual representation of the game. It takes a `DungeonScene` and iterates through all elements, drawing them on the Graphics canvas using appropriate shapes, colors, and pens based on their state (e.g., broken traps, open chests).
* **`Forms/StatsForm.cs`**: A dedicated window that calculates and displays live statistics about the dungeon (e.g., active durability, element counts) using advanced LINQ queries.

## Core Logic and State
* **`ArchitectDungeon.Core/Core/DungeonScene.cs`**: The heart of the simulation. It holds the collection of all `DungeonElement`s. It manages the hero's movement (`TryMoveHero`) and checks for interactions with traps and chests (`UpdateProximityEngine`).
* **`ArchitectDungeon.Core/Models/DungeonElement.cs`**: The abstract base class from which all placeable entities (`Hero`, `Wall`, `Trap`, `TreasureChest`) derive. It defines common properties like X, Y, Id, Durability, and FillColor, as well as virtual methods for moving and triggering.

## Infrastructure
* **`ArchitectDungeon.Core/Commands/CommandManager.cs`**: Implements the logic for Undo/Redo functionality using two Stacks of `ICommand` objects.
* **`ArchitectDungeon.Core/FileStorageService.cs`**: Handles the persistence of the dungeon. It uses `System.Text.Json` to serialize the elements into a JSON file for saving, and deserializes them to load a level back into memory.

# Project Architecture

The Dungeon Architect project follows a clear **Separation of Concerns** architecture, dividing the application into two main parts: the Core business logic and the UI (User Interface).

## 1. Core Library (`ArchitectDungeon.Core`)
This is a separate class library that contains all the non-visual business logic, models, and data management. It compiles into a DLL (Dynamic Link Library) and is entirely independent of the Windows Forms UI.
* **Models (`Models/`)**: Contains the classes that define the state and behavior of the dungeon entities (`DungeonElement`, `Hero`, `Trap`, `Wall`, `TreasureChest`).
* **Logic & State (`Core/`)**: The `DungeonScene` class acts as the central data structure, maintaining a list of elements and handling proximity/movement logic.
* **Commands (`Commands/`)**: Implements the **Command Pattern** (`ICommand`, `AddElementCommand`, `MoveElementCommand`) along with the `CommandManager` to provide robust Undo and Redo functionality.
* **Services**: The `FileStorageService` handles saving and loading the `DungeonScene` state to and from JSON files.

## 2. Windows Forms UI (`architectSteps`)
This is the executable application that the user interacts with. It references the Core library to manipulate the underlying data.
* **Forms (`Forms/`)**: Contains the UI windows like `MainForm`, `StatsForm`, and various editors (`TrapEditorForm`, etc.). It captures user input (mouse clicks, key presses) and translates them into actions or commands on the `DungeonScene`.
* **Rendering**: The `DungeonRenderer` class handles the graphics drawing, taking the state from `DungeonScene` and rendering it onto a `Panel` canvas using `System.Drawing` (GDI+).

### Benefits of this Architecture
* **Reusability**: The Core library can be reused in a different UI framework (e.g., WPF, Avalonia, or a console app) without modifications.
* **Maintainability**: UI bugs are isolated from logic bugs, making the code easier to test and maintain.

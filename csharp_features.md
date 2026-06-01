# C# Features in the Project

The project makes extensive use of modern C# features. Here is where you can find them:

### 1. Generalized Types (Generics)
Generics are used to create type-safe collections and methods.
* **`ArchitectDungeon.Core/Core/DungeonScene.cs`**: Uses `List<DungeonElement>` to store the elements.
* **`ArchitectDungeon.Core/Commands/CommandManager.cs`**: Uses `Stack<ICommand>` for the undo and redo stacks.

### 2. Delegates
Delegates are used to pass methods as arguments and for event handling.
* **`Forms/MainForm.cs`**: Event handler delegates are used throughout to respond to user actions, e.g., `_canvas.Paint += Canvas_Paint;` (uses `PaintEventHandler`) and `_addHeroButton.Click += AddHero_Click;` (uses `EventHandler`).

### 3. Lambda Expressions
Lambdas provide a concise way to write inline functions, often used with LINQ and events.
* **`Forms/StatsForm.cs`**: Used for inline event handling: `_refreshTimer.Tick += (s, e) => UpdateStats();`
* **`ArchitectDungeon.Core/Core/DungeonScene.cs`**: Used inside LINQ queries: `_elements.FirstOrDefault(e => e.GetBounds().Contains(x, y));`

### 4. Expanding Methods (Extension Methods)
Extension methods allow adding methods to existing types.
* **Throughout the project**: The project utilizes the built-in extension methods provided by `System.Linq` that extend `IEnumerable<T>`. For example, `.OfType<Hero>()` and `.FirstOrDefault()` are extension methods called on lists in `MainForm.cs` and `DungeonScene.cs`.

### 5. LINQ (Language Integrated Query)
LINQ is used for declarative data querying and aggregation.
* **`Forms/StatsForm.cs`**: Features advanced LINQ usage, including filtering (`.Where()`), aggregation (`.Sum()`, `.Max()`), grouping (`.GroupBy()`), sorting (`.OrderByDescending()`), and projection (`.Select()`).
* **`ArchitectDungeon.Core/Core/DungeonScene.cs`**: Uses LINQ to find specific elements, e.g., `_elements.OfType<Wall>()`.

### 6. Attributes
Attributes provide metadata about code elements.
* **`ArchitectDungeon.Core/Models/DungeonElement.cs`**: Uses the `[JsonIgnore]` attribute on the `FillColor` property to tell the JSON serializer not to save this specific property.
* **`Program.cs`**: Uses `[STAThread]`, which is required for Windows Forms applications to interact with COM and the clipboard properly.

### 7. Libraries
External and system namespaces are imported via `using` directives.
* **`ArchitectDungeon.Core/FileStorageService.cs`**: Uses `using System.Text.Json;` to bring in the Microsoft JSON serialization library.
* **`Forms/MainForm.cs`**: Uses `using System.Windows.Forms;` and `using System.Drawing;` for the UI and graphics libraries.

### 8. Reflection (Reflection)
Reflection allows the code to inspect object types at runtime.
* **`Forms/StatsForm.cs`**: Uses reflection within a LINQ query to group elements by their actual runtime class name: `.GroupBy(e => e.GetType().Name)`.
* **`ArchitectDungeon.Core/FileStorageService.cs`**: The `JsonSerializer` heavily uses reflection behind the scenes to map JSON text to C# object properties during load/save operations.

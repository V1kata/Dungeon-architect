# Forms and DLL Library

## DLL Library (`ArchitectDungeon.Core`)
The core logic of the application is separated into a **Class Library** project (`ArchitectDungeon.Core.csproj`). 
* **Target Framework**: It is configured to target `net8.0`.
* **Output**: When compiled, instead of producing an executable (`.exe`), this project produces a Dynamic Link Library (`ArchitectDungeon.Core.dll`).
* **Purpose**: This DLL encapsulates all the models, business rules, and services (like JSON storage). By isolating this code into a DLL, it ensures that the UI does not mix with the logic, promoting modularity. 

## Windows Forms UI (`architectSteps`)
The main user interface is built using **Windows Forms** (WinForms) via the `architectSteps.csproj` project.
* **Target Framework**: It targets `net8.0-windows` and explicitly enables WinForms with `<UseWindowsForms>true</UseWindowsForms>`.
* **Linking the DLL**: The UI project references the Core library using a Project Reference: `<ProjectReference Include="ArchitectDungeon.Core\ArchitectDungeon.Core.csproj" />`. This allows the UI code to access classes like `DungeonScene` and `Hero` from the DLL.
* **Form Creation**: Instead of relying heavily on the Visual Studio Drag-and-Drop Designer, the forms in this project (such as `MainForm.cs` and `StatsForm.cs`) construct their UI elements programmatically in a `SetupUI()` method. Controls like `Button`, `Label`, and `Panel` are instantiated, configured with sizes and locations, and added to the form's `Controls` collection via code. Events are also wired up manually (e.g., `_addHeroButton.Click += AddHero_Click;`).

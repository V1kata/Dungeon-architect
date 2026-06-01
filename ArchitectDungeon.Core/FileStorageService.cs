using System.IO;
using System.Linq;
using System.Text.Json;

namespace DungeonArchitect
{
    public class FileStorageService
    {
        public void SaveToFile(string path, DungeonScene scene)
        {
            var data = new SaveData
            {
                Heroes = scene.Elements.OfType<Hero>().ToList(),
                Walls = scene.Elements.OfType<Wall>().ToList(),
                Traps = scene.Elements.OfType<Trap>().ToList(),
                Chests = scene.Elements.OfType<TreasureChest>().ToList()
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(path, json);
        }

        public void LoadFromFile(string path, DungeonScene scene)
        {
            if (!File.Exists(path)) return;

            string json = File.ReadAllText(path);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<SaveData>(json, options);

            var existingElements = scene.Elements.ToList();
            foreach (var el in existingElements)
            {
                scene.RemoveElement(el);
            }

            if (data != null)
            {
                if (data.Heroes != null) foreach (var h in data.Heroes) scene.AddElement(h);
                if (data.Walls != null) foreach (var w in data.Walls) scene.AddElement(w);
                if (data.Traps != null) foreach (var t in data.Traps) scene.AddElement(t);
                if (data.Chests != null) foreach (var c in data.Chests) scene.AddElement(c);
            }
        }
    }
}

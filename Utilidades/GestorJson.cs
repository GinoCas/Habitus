namespace Habitus.Utilidades
{
    using System.Text.Json;

    public class GestorJson<T> where T : class
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _options;

        public GestorJson(string fileName)
        {
            var appPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Habitus");
            _filePath = Path.Combine(appPath, fileName);
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
            _options = new JsonSerializerOptions { WriteIndented = true };
        }

        private List<T> Load()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
            //return new List<T>();
        }

        private void Save(List<T> items)
        {
            /*var json = JsonSerializer.Serialize(items, _options);
            File.WriteAllText(_filePath, json);*/
        }

        public void Add(T item)
        {
            /*var items = Load();
            items.Add(item);
            Save(items);*/
        }

        public List<T> GetAll() => Load();

        public void Update(Func<T, bool> predicate, Action<T> updateAction)
        {
            /*var items = Load();
            var item = items.FirstOrDefault(predicate);
            if (item != null)
            {
                updateAction(item);
                Save(items);
            }*/
        }

        public void Delete(Func<T, bool> predicate)
        {
            /*var items = Load();
            var item = items.FirstOrDefault(predicate);
            if (item != null)
            {
                items.Remove(item);
                Save(items);
            }*/
        }
    }
}

namespace Habitus.Utilidades
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;

    public class GestorJson<T> where T : class
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _options;

        public GestorJson(string fileName, bool isSystemFile)
        {
            string subfolder = isSystemFile ? Path.Combine("Datos", "Sistema") : Path.Combine("Datos", "Usuario");
            var appPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Habitus", subfolder);
            _filePath = Path.Combine(appPath, fileName);

            var directory = Path.GetDirectoryName(_filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            _options = new JsonSerializerOptions { WriteIndented = true };
        }

        private List<T> Load()
        {
            if (!File.Exists(_filePath))
                MessageBox.Show("No existe un archivo json para el archivo:" + _filePath);
                return new List<T>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
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
            var itemsToDelete = items.Where(predicate).ToList();
            if (itemsToDelete.Any())
            {
                items = items.Except(itemsToDelete).ToList();
                Save(items);
            }*/
        }
    }
}

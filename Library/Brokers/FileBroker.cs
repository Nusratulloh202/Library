using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library.Brokers
{
    internal class FileBroker
    {
        public List<T> LoadData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }
            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();   
        }
        public void SaveData<T>(string filePath, List<T> data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }
}

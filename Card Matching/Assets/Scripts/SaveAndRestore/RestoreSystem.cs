using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace CardMatching.Saverestore
{
    public class RestoreSystem : IRestoreSystem
    {
        private string _saveDirectory;
        private string _fileExtension = ".dat";

        public RestoreSystem(string saveDirectory)
        {
            _saveDirectory = saveDirectory;
        }

        /// <summary>
        /// Loads an object from a binary file
        /// </summary>
        public T Load<T>(string saveId) where T : class
        {
            try
            {
                // Construct full file path
                string fullPath = Path.Combine(_saveDirectory, $"{saveId}{_fileExtension}");

                // Check if file exists
                if (!File.Exists(fullPath))
                {
                    Debug.Log($"No save file found at: {fullPath}");
                    return null;
                }

                // Create a binary formatter
                BinaryFormatter formatter = new BinaryFormatter();

                // Open the file
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    // Deserialize the file into the object
                    T loadedObject = (T)formatter.Deserialize(stream);
                    Debug.Log($"Object of type {typeof(T).Name} loaded successfully from: {fullPath}");
                    return loadedObject;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading {typeof(T).Name}: {e.Message}");
                return null;
            }
        }
    }
}
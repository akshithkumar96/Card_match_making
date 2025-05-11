using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace CardMatching.Saverestore
{
    /// <summary>
    /// Binary save system
    /// </summary>
    public class SaveSystem : ISaveSystem
    {
        private string _saveDirectory;
        private string _fileExtension = ".dat";


        public SaveSystem(string saveDirectory)
        {
            _saveDirectory = saveDirectory;
        }

        /// <summary>
        /// Saves any serializable object to a binary file
        /// </summary>
        public void Save<T>(T objectToSave, string saveId) where T : class
        {
            try
            {
                // Validate input
                if (objectToSave == null)
                {
                    Debug.LogError("Cannot save null object");
                }

                // Check if type is serializable
                if (!typeof(T).IsSerializable)
                {
                    Debug.LogError($"Type {typeof(T).Name} is not marked as Serializable");
                }

                // Construct full file path
                var fullPath = Path.Combine(_saveDirectory, $"{saveId}{_fileExtension}");

                // Create a binary formatter
                var formatter = new BinaryFormatter();

                // Create a file stream
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    // Serialize the object to the stream
                    formatter.Serialize(stream, objectToSave);
                }

                Debug.Log($"Object of type {typeof(T).Name} saved successfully to: {fullPath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error saving {typeof(T).Name}: {e.Message}");
            }
        }
    }
}

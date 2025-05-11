
namespace CardMatching.Saverestore
{
    public interface ISaveSystem
    {
        /// <summary>
        /// Saves a serializable object to storage
        /// </summary>
        /// <typeparam name="T">Type of object to save</typeparam>
        /// <param name="objectToSave">The object to save</param>
        /// <param name="saveId">Identifier for the save (eg:- filename)</param>
        void Save<T>(T objectToSave, string saveId) where T : class;
    }
}
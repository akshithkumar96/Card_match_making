
namespace CardMatching.Saverestore
{
    public interface IRestoreSystem
    {
        /// <summary>
        /// Loads a serializable object 
        /// </summary>
        /// <typeparam name="T">Type of object to load</typeparam>
        /// <param name="saveId">Identifier for the save (filename)</param>
        /// <returns>The loaded object, or null if loading failed</returns>
        T Load<T>(string saveId) where T : class;
    }
}
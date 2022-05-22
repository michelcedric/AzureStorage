namespace AzureStorageBlobs
{
    /// <summary>
    /// Represent the azure authentication information
    /// </summary>
    public class AzureAuthentication
    {
        /// <summary>
        /// Default initialize azure account information
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="containerName"></param>     
        public AzureAuthentication(string connectionString, string containerName)
        {
            ConnectionString = connectionString;
            ContainerName = containerName;
        }

        /// <summary>
        /// Get or set the account name
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Get or set the container name
        /// </summary>
        public string ContainerName { get; private set; }

    }
}

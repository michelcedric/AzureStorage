
namespace Azure
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
        /// <param name="keyValue"></param>
        public AzureAuthentication(string accountName, string containerName, string keyValue)
        {
            AccountName = accountName;
            ContainerName = containerName;
            KeyValue = keyValue;
        }
      
        /// <summary>
        /// Get or set the account name
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Get or set the container name
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// Get or set the key value
        /// </summary>
        public string KeyValue { get; set; }

    }
}

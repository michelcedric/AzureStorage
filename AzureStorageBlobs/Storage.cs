using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AzureStorageBlobs
{
    /// <summary>
    /// Object to mnanage a azure storage
    /// </summary>
    public class Storage
    {
        private readonly AzureAuthentication _authentication;
        private BlobContainerClient _blobContainerClient;

        /// <summary>
        /// Initialize a azure storage
        /// </summary>
        /// <param name="connectionString">Account name of your storage</param>
        /// <param name="containerName">Container name of your storage</param>     
        public Storage(string connectionString, string containerName)
        {
            _authentication = new AzureAuthentication(connectionString, containerName);
            InitializeAzureConnection();
        }

        /// <summary>
        /// Initialize a azure storage 
        /// </summary>
        /// <param name="authentication">AzureAuthentication object</param>
        public Storage(AzureAuthentication authentication)
        {
            _authentication = authentication;
            InitializeAzureConnection();
        }

        /// <summary>
        /// Initialize the connection with the azure container
        /// </summary>
        private void InitializeAzureConnection()
        {
            var blobServiceClient = new BlobServiceClient(_authentication.ConnectionString);
            _blobContainerClient = blobServiceClient.CreateBlobContainer(_authentication.ContainerName).Value;
        }

        /// <summary>
        /// Upload file from stream to Azure
        /// </summary>
        /// <param name="file">Stream file</param>     
        /// <param name="fileName">the name to give on your file</param>    
        public void Upload(Stream file, string fileName, CancellationToken cancellationToken = default)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            blobClient.Upload(file, cancellationToken);
        }

        /// <summary>
        /// Upload file from stream to Azure
        /// </summary>
        /// <param name="file">Stream file</param>     
        /// <param name="fileName">the name to give on your file</param>    
        public async Task UploadAsync(Stream file, string fileName, CancellationToken cancellationToken = default)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(file, cancellationToken);
        }

        /// <summary>
        /// Upload file from stream to Azure
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The real name of your file on Azure (guid)</returns>
        public string Upload(Stream file)
        {
            var guid = Guid.NewGuid();
            Upload(file, guid.ToString());
            return guid.ToString();
        }

        /// <summary>
        /// Upload file from stream to Azure
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The real name of your file on Azure (guid)</returns>
        public async Task<string> UploadAsync(Stream file)
        {
            var guid = Guid.NewGuid();
            await UploadAsync(file, guid.ToString());
            return guid.ToString();
        }

        /// <summary>
        /// Upload file from string (base64)
        /// </summary>
        /// <param name="file">file as base64</param>
        /// <param name="fileName">the name to give on your file</param>     
        public async Task UploadAsync(string file, string fileName)
        {
            byte[] fileArray = Convert.FromBase64String(file);
            var data = BinaryData.FromBytes(fileArray);
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(data);
        }

        /// <summary>
        /// Upload file from string (base64)
        /// </summary>
        /// <param name="file">file as base64</param>
        /// <param name="fileName">the name to give on your file</param>     
        public void Upload(string file, string fileName)
        {
            byte[] fileArray = Convert.FromBase64String(file);
            var data = BinaryData.FromBytes(fileArray);
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            blobClient.Upload(data);
        }

        /// <summary>
        /// Upload file from string (base64)
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The real name of your file on Azure (guid)</returns>
        public string Upload(string file)
        {
            var guid = Guid.NewGuid();
            Upload(file, guid.ToString());
            return guid.ToString();
        }

        /// <summary>
        /// Upload file from string (base64)
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The real name of your file on Azure (guid)</returns>
        public async Task<string> UploadAsync(string file)
        {
            var guid = Guid.NewGuid();
            await UploadAsync(file, guid.ToString());
            return guid.ToString();
        }

        /// <summary>
        /// Download the file
        /// </summary>
        /// <param name="fileName">Name of file on azure</param>
        /// <returns>An array of byte</returns>
        public byte[] Download(string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            MemoryStream stream = new MemoryStream();
            blobClient.DownloadTo(stream);
            return stream.ToArray();
        }

        /// <summary>
        /// Download the file
        /// </summary>
        /// <param name="fileName">Name of file on azure</param>
        /// <returns>An array of byte</returns>
        public async Task<byte[]> DownloadAsync(string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            MemoryStream stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            return stream.ToArray();
        }
    }
}
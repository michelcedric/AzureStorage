using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace Azure
{
    /// <summary>
    /// Object to mnanage a azure storage
    /// </summary>
    public class AzureStorage
    {
        private AzureAuthentication _authentication = null;
        private CloudBlobClient _blobClient = null;

        /// <summary>
        /// Initialize a azure storage
        /// </summary>
        /// <param name="accountName">Account name of your storage</param>
        /// <param name="containerName">Container name of your storage</param>
        /// <param name="keyValue">Key of your storage</param>
        public AzureStorage(string accountName, string containerName, string keyValue)
        {
            this._authentication = new AzureAuthentication(accountName, containerName, keyValue);
            InitializeAzureConnection();
        }

        /// <summary>
        /// Initialize a azure storage 
        /// </summary>
        /// <param name="authentication">AzureAuthentication object</param>
        public AzureStorage(AzureAuthentication authentication)
        {
            this._authentication = authentication;
            InitializeAzureConnection();
        }

        /// <summary>
        /// Initialize the connection with the azure container
        /// </summary>
        private void InitializeAzureConnection()
        {
            var storageCredential = new StorageCredentials(this._authentication.AccountName, this._authentication.KeyValue);
            var storageAccount = new CloudStorageAccount(storageCredential, true);
            this._blobClient = storageAccount.CreateCloudBlobClient();

        }

        /// <summary>
        /// Upload file from stream to Azure
        /// </summary>
        /// <param name="file">Stream file</param>     
        /// <param name="fileName">the name to give on your file</param>    
        public void Upload(Stream file,string fileName)
        {
            var binary = new BinaryReader(file);
            var binData = binary.ReadBytes((int)file.Length);
            CloudBlobContainer container = this._blobClient.GetContainerReference(this._authentication.ContainerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.UploadFromByteArray(binData, 0, binData.Length);
          
        }

        /// <summary>
        /// Upload file from stream to Azure
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The real name of your file on Azure (guid)</returns>
        public string Upload(Stream file)
        {
            Guid guid = Guid.NewGuid();
            this.Upload(file, guid.ToString());
            return guid.ToString();
        }



        /// <summary>
        /// Upload file from string (base64)
        /// </summary>
        /// <param name="file">file as base64</param>
        /// <param name="fileName">the name to give on your file</param>     
        public void Upload(string file, string fileName)
        {
            CloudBlobContainer container = this._blobClient.GetContainerReference(this._authentication.ContainerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName.ToString());
            byte[] fileArray = Convert.FromBase64String(file);
            blockBlob.UploadFromByteArray(fileArray, 0, fileArray.Length);

        }

        /// <summary>
        /// Upload file from string (base64)
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The real name of your file on Azure (guid)</returns>
        public string Upload(string file)
        {
            Guid guid = Guid.NewGuid();
            this.Upload(file, guid.ToString());
            return guid.ToString();
        }


        /// <summary>
        /// Download the file
        /// </summary>
        /// <param name="fileName">name of file on azure</param>
        /// <returns>an array of byte</returns>
        public byte[] Download(string fileName)
        {
            CloudBlobContainer container = this._blobClient.GetContainerReference(this._authentication.ContainerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName.ToLower());
            blockBlob.FetchAttributes();
            long fileByteLength = blockBlob.Properties.Length;
            byte[] file = new byte[fileByteLength];
            blockBlob.DownloadToByteArray(file, 0);
            return file;
        }

    }


}
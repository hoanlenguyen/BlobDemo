using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.IO;

namespace BlobsDemo
{
    internal class Program
    {
        public static string CONN_STRING = "AZURE_STORAGE_CONNECTION_STRING";

        public static bool BlobExists(CloudBlockBlob blob)
        {
            try
            {
                blob.FetchAttributes();
                return true;
            }
            catch (StorageException)
            {
                return false;
            }
        }

        private static void Main(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable(CONN_STRING);
            connectionString.AsConnectionString()
                            .Get("AccountName", out var accountName)
                            .Get("AccountKey", out var key)
                            .Get("BlobEndpoint", out var domain);
            //Console.WriteLine(accountName);

            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                //var delegationkey = cloudBlobClient.GetUserDelegationKey(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));
                //Console.WriteLine("User delegation key properties:");
                //Console.WriteLine("Key signed start: {0}", delegationkey.SignedStart);
                //Console.WriteLine("Key signed expiry: {0}", delegationkey.SignedExpiry);
                //Console.WriteLine("Key signed object ID: {0}", delegationkey.SignedOid);
                //Console.WriteLine("Key signed tenant ID: {0}", delegationkey.SignedTid);
                //Console.WriteLine("Key signed service: {0}", delegationkey.SignedService);
                //Console.WriteLine("Key signed version: {0}", delegationkey.SignedVersion);

                string containerName = "test";
                string directory = "image";
                string subdirectory = DateTime.Now.ToString("yyyy-MM-dd");
                string blobName = "Restful API.docx";
                string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string localFileName = Path.Combine(localPath, blobName);
                CloudBlobContainer container = cloudBlobClient.GetContainerReference(containerName/* + Guid.NewGuid().ToString()*/);
                container.CreateIfNotExists();

                var blob = container.GetDirectoryReference(directory)
                                    .GetDirectoryReference(subdirectory)
                                    .GetBlockBlobReference(blobName);

                //blob.UploadFromFile(localFileName);
                //var url = blob.Uri.AbsoluteUri;

                //var blob = container.GetBlockBlobReference(blobName);
                if (BlobExists(blob))
                {
                    //var blobPermission = SharedAccessBlobPermissions.Read;
                    //TimeSpan clockSkew = TimeSpan.FromMinutes(15d);
                    //TimeSpan accessDuration = TimeSpan.FromMinutes(15d);
                    //var blobSAS = new SharedAccessBlobPolicy
                    //{
                    //    SharedAccessStartTime = DateTime.UtcNow.Subtract(clockSkew),
                    //    SharedAccessExpiryTime = DateTime.UtcNow.Add(accessDuration) + clockSkew,
                    //    Permissions = blobPermission
                    //};

                    //string sasBlobToken = blob.GetSharedAccessSignature(blobSAS);
                    //string url = string.Format("{0}{1}", blob.Uri.AbsoluteUri, sasBlobToken);
                    //Console.WriteLine(url);
                    var name = blob.Name;
                    Console.WriteLine(name);
                    var blobPage = new CloudPageBlob(blob.Uri);
                }
                else
                {
                    Console.WriteLine("Not found");
                }

                //BlobContainerPermissions containerPermissions = new BlobContainerPermissions
                //{
                //    PublicAccess = BlobContainerPublicAccessType.Off
                //};

                //cloudBlobContainer.SetPermissions(containerPermissions);

                //string localFileName = "teemo.jpg";
                //CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);

                //var blobPermission = SharedAccessBlobPermissions.Read;
                //TimeSpan clockSkew = TimeSpan.FromMinutes(15d);
                //TimeSpan accessDuration = TimeSpan.FromMinutes(15d);
                //var blobSAS = new SharedAccessBlobPolicy
                //{
                //    SharedAccessStartTime = DateTime.UtcNow.Subtract(clockSkew),
                //    SharedAccessExpiryTime = DateTime.UtcNow.Add(accessDuration) + clockSkew,
                //    Permissions = blobPermission
                //};

                //string sasBlobToken = cloudBlockBlob.GetSharedAccessSignature(blobSAS);

                ////Console.WriteLine(sasBlobToken);

                ////string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //////string localFileName = "QuickStart_" + Guid.NewGuid().ToString() + ".txt";

                ////string sourceFile = Path.Combine(localPath, localFileName);
                ////// Write text to the file.
                //////File.WriteAllText(sourceFile, "Hello, World!");

                ////Console.WriteLine("Temp file = {0}", sourceFile);
                ////Console.WriteLine("Uploading to Blob storage as blob '{0}'", localFileName);

                ////// Get a reference to the blob address, then upload the file to the blob.
                ////// Use the value of localFileName for the blob name.
                ////cloudBlockBlob.UploadFromFile(sourceFile);
                ////Console.WriteLine("Done upload!");

                ////string destinationFile = sourceFile.Replace(".docx", "_DOWNLOADED.docx");
                ////Console.WriteLine("Downloading blob to {0}", destinationFile);
                ////cloudBlockBlob.DownloadToFile(destinationFile, FileMode.Create);
                ////Console.WriteLine("Done download!");

                ////UriBuilder fullUri = new UriBuilder()
                ////{
                ////    Scheme = "https",
                ////    Host = /*string.Format("{0}/{1}",domain, accountName)*/"http://127.0.0.1:10000/devstoreaccount1/",
                ////    Path = string.Format("{0}/{1}", containerName, localFileName),
                ////    Query = sasBlobToken
                ////};

                //var url = cloudBlockBlob.Uri.AbsoluteUri + sasBlobToken;
                //Console.WriteLine(url);
            }
        }
    }
}
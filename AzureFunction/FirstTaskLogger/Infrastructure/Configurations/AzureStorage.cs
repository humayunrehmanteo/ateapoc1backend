using System;

namespace FirstTaskLogger.Infrastructure.Configurations
{
    internal class AzureStorage
    {
        private AzureStorage(string value) { Value = value; }

        public string Value { get; private set; }

        public static AzureStorage ConnectionString
        {
            get
            {
                var storageConnection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                return new AzureStorage(storageConnection);

            }
        }
        public static AzureStorage TableName
        {
            get
            {
                var tableName = Environment.GetEnvironmentVariable("StorageTableName");
                if (string.IsNullOrEmpty(tableName))
                    tableName = "firstTaskTable";
                return new AzureStorage(tableName);
            }
        }
        public static AzureStorage ContainerName {
            get
            {
                var blobName = Environment.GetEnvironmentVariable("StorageBlobName");
                if (string.IsNullOrEmpty(blobName))
                    blobName = "first-task-blob";
                return new AzureStorage("first-task-blob"); 
            } 
        }
        public override string ToString()
        {
            return Value;
        }
    }
}

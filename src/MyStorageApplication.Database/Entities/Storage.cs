namespace MyStorageApplication.Database.Entities
{
    public class Storage
    {
        public int StorageId { get; private set; }
        public string Identification { get; private set; } = string.Empty;

        public Storage() { }

        public Storage(string identification)
        {
            Identification = identification;
        }

        public void Update(int storageId, string identification)
        {
            StorageId = storageId;
            Identification = identification;
        }
    }
}

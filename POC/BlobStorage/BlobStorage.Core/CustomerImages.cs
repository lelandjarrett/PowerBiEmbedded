namespace BlobStorage.Core
{
    public  class CustomerImages
    {
            public int ID { get; set; }
            public int CustomerID { get; set; }
            public string FileName { get; set; }

        public static object SingleOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}

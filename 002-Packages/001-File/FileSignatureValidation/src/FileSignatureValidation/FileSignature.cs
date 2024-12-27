namespace FileSignatureValidation
{
    public class FileSignature
    {
        public string FileExtension { get; set; }

        public byte[] Signature { get; set; }

        public int Offset { get; set; } = 0;

        public int Skip { get; set; } = 0;

        public byte[] SecondSignature { get; set; }


        public FileSignature(string fileExtension, byte[] signature, int offset = 0, int skip = 0)
        {
            FileExtension = fileExtension;
            Signature = signature;
            Offset = offset;
            Skip = skip;
            SecondSignature = new byte[] { };
        }
        public FileSignature(string fileExtension, byte[] signature, int offset, int skip, byte[] secondSignature)
        {
            FileExtension = fileExtension;
            Signature = signature;
            Offset = offset;
            Skip = skip;
            SecondSignature = secondSignature;
        }
    }
}

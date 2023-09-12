using System.Security.Cryptography;

namespace GuidGeneratorSample.AbpGuids
{
    public enum SequentialGuidType 
    {
        SequentialAsString,
        SequentialAsBinary,
        SequentialAtEnd
    }


    public class SequentialGuidGenerator : IGuidGenerator
    {
        public SequentialGuidType Type { get; set; }
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

        public SequentialGuidGenerator()
        {
            Type = SequentialGuidType.SequentialAsString;
        }
        public SequentialGuidGenerator(SequentialGuidType type)
        {
            Type = type;
        }

        public Guid Create()
        {
            return Create(Type);
        }

        public Guid Create(SequentialGuidType guidType)
        {
            var randomBytes = new byte[10];
            RandomNumberGenerator.GetBytes(randomBytes);

            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }


            byte[] guidBytes = new byte[16];
            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }

                    break;

                case SequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }
    }
}

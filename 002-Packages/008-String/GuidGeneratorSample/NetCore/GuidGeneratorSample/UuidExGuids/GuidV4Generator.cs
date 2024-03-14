using UUIDNext.Generator;

namespace GuidGeneratorSample.UuidExGuids
{
    public class GuidV4Generator : IGuidGenerator
    {
        private static readonly UuidV4Generator _generator = new UuidV4Generator();
        public static GuidV4Generator Instance { get; } = new GuidV4Generator();

        public Guid Create()
        {
           return _generator.New();
        }
    }
}

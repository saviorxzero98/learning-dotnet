using UUIDNext.Generator;

namespace GuidGeneratorSample.UuidExGuids
{
    public class GuidV6Generator : IGuidGenerator
    {
        private static readonly UuidV6Generator _generator = new UuidV6Generator();
        public static GuidV6Generator Instance { get; } = new GuidV6Generator();

        public Guid Create()
        {
            return _generator.New();
        }
    }
}

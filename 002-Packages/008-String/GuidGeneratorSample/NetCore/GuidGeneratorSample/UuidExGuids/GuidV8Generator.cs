using UUIDNext.Generator;

namespace GuidGeneratorSample.UuidExGuids
{
    public class GuidV8Generator : IGuidGenerator
    {
        private static readonly UuidV8SqlServerGenerator _generator = new UuidV8SqlServerGenerator();
        public static GuidV8Generator Instance { get; } = new GuidV8Generator();

        public Guid Create()
        {
            return _generator.New();
        }
    }
}

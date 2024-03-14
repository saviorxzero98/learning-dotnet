using UUIDNext.Generator;

namespace GuidGeneratorSample.UuidExGuids
{
    public enum GuidNamespaceType
    {
        Dns,
        Url,
        Oid,
        X500
    }

    public class GuidV5Generator : IGuidGenerator
    {
        private static readonly UuidV5Generator _generator = new UuidV5Generator();
        private Guid Namespace { get; set; }
        private string Value { get; set; }

        public GuidV5Generator(string value = "") : this(GuidNamespaceType.Dns, value)
        {
        }
        public GuidV5Generator(GuidNamespaceType type, string value)
        {
            Namespace = GetNamespaceBase(type);
            Value = value;
        }


        public Guid Create()
        {
            return _generator.New(Namespace, Value);
        }
        public Guid Create(GuidNamespaceType type, string value)
        {
            var namespaceBase = GetNamespaceBase(type);
            return _generator.New(namespaceBase, value);
        }


        protected Guid GetNamespaceBase(GuidNamespaceType type)
        {
            switch (type)
            {
                case GuidNamespaceType.Url:
                    return GuidNamespaces.Url;

                case GuidNamespaceType.Oid: 
                    return GuidNamespaces.Oid;

                case GuidNamespaceType.X500:
                    return GuidNamespaces.X500;

                case GuidNamespaceType.Dns:
                default:
                    return GuidNamespaces.Dns;
            }
        }


        public static class GuidNamespaces
        {
            // 6ba7b810-9dad-11d1-80b4-00c04fd430c8
            public static readonly Guid Dns = new Guid(
                0x6ba7b810, 0x9dad, 0x11d1,
                0x80, 0xb4, 0x00, 0xc0, 0x4f, 0xd4, 0x30, 0xc8);

            // 6ba7b811-9dad-11d1-80b4-00c04fd430c8
            public static readonly Guid Url = new Guid(
                0x6ba7b811, 0x9dad, 0x11d1,
                0x80, 0xb4, 0x00, 0xc0, 0x4f, 0xd4, 0x30, 0xc8);

            // 6ba7b812-9dad-11d1-80b4-00c04fd430c8
            public static readonly Guid Oid = new Guid(
                0x6ba7b812, 0x9dad, 0x11d1,
                0x80, 0xb4, 0x00, 0xc0, 0x4f, 0xd4, 0x30, 0xc8);

            // 6ba7b814-9dad-11d1-80b4-00c04fd430c8
            public static readonly Guid X500 = new Guid(
                0x6ba7b814, 0x9dad, 0x11d1,
                0x80, 0xb4, 0x00, 0xc0, 0x4f, 0xd4, 0x30, 0xc8);
        }
    }
}

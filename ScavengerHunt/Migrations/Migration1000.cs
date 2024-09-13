using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace ScavengerHunt.Migrations;

public class Migration1000 : MigrationBase
{
    #region Classes

    public class Unit : AuditBase
    {
        [AutoIncrement]
        public long Id { get; set; }

        public UnitType Type { get; set; }
        public int Number { get; set; }

        [Reference]
        public List<Scouter> Scouters { get; set; } = [];
    }

    public class Scouter : AuditBase
    {
        [AutoIncrement]
        public long Id { get; set; }

        public Guid LinkingId { get; set; }
        public ScouterType ScouterType { get; set; }

        [Reference]
        public Unit Unit { get; set; } = default!;

        [Reference]
        public List<CacheFinding> Findings { get; set; } = [];

        [Ref(Model = nameof(Unit), RefId = nameof(Unit.Id), RefLabel = nameof(Unit.Number))]
        [References(typeof(Unit))]
        public long UnitId { get; set; }
    }

    public class Cache : AuditBase
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string LocationDescription { get; set; } = string.Empty;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int BasePoints { get; set; }
        public int BountyPoints { get; set; }
        public bool IsEnabled { get; set; }
        public Guid? QRCodeId { get; set; }

        [Reference]
        public List<CacheFinding> Findings { get; set; } = [];
    }

    public class CacheFinding : AuditBase
    {
        [AutoIncrement]
        public long Id { get; set; }

        [Ref(Model = nameof(Cache), RefId = nameof(Cache.Id), RefLabel = nameof(Cache.Name))]
        [References(typeof(Cache))]
        public long CacheId { get; set; }

        [Reference]
        Cache Cache { get; set; } = default!;

        [Ref(Model = nameof(Scouter), RefId = nameof(Scouter.Id), RefLabel = nameof(Scouter.LinkingId))]
        [References(typeof(Scouter))]
        public long ScouterId { get; set; }

        [Reference]
        Scouter Scouter { get; set; } = default!;

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int BasePoints { get; set; }
        public int BountyPoints { get; set; }
        public bool IsValidFind { get; set; }
    }

    #endregion

    #region Enums

    public enum UnitType
    {
        Pack,
        Troop,
        Crew,
        Ship
    }

    public enum ScouterType
    {
        Lion,
        Tiger,
        Wolf,
        Bear,
        WEBELOS,
        AOL,
        ScoutsBSAYouth,
        VenturingYouth,
        Adult
    }

    #endregion

    public override void Up()
    {
        Db.CreateTable<Unit>();
        Db.CreateTable<Scouter>();
        Db.CreateTable<Cache>();
        Db.CreateTable<CacheFinding>();
    }

    public override void Down()
    {
        Db.DropTable<CacheFinding>();
        Db.DropTable<Cache>();
        Db.DropTable<Scouter>();
        Db.DropTable<Unit>();
    }
}
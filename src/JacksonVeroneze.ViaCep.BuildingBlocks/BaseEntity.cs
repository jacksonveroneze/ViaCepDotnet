using System;

namespace JacksonVeroneze.ViaCep.BuildingBlocks
{
    public abstract class BaseEntity
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } = null;

        public DateTime? DeletedAt { get; set; } = null;

        public int Version { get; set; } = 1;

        //
        // Summary:
        //     /// Method responsible for initializing the entity. ///
        //
        public BaseEntity() => CreatedAt = DateTime.Now;

        //
        // Summary:
        //     /// Method responsible for updating the entity. ///
        //
        public void IncrementVersion() => Version++;

        //
        // Summary:
        //     /// Method responsible for returning a string
        //     representation of the object. ///
        //
        public override string ToString()
            => $"CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}, DeletedAt: {DeletedAt}, Version: {Version}";
    }
}

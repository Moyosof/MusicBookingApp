using System.ComponentModel.DataAnnotations;

namespace MusicBookingApp.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// The unique identifier for this entity.
        /// </summary>
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The date this entity was created.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The last time this entity was modified.
        /// </summary>
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
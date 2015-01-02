using Microsoft.Xna.Framework;

namespace XenScript
{
    public interface IEntity
    {
        /// <summary>
        /// Gets the Id of this Entity.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the Group Id of this Entity.
        /// </summary>
        int GroupId { get; }
    }
}

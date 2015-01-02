using Microsoft.Xna.Framework;

namespace XenGameBase
{
    /// <summary>
    /// Interface for an object that can process input (mouse/keyboard)
    /// </summary>
    public interface IAcceptsInput
    {
        /// <summary>
        /// Returns true if this object wants a chance to process input.  False otherwise.
        /// </summary>
        bool WantsInput { get; }

        /// <summary>
        /// Processes the input and updates the process flags if the input was consumed.  
        /// </summary>
        /// <param name="input">The input state</param>
        void ProcessInput( ref InputState input, Matrix transformFromCameraToWorld );
    }
}

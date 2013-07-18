//-----------------------------------------------------------------------
// <copyright file="KeyMapping.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Declares all of the key mappings for a given player. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Players.Actions
{
    using System.Windows.Forms;

    /// <summary>
    /// Defines the KeyMapping used in OpenMario.
    /// </summary>
    public class KeyMapping
    {
        /// <summary>
        /// Enumerates the different types of keys.
        /// </summary>
        public enum KeyAction
        {
            /// <summary>
            /// Move the actor to the left.
            /// </summary>
            LEFT,

            /// <summary>
            /// Move the actor "up" this is different from jump and would likely be used for air controls or swimming.
            /// </summary>
            UP,

            /// <summary>
            /// Move the actor to the right.
            /// </summary>
            RIGHT,

            /// <summary>
            /// Move the actor down. This would be down when swimming and duck when on solid land.
            /// </summary>
            DOWN,
            
            /// <summary>
            /// Have the actor "jump" into the air.
            /// </summary>
            JUMP,
            
            /// <summary>
            /// Have the actor "shoot."  This will be used for fireballs etc.
            /// </summary>
            SHOOT,

            /// <summary>
            /// Have the actor run.
            /// </summary>
            RUN,
        }

        /// <summary>
        /// Enumerates the different states of keys.
        /// </summary>
        public enum KeyPressType
        {
            /// <summary>
            /// Denotes the key is currently being pressed.
            /// </summary>
            DOWN,

            /// <summary>
            /// Denotes the key has been released.
            /// </summary>
            UP
        }

        /// <summary>
        /// Gets or sets the Keys that are being pressed.
        /// </summary>
        public Keys Key { get; set; }

        /// <summary>
        /// Gets or sets the Action that is being pressed.
        /// </summary>
        public KeyAction Action { get; set; }

        /// <summary>
        /// Gets or sets the PressType that is being pressed.
        /// </summary>
        public KeyPressType PressType { get; set; }

        /// <summary>
        /// Overrides the GetHashCode method.
        /// TODO: Figure out what this is used for as it's the only usage.
        /// </summary>
        /// <returns>The base.GetHashCode()</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Sets the comparator for this type.
        /// </summary>
        /// <param name="obj">The KeyMapping to compare</param>
        /// <returns>True if the action key is the same.</returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Action == ((KeyMapping)obj).Action;

                // && this.Key == ((KeyMapping)obj).Key
                // && this.PressType == ((KeyMapping)obj).PressType;
        }
    }
}

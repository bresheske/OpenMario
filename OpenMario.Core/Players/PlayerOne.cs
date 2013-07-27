//-----------------------------------------------------------------------
// <copyright file="PlayerOne.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Controls to allow player one to interact with Mario. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Players
{
    using System.Collections.Generic;

    /// <summary>
    /// The player one.
    /// </summary>
    public class PlayerOne : BasePlayer
    {
        /// <summary>
        /// The get key mappings.
        /// </summary>
        /// <returns>
        /// The <see cref="Actions.KeyMapping"/>.
        /// </returns>
        public override List<Actions.KeyMapping> GetKeyMappings()
        {
            return new List<Actions.KeyMapping>
                       {
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.UP,
                                   Key = System.Windows.Forms.Keys.Up,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.DOWN,
                               },
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.UP,
                                   Key = System.Windows.Forms.Keys.Up,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.UP,
                               },
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.DOWN,
                                   Key = System.Windows.Forms.Keys.Down,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.DOWN,
                               },
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.DOWN,
                                   Key = System.Windows.Forms.Keys.Down,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.UP,
                               },
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.RIGHT,
                                   Key = System.Windows.Forms.Keys.Right,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.DOWN,
                               },
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.RIGHT,
                                   Key = System.Windows.Forms.Keys.Right,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.UP,
                               },
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.LEFT,
                                   Key = System.Windows.Forms.Keys.Left,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.DOWN,
                               },
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.LEFT,
                                   Key = System.Windows.Forms.Keys.Left,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.UP,
                               },
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.JUMP,
                                   Key = System.Windows.Forms.Keys.Space,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.DOWN,
                               },
                           new Actions.KeyMapping
                               {
                                   Action = Actions.KeyMapping.KeyAction.JUMP,
                                   Key = System.Windows.Forms.Keys.Space,
                                   PressType =
                                       Actions.KeyMapping.KeyPressType.UP,
                               },
                       };
        }
    }
}
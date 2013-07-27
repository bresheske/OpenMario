//-----------------------------------------------------------------------
// <copyright file="BasePlayer.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Defines the base BasePlayer class. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Players
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using OpenMario.Core.Players.Actions;

    /// <summary>
    /// Defines the abstract BasePlayer class.
    /// </summary>
    public abstract class BasePlayer
    {
        /// <summary>
        /// List of <see cref="KeyMapping"/>
        /// </summary>
        private readonly List<KeyMapping> currentKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePlayer"/> class.
        /// </summary>
        protected BasePlayer()
        {
            this.currentKeys = new List<KeyMapping>();
        }

        /// <summary>
        /// Event handling for the KeyUp button press.
        /// </summary>
        public event EventHandler<Actions.KeyEventArgs> OnKeyUp;

        /// <summary>
        /// Event handling for the KeyDown button press.
        /// </summary>
        public event EventHandler<Actions.KeyEventArgs> OnKeyDown;

        /// <summary>
        /// Declares the abstract method for getting key mappings.
        /// </summary>
        /// <returns>List of <c>KeyMapping</c></returns>
        public abstract List<KeyMapping> GetKeyMappings();

        /// <summary>
        /// Method for determining if the current key contains an action.
        /// </summary>
        /// <param name="action"> The <see cref="KeyMapping"/> action pressed</param>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsActionPressed(KeyMapping action)
        {
            return this.currentKeys.Contains(action);
        }

        /// <summary>
        /// Registers key mappings for player input.
        /// Method handles the pressing and release of the various keys.
        /// </summary>
        /// <param name="form">Windows.Forms.Form - OpenMario</param>
        public void RegisterKeyMappings(Form form)
        {
            var mappings = this.GetKeyMappings();
            form.KeyDown += (o, e) =>
                {
                    var m = mappings.FirstOrDefault(x => x.Key == e.KeyCode && x.PressType == KeyMapping.KeyPressType.DOWN);
                    if (m != null && !this.currentKeys.Contains(m))
                    {
                        this.currentKeys.Add(m);
                        if (OnKeyDown != null)
                        {
                            OnKeyDown(this, new Actions.KeyEventArgs { KeyMapping = m });
                        }
                    }
                };
            form.KeyUp += (o, e) =>
            {
                var m = mappings.FirstOrDefault(x => x.Key == e.KeyCode && x.PressType == KeyMapping.KeyPressType.UP);
                if (m != null && this.currentKeys.Contains(m))
                {
                    this.currentKeys.Remove(m);
                    if (OnKeyUp != null)
                    {
                        OnKeyUp(this, new Actions.KeyEventArgs { KeyMapping = m });
                    }
                }
            };
        }
    }
}
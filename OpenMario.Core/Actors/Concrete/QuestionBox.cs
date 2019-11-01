//-----------------------------------------------------------------------
// <copyright file="QuestionBox.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Contains all of the logic for drawing and interacting with the Question Box </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using VectorClass;

    /// <summary>
    /// The question box.
    /// </summary>
    public class QuestionBox : StaticBox
    {
        /// <summary>
        /// The drawable object.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Bitmap drawable;
        private bool isActivated = false;
        private bool previouslyActivated = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionBox"/> class.
        /// </summary>
        public QuestionBox()
        {
            this.Width = 30;
            this.Height = 30;
        }

        /// <summary>
        /// The Load method for <see cref="QuestionBox"/>
        /// </summary>
        /// <param name="env">The env.</param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;

            Bitmap B = new Bitmap(this.Width, this.Height);
            for (int i = 0; i < B.Height; i++)
                for (int j = 0; j < B.Width; j++)
                    B.SetPixel(j, i, Color.Orange);
            this.drawable = B;
        }

        /// <summary>
        /// The drawing method for the <see cref="QuestionBox"/> actor.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> for the <see cref="QuestionBox"/> actor</param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(this.drawable, (int)pos.X, (int)pos.Y);
        }
        public void ActivateBox()
        {
            if (!this.previouslyActivated)
            {
                this.Environment.isBoxActivated = true;
                this.Environment.ActiveBox = this;
            }
        }
    }
}

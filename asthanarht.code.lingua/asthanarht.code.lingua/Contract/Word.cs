namespace asthanarht.code.lingua.Contract
{
    public class Word
    {
        /// <summary>
        /// Gets or sets the bounding box.
        /// </summary>
        /// <value>
        /// The bounding box.
        /// </value>
        public string BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        /// <value>
        /// The rectangle.
        /// </value>
        public Rectangle Rectangle
        {
            get
            {
                return Rectangle.FromString(this.BoundingBox);
            }
        }
    }
}
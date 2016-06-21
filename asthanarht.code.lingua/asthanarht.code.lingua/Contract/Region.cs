namespace asthanarht.code.lingua.Contract
{
    public class Region
    {
        /// <summary>
        /// Gets or sets the bounding box.
        /// </summary>
        /// <value>
        /// The bounding box.
        /// </value>
        public string BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the lines.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        public Line[] Lines { get; set; }

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
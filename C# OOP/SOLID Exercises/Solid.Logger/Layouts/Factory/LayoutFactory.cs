namespace Solid.Logger.Layouts.Factory
{
    using System;

    using Solid.Logger.Layouts.Contracts;
    using Solid.Logger.Layouts.Factory.Contracts;
    public class LayoutFactory : ILayoutFactory
    {
        public ILayout CreateLayout(string type)
        {
            string typeAsLowerCase = type.ToLower();
            switch (typeAsLowerCase)
            {
                case "simplelayout":
                    return new SimpleLayout();
                case "xmllayout":
                    return new XmlLayout();
                default:
                    throw new ArgumentException("Invalid layout type!");
            }
        }
    }
}

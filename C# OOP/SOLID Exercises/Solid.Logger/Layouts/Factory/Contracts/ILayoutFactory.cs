namespace Solid.Logger.Layouts.Factory.Contracts
{
    using Solid.Logger.Layouts.Contracts;
    public interface ILayoutFactory
    {
        ILayout CreateLayout(string type);
    }
}

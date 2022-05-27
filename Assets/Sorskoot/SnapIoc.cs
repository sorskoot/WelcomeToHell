namespace Sorskoot.Ioc
{
    public static class SnapIoc
    {
        public static readonly Container Container;

        static SnapIoc()
        {
            Container = new Container();
        }
    }
}
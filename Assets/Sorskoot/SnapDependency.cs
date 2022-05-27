namespace Sorskoot.Ioc
{
    public struct SnapDependency<TValue>
        where TValue : class
    {
        private TValue value;
        public TValue Value
        {
            get
            {
                return value ??= SnapIoc.Container.Resolve<TValue>();
            }
        }
    }
}
namespace OfficeProject.Servicess
{
    public interface IDataSerializer
    {
        void Serializer<T>(T dataSource, string componentName);
    }
}

namespace OfficeProject.Servicess
{
    public interface IWebDevelopmentService
    {
        Task<bool> DeleteWebServiceAsync(int webId);
    }
}

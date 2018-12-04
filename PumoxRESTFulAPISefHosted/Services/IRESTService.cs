using System.Net.Http;
using System.ServiceModel;


namespace RESTFulAPIConsole.Services
{
    [ServiceContract(Name = "RESTService")]
    public interface IRESTService
    {
        //[OperationContract]
        //[WebGet(UriTemplate = Routing.GetClientRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetClientNameById(string Id);

        //[OperationContract]
        //[WebGet(UriTemplate = Routing.GetCompanyRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        HttpResponseMessage GetAllCompany();
        
    }
}

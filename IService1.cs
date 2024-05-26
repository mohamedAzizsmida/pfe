using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace ProjetPFEService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "/Authentification?login={login}&password={password}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string Authentification(string login, string password);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "testsession",  BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string testsession();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "/inscription?login={login}&password={password}&Nom={Nom}&Prénom={Prénom}&Adresse={Adresse}&code_postal={code_postal}&ville={ville}" +"&pays={pays}&telephone={telephone}&Date_de_naissance={Date_de_naissance}",BodyStyle = WebMessageBodyStyle.Bare,     ResponseFormat = WebMessageFormat.Json)]
        string inscription(string Nom, string Prénom, string login, string password, string Adresse,string Code_postal,string ville,string pays, string telephone,string Date_de_naissance );

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "/mot_de_passe_oublié?login={login}&newPassword={newPassword}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string mot_de_passe_oublié(string login, string newPassword);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "/modifierclient?login={login}&password={password}&Nom={Nom}&Prénom={Prénom}&Adresse={Adresse}&code_postal={code_postal}&ville={ville}" + "&pays={pays}&telephone={telephone}&Date_de_naissance={Date_de_naissance}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string modifierclient(string Nom, string Prénom, string login, string password, string Adresse, string Code_postal, string ville, string pays, string telephone, string Date_de_naissance);
    
    [OperationContract]
    [WebInvoke(Method = "*", UriTemplate = "/selectclient", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
    string selectclient();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "/portefeuille?datePf={datePf}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string portefeuille(string datePf);//(string date);
    }

    }

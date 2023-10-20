using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Web.Script;
using Microsoft.VisualBasic;
using System.Net.Http;

using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

partial class dpdOrder // : System.Web.UI.Page
{
    public void setOrder()
    {
        HttpClient myClient = new HttpClient();
        myClient.BaseAddress = new Uri("https://cloud-stage.dpd.com/");
        myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        {
            var withBlock = myClient.DefaultRequestHeaders;
            withBlock.Add("Version", "100");
            withBlock.Add("Language", "de_DE");
            withBlock.Add("PartnerCredentials-Name", "DPD Sandbox");
            withBlock.Add("PartnerCredentials-Token", "06445364853584D75564");
            withBlock.Add("UserCredentials-cloudUserID", "2784301");
            withBlock.Add("UserCredentials-Token", "41453373646A726C4F34");
        }

        setOrderRequestType setOrderRequest = new setOrderRequestType();
        {
            var withBlock = setOrderRequest;
            withBlock.OrderAction = "startOrder";
            withBlock.OrderSettings = new OrderSettingsType();
            withBlock.OrderSettings.LabelSize = "PDF_A4";
            withBlock.OrderSettings.LabelStartPosition = "UpperLeft";
            withBlock.OrderSettings.ShipDate = new DateTime(2023,10,23);

            OrderDataType myOrder = new OrderDataType();
            myOrder.ParcelShopID = 0;

            myOrder.ShipAddress = new AddressType();
            {
                var withBlock1 = myOrder.ShipAddress;
                withBlock1.Company = "Mustermann AG";
                withBlock1.Gender = "male";
                withBlock1.Salutation = "Mr.";
                withBlock1.FirstName = "Max";
                withBlock1.LastName = "Mustermann";
                withBlock1.Name = "Max Mustermann";
                withBlock1.Street = "Wailandtstr.";
                withBlock1.HouseNo = "1";
                withBlock1.ZipCode = "63741";
                withBlock1.City = "Aschaffenburg";
                withBlock1.Country = "DEU";
                withBlock1.State = "";
                withBlock1.Phone = "+49 6021 123 456";
                withBlock1.Mail = "m.mustermann@mustermann.com";
            }

            myOrder.ParcelData = new ParcelDataType();
            {
                var withBlock1 = myOrder.ParcelData;
                withBlock1.Reference1 = "Customer email";
                withBlock1.Content = "Order number";
                withBlock1.Weight = (decimal)13.5;
                withBlock1.YourInternalID = "123";
                withBlock1.ShipService = "Classic";
            }

            withBlock.OrderDataList = new List<OrderDataType>();
            withBlock.OrderDataList.Add(myOrder);
        }

        //var myResult = myClient.PostAsJsonAsync("api/v1/setOrder", setOrderRequest).Result;
        var myResult = myClient.PostAsync("api/v1/setOrder", new StringContent(new JavaScriptSerializer().Serialize(setOrderRequest), Encoding.UTF8, "application/json")).Result;
        //System.Web.UI.Page.Response.Write(myResult.Content.ReadAsStringAsync.Result);
        String sResult = myResult.Content.ReadAsStringAsync().Result;
        Console.WriteLine("Result: " + sResult);
        System.Diagnostics.Debug.WriteLine("Result: " + sResult);
    }

    public class setOrderRequestType
    {
        public int Version { get; set; } = 0;
        public string Language { get; set; } = "";
        //public PartnerCredentialType PartnerCredentials { get; set; } = new PartnerCredentialType();
        //public UserCredentialType UserCredentials { get; set; } = new UserCredentialType();

        public string OrderAction { get; set; } = "";
        public OrderSettingsType OrderSettings { get; set; } = new OrderSettingsType();
        public List<OrderDataType> OrderDataList { get; set; } = new List<OrderDataType>();
    }

    public class OrderSettingsType
    {
        public DateTime ShipDate { get; set; } = new DateTime(2023, 10, 23);// (DateTime)"20.10.2023";
        public string LabelSize { get; set; } = "";
        public string LabelStartPosition { get; set; } = "";
    }

    public class OrderDataType
    {
        public AddressType ShipAddress { get; set; } = new AddressType();
        public int ParcelShopID { get; set; } = 0;
        public ParcelDataType ParcelData { get; set; } = new ParcelDataType();
    }

    public class AddressType
    {
        public string Company { get; set; } = "";
        public string Salutation { get; set; } = "";
        public string FirstName { get; set; } = ""; 
        public string LastName { get; set; } = "";
        public string Name { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Street { get; set; } = "";
        public string HouseNo { get; set; } = "";
        public string Country { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Mail { get; set; } = "";
    }

    public class ParcelDataType
    {
        public string ShipService { get; set; } = "";
        public decimal Weight { get; set; } = 0;
        public string Content { get; set; } = "";
        public string YourInternalID { get; set; } = "";
        public string Reference1 { get; set; } = "";
        public string Reference2 { get; set; } = "";
        public CODType COD { get; set; } = new CODType();
    }

    public class CODType
    {
        public string Purpose { get; set; } = "";
        public decimal Amount { get; set; } = 0;
        public string Payment { get; set; } = "";
    }
}

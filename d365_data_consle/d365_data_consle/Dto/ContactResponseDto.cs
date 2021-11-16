using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d365_data_consle.Dto
{
    public class LoginResponseDto
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("ext_expires_in")]
        public int ExtExpiresIn { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Value
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("customertypecode")]
        public int Customertypecode { get; set; }

        [JsonProperty("address2_addresstypecode")]
        public int Address2Addresstypecode { get; set; }

        [JsonProperty("merged")]
        public bool Merged { get; set; }

        [JsonProperty("territorycode")]
        public int Territorycode { get; set; }

        [JsonProperty("emailaddress1")]
        public string Emailaddress1 { get; set; }

        [JsonProperty("haschildrencode")]
        public int Haschildrencode { get; set; }

        [JsonProperty("exchangerate")]
        public double Exchangerate { get; set; }

        [JsonProperty("preferredappointmenttimecode")]
        public int Preferredappointmenttimecode { get; set; }

        [JsonProperty("isbackofficecustomer")]
        public bool Isbackofficecustomer { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime Modifiedon { get; set; }

        [JsonProperty("_owninguser_value")]
        public string OwninguserValue { get; set; }

        [JsonProperty("address1_composite")]
        public string Address1Composite { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("marketingonly")]
        public bool Marketingonly { get; set; }

        [JsonProperty("donotphone")]
        public bool Donotphone { get; set; }

        [JsonProperty("preferredcontactmethodcode")]
        public int Preferredcontactmethodcode { get; set; }

        [JsonProperty("educationcode")]
        public int Educationcode { get; set; }

        [JsonProperty("_ownerid_value")]
        public string OwneridValue { get; set; }

        [JsonProperty("customersizecode")]
        public int Customersizecode { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("donotpostalmail")]
        public bool Donotpostalmail { get; set; }

        [JsonProperty("yomifullname")]
        public string Yomifullname { get; set; }

        [JsonProperty("donotemail")]
        public bool Donotemail { get; set; }

        [JsonProperty("address2_shippingmethodcode")]
        public int Address2Shippingmethodcode { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("address1_addressid")]
        public string Address1Addressid { get; set; }

        [JsonProperty("address2_freighttermscode")]
        public int Address2Freighttermscode { get; set; }

        [JsonProperty("statuscode")]
        public int Statuscode { get; set; }

        [JsonProperty("createdon")]
        public DateTime Createdon { get; set; }

        [JsonProperty("address1_stateorprovince")]
        public string Address1Stateorprovince { get; set; }

        [JsonProperty("donotsendmm")]
        public bool Donotsendmm { get; set; }

        [JsonProperty("donotfax")]
        public bool Donotfax { get; set; }

        [JsonProperty("leadsourcecode")]
        public int Leadsourcecode { get; set; }

        [JsonProperty("address1_country")]
        public string Address1Country { get; set; }

        [JsonProperty("versionnumber")]
        public int Versionnumber { get; set; }

        [JsonProperty("creditonhold")]
        public bool Creditonhold { get; set; }

        [JsonProperty("_transactioncurrencyid_value")]
        public string TransactioncurrencyidValue { get; set; }

        [JsonProperty("address3_addressid")]
        public string Address3Addressid { get; set; }

        [JsonProperty("donotbulkemail")]
        public bool Donotbulkemail { get; set; }

        [JsonProperty("_modifiedby_value")]
        public string ModifiedbyValue { get; set; }

        [JsonProperty("followemail")]
        public bool Followemail { get; set; }

        [JsonProperty("shippingmethodcode")]
        public int Shippingmethodcode { get; set; }

        [JsonProperty("_createdby_value")]
        public string CreatedbyValue { get; set; }

        [JsonProperty("address1_name")]
        public string Address1Name { get; set; }

        [JsonProperty("address1_city")]
        public string Address1City { get; set; }

        [JsonProperty("donotbulkpostalmail")]
        public bool Donotbulkpostalmail { get; set; }

        [JsonProperty("_parentcustomerid_value")]
        public string ParentcustomeridValue { get; set; }

        [JsonProperty("contactid")]
        public string Contactid { get; set; }

        [JsonProperty("participatesinworkflow")]
        public bool Participatesinworkflow { get; set; }

        [JsonProperty("statecode")]
        public int Statecode { get; set; }

        [JsonProperty("_owningbusinessunit_value")]
        public string OwningbusinessunitValue { get; set; }

        [JsonProperty("address2_addressid")]
        public string Address2Addressid { get; set; }

        [JsonProperty("address1_postalcode")]
        public string Address1Postalcode { get; set; }

        [JsonProperty("telephone3")]
        public object Telephone3 { get; set; }

        [JsonProperty("address1_shippingmethodcode")]
        public object Address1Shippingmethodcode { get; set; }

        [JsonProperty("familystatuscode")]
        public object Familystatuscode { get; set; }

        [JsonProperty("nickname")]
        public object Nickname { get; set; }

        [JsonProperty("address1_freighttermscode")]
        public object Address1Freighttermscode { get; set; }

        [JsonProperty("address3_upszone")]
        public object Address3Upszone { get; set; }

        [JsonProperty("annualincome_base")]
        public object AnnualincomeBase { get; set; }

        [JsonProperty("anniversary")]
        public object Anniversary { get; set; }

        [JsonProperty("address1_upszone")]
        public object Address1Upszone { get; set; }

        [JsonProperty("websiteurl")]
        public object Websiteurl { get; set; }

        [JsonProperty("address2_city")]
        public object Address2City { get; set; }

        [JsonProperty("_slainvokedid_value")]
        public object SlainvokedidValue { get; set; }

        [JsonProperty("address1_postofficebox")]
        public object Address1Postofficebox { get; set; }

        [JsonProperty("importsequencenumber")]
        public object Importsequencenumber { get; set; }

        [JsonProperty("address3_longitude")]
        public object Address3Longitude { get; set; }

        [JsonProperty("preferredappointmentdaycode")]
        public object Preferredappointmentdaycode { get; set; }

        [JsonProperty("utcconversiontimezonecode")]
        public object Utcconversiontimezonecode { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object Overriddencreatedon { get; set; }

        [JsonProperty("aging90")]
        public object Aging90 { get; set; }

        [JsonProperty("stageid")]
        public object Stageid { get; set; }

        [JsonProperty("address3_primarycontactname")]
        public object Address3Primarycontactname { get; set; }

        [JsonProperty("address1_utcoffset")]
        public object Address1Utcoffset { get; set; }

        [JsonProperty("address1_latitude")]
        public object Address1Latitude { get; set; }

        [JsonProperty("home2")]
        public object Home2 { get; set; }

        [JsonProperty("yomifirstname")]
        public object Yomifirstname { get; set; }

        [JsonProperty("_cr993_autoid_value")]
        public string Cr993AutoidValue { get; set; }

        [JsonProperty("_masterid_value")]
        public object MasteridValue { get; set; }

        [JsonProperty("address3_shippingmethodcode")]
        public object Address3Shippingmethodcode { get; set; }

        [JsonProperty("lastonholdtime")]
        public object Lastonholdtime { get; set; }

        [JsonProperty("address2_fax")]
        public object Address2Fax { get; set; }

        [JsonProperty("address3_stateorprovince")]
        public object Address3Stateorprovince { get; set; }

        [JsonProperty("address3_telephone3")]
        public object Address3Telephone3 { get; set; }

        [JsonProperty("address3_telephone2")]
        public object Address3Telephone2 { get; set; }

        [JsonProperty("address3_telephone1")]
        public object Address3Telephone1 { get; set; }

        [JsonProperty("governmentid")]
        public object Governmentid { get; set; }

        [JsonProperty("address2_line1")]
        public object Address2Line1 { get; set; }

        [JsonProperty("address1_telephone3")]
        public object Address1Telephone3 { get; set; }

        [JsonProperty("address1_telephone2")]
        public object Address1Telephone2 { get; set; }

        [JsonProperty("address1_telephone1")]
        public object Address1Telephone1 { get; set; }

        [JsonProperty("address2_postofficebox")]
        public object Address2Postofficebox { get; set; }

        [JsonProperty("ftpsiteurl")]
        public object Ftpsiteurl { get; set; }

        [JsonProperty("emailaddress2")]
        public object Emailaddress2 { get; set; }

        [JsonProperty("address2_latitude")]
        public object Address2Latitude { get; set; }

        [JsonProperty("processid")]
        public string Processid { get; set; }

        [JsonProperty("emailaddress3")]
        public object Emailaddress3 { get; set; }

        [JsonProperty("address2_composite")]
        public object Address2Composite { get; set; }

        [JsonProperty("traversedpath")]
        public object Traversedpath { get; set; }

        [JsonProperty("spousesname")]
        public object Spousesname { get; set; }

        [JsonProperty("address3_name")]
        public object Address3Name { get; set; }

        [JsonProperty("address3_postofficebox")]
        public object Address3Postofficebox { get; set; }

        [JsonProperty("address2_line2")]
        public object Address2Line2 { get; set; }

        [JsonProperty("aging30_base")]
        public object Aging30Base { get; set; }

        [JsonProperty("address1_addresstypecode")]
        public object Address1Addresstypecode { get; set; }

        [JsonProperty("managerphone")]
        public object Managerphone { get; set; }

        [JsonProperty("address2_stateorprovince")]
        public object Address2Stateorprovince { get; set; }

        [JsonProperty("address2_postalcode")]
        public object Address2Postalcode { get; set; }

        [JsonProperty("entityimage_url")]
        public object EntityimageUrl { get; set; }

        [JsonProperty("aging60")]
        public object Aging60 { get; set; }

        [JsonProperty("managername")]
        public object Managername { get; set; }

        [JsonProperty("address3_postalcode")]
        public object Address3Postalcode { get; set; }

        [JsonProperty("jobtitle")]
        public object Jobtitle { get; set; }

        [JsonProperty("timezoneruleversionnumber")]
        public object Timezoneruleversionnumber { get; set; }

        [JsonProperty("address3_utcoffset")]
        public object Address3Utcoffset { get; set; }

        [JsonProperty("address2_telephone3")]
        public object Address2Telephone3 { get; set; }

        [JsonProperty("address2_telephone2")]
        public object Address2Telephone2 { get; set; }

        [JsonProperty("address2_telephone1")]
        public object Address2Telephone1 { get; set; }

        [JsonProperty("numberofchildren")]
        public object Numberofchildren { get; set; }

        [JsonProperty("address2_upszone")]
        public object Address2Upszone { get; set; }

        [JsonProperty("_owningteam_value")]
        public object OwningteamValue { get; set; }

        [JsonProperty("address2_line3")]
        public object Address2Line3 { get; set; }

        [JsonProperty("timespentbymeonemailandmeetings")]
        public object Timespentbymeonemailandmeetings { get; set; }

        [JsonProperty("department")]
        public object Department { get; set; }

        [JsonProperty("address2_longitude")]
        public object Address2Longitude { get; set; }

        [JsonProperty("suffix")]
        public object Suffix { get; set; }

        [JsonProperty("_modifiedonbehalfby_value")]
        public object ModifiedonbehalfbyValue { get; set; }

        [JsonProperty("creditlimit")]
        public object Creditlimit { get; set; }

        [JsonProperty("address1_line2")]
        public object Address1Line2 { get; set; }

        [JsonProperty("cr993_memberid")]
        public int? Cr993Memberid { get; set; }

        [JsonProperty("paymenttermscode")]
        public object Paymenttermscode { get; set; }

        [JsonProperty("address1_county")]
        public object Address1County { get; set; }

        [JsonProperty("_preferredsystemuserid_value")]
        public object PreferredsystemuseridValue { get; set; }

        [JsonProperty("accountrolecode")]
        public object Accountrolecode { get; set; }

        [JsonProperty("assistantname")]
        public object Assistantname { get; set; }

        [JsonProperty("address1_fax")]
        public object Address1Fax { get; set; }

        [JsonProperty("_createdonbehalfby_value")]
        public object CreatedonbehalfbyValue { get; set; }

        [JsonProperty("annualincome")]
        public object Annualincome { get; set; }

        [JsonProperty("_accountid_value")]
        public object AccountidValue { get; set; }

        [JsonProperty("address2_name")]
        public object Address2Name { get; set; }

        [JsonProperty("creditlimit_base")]
        public object CreditlimitBase { get; set; }

        [JsonProperty("_modifiedbyexternalparty_value")]
        public object ModifiedbyexternalpartyValue { get; set; }

        [JsonProperty("address2_utcoffset")]
        public object Address2Utcoffset { get; set; }

        [JsonProperty("business2")]
        public object Business2 { get; set; }

        [JsonProperty("address3_composite")]
        public object Address3Composite { get; set; }

        [JsonProperty("_slaid_value")]
        public object SlaidValue { get; set; }

        [JsonProperty("fax")]
        public object Fax { get; set; }

        [JsonProperty("address1_line1")]
        public object Address1Line1 { get; set; }

        [JsonProperty("childrensnames")]
        public object Childrensnames { get; set; }

        [JsonProperty("address2_county")]
        public object Address2County { get; set; }

        [JsonProperty("address3_city")]
        public object Address3City { get; set; }

        [JsonProperty("aging30")]
        public object Aging30 { get; set; }

        [JsonProperty("externaluseridentifier")]
        public object Externaluseridentifier { get; set; }

        [JsonProperty("address1_line3")]
        public object Address1Line3 { get; set; }

        [JsonProperty("_parentcontactid_value")]
        public object ParentcontactidValue { get; set; }

        [JsonProperty("assistantphone")]
        public object Assistantphone { get; set; }

        [JsonProperty("birthdate")]
        public object Birthdate { get; set; }

        [JsonProperty("address3_addresstypecode")]
        public object Address3Addresstypecode { get; set; }

        [JsonProperty("onholdtime")]
        public object Onholdtime { get; set; }

        [JsonProperty("_createdbyexternalparty_value")]
        public object CreatedbyexternalpartyValue { get; set; }

        [JsonProperty("entityimage_timestamp")]
        public object EntityimageTimestamp { get; set; }

        [JsonProperty("mobilephone")]
        public object Mobilephone { get; set; }

        [JsonProperty("address3_county")]
        public object Address3County { get; set; }

        [JsonProperty("employeeid")]
        public object Employeeid { get; set; }

        [JsonProperty("subscriptionid")]
        public object Subscriptionid { get; set; }

        [JsonProperty("entityimageid")]
        public object Entityimageid { get; set; }

        [JsonProperty("company")]
        public object Company { get; set; }

        [JsonProperty("gendercode")]
        public object Gendercode { get; set; }

        [JsonProperty("callback")]
        public object Callback { get; set; }

        [JsonProperty("lastusedincampaign")]
        public object Lastusedincampaign { get; set; }

        [JsonProperty("address3_line3")]
        public object Address3Line3 { get; set; }

        [JsonProperty("telephone2")]
        public object Telephone2 { get; set; }

        [JsonProperty("address3_freighttermscode")]
        public object Address3Freighttermscode { get; set; }

        [JsonProperty("yomilastname")]
        public object Yomilastname { get; set; }

        [JsonProperty("address3_fax")]
        public object Address3Fax { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("address3_line1")]
        public object Address3Line1 { get; set; }

        [JsonProperty("address3_line2")]
        public object Address3Line2 { get; set; }

        [JsonProperty("yomimiddlename")]
        public object Yomimiddlename { get; set; }

        [JsonProperty("aging90_base")]
        public object Aging90Base { get; set; }

        [JsonProperty("telephone1")]
        public object Telephone1 { get; set; }

        [JsonProperty("address1_primarycontactname")]
        public object Address1Primarycontactname { get; set; }

        [JsonProperty("address1_longitude")]
        public object Address1Longitude { get; set; }

        [JsonProperty("middlename")]
        public object Middlename { get; set; }

        [JsonProperty("address2_primarycontactname")]
        public object Address2Primarycontactname { get; set; }

        [JsonProperty("entityimage")]
        public object Entityimage { get; set; }

        [JsonProperty("address3_latitude")]
        public object Address3Latitude { get; set; }

        [JsonProperty("salutation")]
        public object Salutation { get; set; }

        [JsonProperty("aging60_base")]
        public object Aging60Base { get; set; }

        [JsonProperty("pager")]
        public object Pager { get; set; }

        [JsonProperty("address2_country")]
        public object Address2Country { get; set; }

        [JsonProperty("address3_country")]
        public object Address3Country { get; set; }
    }

    public class ContactResponseDto
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }

        [JsonProperty("value")]
        public List<Value> Value { get; set; }
    }


}

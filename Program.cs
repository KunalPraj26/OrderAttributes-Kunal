using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace OrderValuesInAttributes
{
    class Program
    {
        //public static void test()
        //{
        //    string teste = "cn=UserRoles,cn=SAP-ECC,cn=DriverSet,ou=IDM,ou=services,o=vale#1#<?xml version=1.0 encoding=UTF - 8?>  <ref>  <src>BULKResource</src>  <id/>  <param>Z:MM016_BR_1001_V1</param>  </ref>";
        //    //string result = string.Empty;

        //    teste = teste.Replace(">  <", "><");
        //    teste = teste.Replace("> <", "><");

        //}
        public enum SQLInputMethod { Bulk, Insert };
        static void Main(string[] args)
        {
            //test();

            Console.WriteLine("Start process at " + DateTime.Now.ToString("h:mm:ss tt") + "\n");

            List<String> tables = new List<string>();
            List<String> attributes = new List<string>();
            List<String> destTables = new List<string>();

            String tableType = args.GetValue(0).ToString();
            String operation = args.GetValue(1).ToString();
            String dbPlace = args.GetValue(2).ToString();
 

            ////String tableType = "ValeCity";
            ////String operation = "Sort";
            ////String dbPlace = "remote";
            ////string destinationType = "test";

            ////EntitlementRef Split local test

            switch (tableType)
            {
                //case "Applications_ValeDriverDN":

                //    tables.Add("Applications_Old");
                //    tables.Add("Applications_New");

                //    destTables.Add("Splited_Applications_ValeDriverDN_OLD");
                //    destTables.Add("Splited_Applications_ValeDriverDN_NEW");

                //    attributes.Add("ValeDriverDN");
                //    break;

                case "Users_Container":

                    tables.Add("STG_Users_OLD");
                    tables.Add("STG_Users_NEW");

                    destTables.Add("Users_OLD");
                    destTables.Add("Users_NEW");

                    attributes.Add("DN");
                    break;

                //case "Applications_DirXML-Associations":

                //    tables.Add("Applications_Old");
                //    tables.Add("Applications_New");

                //    destTables.Add("Splited_Applications_DirXML_Associations_OLD");
                //    destTables.Add("Splited_Applications_DirXML_Associations_NEW");


                //    attributes.Add("DirXML-Associations");
                //    break;

                //case "Role":

                //    tables.Add("nrfRole_OLD_PRD");
                //    tables.Add("nrfRole_NEW_PRD");

                //    attributes.Add("EquivalentToMe");
                //    attributes.Add("DirXML_Associations_Equalized");
                //    attributes.Add("ValeParentRoles_Equalized");
                //    attributes.Add("ValeChildRoles_Equalized");
                //    break;

                //case "Roles_DirXML-Associations":

                //    tables.Add("nrfRole_OLD_PRD");
                //    tables.Add("nrfRole_NEW_PRD");

                //    destTables.Add("Splited_Roles_DirXML_Associations_OLD");
                //    destTables.Add("Splited_Roles_DirXML_Associations_NEW");

                //    attributes.Add("DirXML-Associations");
                //    break;

                //case "Roles_EquivalentToMe":

                //    tables.Add("nrfRole_OLD_PRD");
                //    tables.Add("nrfRole_NEW_PRD");

                //    destTables.Add("Splited_Roles_EquivalentToMe_OLD");
                //    destTables.Add("Splited_Roles_EquivalentToMe_NEW");

                //    attributes.Add("EquivalentToMe");
                //    break;

                //case "Roles_ValeChildRoles":

                //    tables.Add("nrfRole_OLD_PRD");
                //    tables.Add("nrfRole_NEW_PRD");

                //    destTables.Add("Splited_Roles_ValeChildRoles_OLD");
                //    destTables.Add("Splited_Roles_ValeChildRoles_NEW");

                //    attributes.Add("ValeChildRoles");
                //    break;

                //case "ResourcesAssociation_DirXML-Associations":

                //    tables.Add("nrfResourceAssociations_OLD_PRD");
                //    tables.Add("nrfResourceAssociations_NEW_PRD");

                //    destTables.Add("Splited_ResourcesAssociation_DirXML_Associations_OLD");
                //    destTables.Add("Splited_ResourcesAssociation_DirXML_Associations_NEW");

                //    attributes.Add("DirXML-Associations");
                //    break;

                //case "ResourcesAssociation_nrfResource":

                //    tables.Add("nrfResourceAssociations_OLD_PRD");
                //    tables.Add("nrfResourceAssociations_NEW_PRD");

                //    destTables.Add("Splited_ResourcesAssociation_nrfResource_OLD");
                //    destTables.Add("Splited_ResourcesAssociation_nrfResource_NEW");

                //    attributes.Add("nrfResource");
                //    break;

                //case "ResourcesAssociation_nrfRole":

                //    tables.Add("nrfResourceAssociations_OLD_PRD");
                //    tables.Add("nrfResourceAssociations_NEW_PRD");

                //    destTables.Add("Splited_ResourcesAssociation_nrfRole_OLD");
                //    destTables.Add("Splited_ResourcesAssociation_nrfRole_NEW");

                //    attributes.Add("nrfRole");
                //    break;

                //case "Resources":

                //    tables.Add("nrfResources_OLD_PRD");
                //    tables.Add("nrfResources_NEW_PRD");

                //    attributes.Add("DirXML_Associations_Equalized");
                //    break;

                //case "Resources_DirXML-Associations":

                //    tables.Add("nrfResources_OLD_PRD");
                //    tables.Add("nrfResources_NEW_PRD");

                //    destTables.Add("Splited_Resources_DirXML_Associations_OLD");
                //    destTables.Add("Splited_Resources_DirXML_Associations_NEW");

                //    attributes.Add("DirXML-Associations");
                //    break;

                //case "Resources_nrfEntitlementRef":

                //    tables.Add("nrfResources_OLD_PRD");
                //    tables.Add("nrfResources_NEW_PRD");

                //    destTables.Add("Splited_Resources_nrfEntitlementRef_OLD");
                //    destTables.Add("Splited_Resources_nrfEntitlementRef_NEW");

                //    attributes.Add("nrfEntitlementRef");
                //    break;


                //case "User_nrfMemberOf":

                //    tables.Add("Users_MemberOf_IDV_OLD");
                //    tables.Add("Users_MemberOf_IDV_NEW");

                //    destTables.Add("Splited_Users_nrfMemberOf_OLD");
                //    destTables.Add("Splited_Users_nrfMemberOf_NEW");

                //    attributes.Add("nrfMemberOf");
                //    break;

                //case "UsersAttributes":

                //    tables.Add("Users_Attributes_IDV_NEW");
                //    tables.Add("Users_Attributes_IDV_OLD");

                //    attributes.Add("ValeLoginDisabled_Equalized");
                //    attributes.Add("ValeLastLogonDate_Equalized");
                //    break;

                //case "AssignedResources":

                //    //tables.Add("Users_Resources_IDV_OLD");
                //    //tables.Add("Users_Resources_IDV_NEW");

                //    destTables.Add("Splited_Users_Resources_IDV_OLD");
                //    destTables.Add("Splited_Users_Resources_IDV_NEW");

                //    attributes.Add("nrfAssignedResources");




                //    break;

                //case "EntitlementRef":

                //    tables.Add("Users_EntitlementRef_OLD");
                //    tables.Add("Users_EntitlementRef_NEW");

                //    destTables.Add("Splited_Users_EntitlementRef_OLD");
                //    destTables.Add("Splited_Users_EntitlementRef_NEW");

                //    attributes.Add("EntitlementRef");
                //    break;

                //case "groupMembership":

                //    tables.Add("Users_groupMembership_OLD");
                //    tables.Add("Users_groupMembership_NEW");

                //    destTables.Add("Splited_Users_groupMembership_OLD");
                //    destTables.Add("Splited_Users_groupMembership_NEW");

                //    attributes.Add("groupMembership");
                //    break;

                //case "users_securityEquals":

                //    tables.Add("Users_securityEquals_OLD");
                //    tables.Add("Users_securityEquals_NEW");

                //    destTables.Add("Splited_Users_securityEquals_OLD");
                //    destTables.Add("Splited_Users_securityEquals_NEW");

                //    attributes.Add("securityEquals");
                //    break;

                //case "DirXML-Associations":

                //    tables.Add("Users_DirXML_Associations_OLD");
                //    tables.Add("Users_DirXML_Associations_NEW");

                //    destTables.Add("Splited_Users_DirXML_Associations_OLD");
                //    destTables.Add("Splited_Users_DirXML_Associations_NEW");

                //    attributes.Add("DirXML-Associations");
                //    break;


                //case "DirXML-Associations_NEW_IDV_AUTH":

                //    tables.Add("Users_DirXML_Associations_NEW");
                //    tables.Add("Users_DirXML_Associations_NEW_AUTH");

                //    destTables.Add("Splited_Users_DirXML_Associations_NEW");
                //    destTables.Add("Splited_Users_DirXML_Associations_NEW_AUTH");

                //    attributes.Add("DirXML-Associations");
                //    break;

                case "Users_nrfAssignedRoles":

                    tables.Add("STG_Users_Roles_IDV_OLD");
                    tables.Add("STG_Users_Roles_IDV_NEW");

                    destTables.Add("Splited_Users_Roles_IDV_OLD");
                    destTables.Add("Splited_Users_Roles_IDV_NEW");

                    attributes.Add("nrfAssignedRoles");
                    break;

                //case "Users_nrfResourceHistory":

                //    tables.Add("Users_ResourceHistory_IDV_OLD");
                //    tables.Add("Users_ResourceHistory_IDV_NEW");

                //    destTables.Add("Splited_Users_ResourceHistory_IDV_OLD");
                //    destTables.Add("Splited_Users_ResourceHistory_IDV_NEW");

                //    attributes.Add("nrfResourceHistory");
                //    break;

                //case "User_ValeLoginDisabled":

                //    tables.Add("Users_Attributes_IDV_OLD");
                //    tables.Add("Users_Attributes_IDV_NEW");

                //    destTables.Add("Splited_Users_ValeLoginDisabled_OLD");
                //    destTables.Add("Splited_Users_ValeLoginDisabled_NEW");

                //    attributes.Add("ValeLoginDisabled");
                //    break;

                //case "User_ValeLastLogonDate":

                //    tables.Add("Users_Attributes_IDV_OLD");
                //    tables.Add("Users_Attributes_IDV_NEW");

                //    destTables.Add("Splited_Users_ValeLastLogonDate_OLD");
                //    destTables.Add("Splited_Users_ValeLastLogonDate_NEW");

                //    attributes.Add("ValeLastLogonDate");
                //    break;

                //case "Users_DirXML_Accounts":

                //    tables.Add("Users_DirXML_Accounts_OLD");
                //    tables.Add("Users_DirXML_Accounts_NEW");

                //    destTables.Add("Splited_Users_DirXML_Accounts_OLD");
                //    destTables.Add("Splited_Users_DirXML_Accounts_NEW");

                //    attributes.Add("DirXML_Accounts");
                //    break;


                case "Groups_member":

                    tables.Add("STG_Groups_OLD");
                    tables.Add("STG_Groups_NEW");

                    destTables.Add("Splited_Groups_OLD");
                    destTables.Add("Splited_Groups_NEW");

                    attributes.Add("Member");
                    break;

                case "Groups_equivalentToMe":

                    tables.Add("STG_Groups_OLD");
                    tables.Add("STG_Groups_NEW");

                    destTables.Add("Splited_Groups_EquivalentToMe_OLD");
                    destTables.Add("Splited_Groups_EquivalentToMe_NEW");

                    attributes.Add("EquivalentToMe");
                    break;

                //case "nrfSOD":

                //    tables.Add("nrfSoD_OLD");
                //    tables.Add("nrfSoD_NEW");

                //    destTables.Add("Splited_Groups_EquivalentToMe_OLD");
                //    destTables.Add("Splited_Groups_EquivalentToMe_NEW");

                //    attributes.Add("EquivalentToMe");
                //    break;

                case "Organization_ACL":

                    tables.Add("STG_Organization_OLD");
                    tables.Add("STG_Organization_NEW");

                    destTables.Add("Splited_Organization_ACL_OLD");
                    destTables.Add("Splited_Organization_ACL_NEW");

                    attributes.Add("ACL");
                    break;

                case "OrganizationalUnit_ACL":

                    tables.Add("STG_OrganizationalUnit_OLD");
                    tables.Add("STG_OrganizationalUnit_NEW");

                    destTables.Add("Splited_OrganizationalUnit_ACL_OLD");
                    destTables.Add("Splited_OrganizationalUnit_ACL_NEW");

                    attributes.Add("ACL");
                    break;

                case "ValeRegion":

                    tables.Add("STG_ValeRegion_OLD");
                    tables.Add("STG_ValeRegion_NEW");

                    destTables.Add("ValeRegion_OLD");
                    destTables.Add("ValeRegion_NEW");

                    attributes.Add("ObjectClass");
                    break;

                case "ValeCountry":

                    tables.Add("STG_ValeCountry_OLD");
                    tables.Add("STG_ValeCountry_NEW");

                    destTables.Add("ValeCountry_OLD");
                    destTables.Add("ValeCountry_NEW");

                    attributes.Add("ObjectClass");
                    break;

                //case "ValeState":

                //    tables.Add("ValeState_OLD");
                //    tables.Add("ValeState_NEW");

                //    destTables.Add("ValeState_OLD");
                //    destTables.Add("ValeState_NEW");

                //    attributes.Add("ObjectClass");
                //    break;

                case "ValeCity":

                    tables.Add("STG_ValeCity_OLD");
                    tables.Add("STG_ValeCity_NEW");

                    destTables.Add("STG_ValeCity_OLD");
                    destTables.Add("STG_ValeCity_NEW");

                    attributes.Add("ObjectClass");
                    break;

                case "ValeWorkflowSOAP":

                    tables.Add("STG_ValeWorkflowSOAP_OLD");
                    tables.Add("STG_ValeWorkflowSOAP_NEW");

                    destTables.Add("ValeWorkflowSOAP_OLD");
                    destTables.Add("ValeWorkflowSOAP_NEW");

                    attributes.Add("ObjectClass");
                    break;

                case "dynamicGroup":

                    tables.Add("STG_DynamicGroup_OLD");
                    tables.Add("STG_DynamicGroup_NEW");

                    destTables.Add("DynamicGroup_OLD");
                    destTables.Add("DynamicGroup_NEW");

                    attributes.Add("ObjectClass");
                    break;

                case "DynamicGroups_member":

                    tables.Add("STG_DynamicGroup_OLD");
                    tables.Add("STG_DynamicGroup_NEW");

                    destTables.Add("Splited_DynamicGroups_member_OLD");
                    destTables.Add("Splited_DynamicGroups_member_NEW");

                    attributes.Add("Member");
                    break;

                case "DynamicGroups_equivalentToMe":

                    tables.Add("STG_DynamicGroup_OLD");
                    tables.Add("STG_DynamicGroup_NEW");

                    destTables.Add("Splited_DynamicGroups_EquivalentToMe_OLD");
                    destTables.Add("Splited_DynamicGroups_EquivalentToMe_NEW");

                    attributes.Add("EquivalentToMe");
                    break;


                default:
                    Console.WriteLine(String.Format("Unknown table passed in the arguments: {0}", tableType)); 
                    break;
            }


            if (operation == "Split")
                Split(tables, attributes, tableType, destTables, dbPlace);

            else if (operation == "Sort")
                ConnectToSQL(tables, attributes, tableType, dbPlace);

            else if (operation == "CheckAssociation")
                CheckMissingAssociation(tables);

            else if (operation == "AssignedResources")
                AssignedResourcesProcessing(attributes, operation, destTables);


            Console.WriteLine("Finish process at " + DateTime.Now.ToString("h:mm:ss tt") + "\n");

            Thread.Sleep(10000);
        }

        public static void ConnectToSQL(List<String> tables, List<String> attributes, String tableType, String dbPlace)
        {
            int DirXML_Associations_Equalized = 0;
            int EquivalentToMe = 0;
            int ValeChildRoles_Equalized = 0;
            int ValeParentRoles_Equalized = 0;
            int nrfMemberOf_Equalized = 0;
            int ValeLoginDisabled_Equalized = 0;
            int ValeLastLogonDate_Equalized = 0;
            int ObjectClass = 0;

            if (tableType == "Role")
            {
                DirXML_Associations_Equalized = 6;
                EquivalentToMe = 7;
                ValeChildRoles_Equalized = 14;
                ValeParentRoles_Equalized = 19;
            }

            else if (tableType == "Resources")
            {
                DirXML_Associations_Equalized = 5;

            }

            else if (tableType == "UsersAssociations")
            {
                nrfMemberOf_Equalized = 7;

            }

            else if (tableType == "UsersAttributes")
            {
                ValeLoginDisabled_Equalized = 2;
                ValeLastLogonDate_Equalized = 4;
            }

            else if (tableType == "ValeRegion" || tableType == "ValeCountry" || tableType == "ValeState" || tableType == "ValeCity" || tableType == "ValeWorkflowSOAP")
            {
                ObjectClass = 2;
            }

            else if (tableType == "dynamicGroup")
            {
                ObjectClass = 11;
            }


            SqlConnection cnn = new SqlConnection();
            //string connetionString;
            if (dbPlace == "local")
                cnn = Connection.DBConnection(@"Data Source="+System.Environment.MachineName+";Initial Catalog=IAM;Integrated Security=True");

            else if (dbPlace == "remote")
                cnn = Connection.DBConnection(@"Data Source=iamupgradeanalysis.ctvkpsgubutg.us-east-1.rds.amazonaws.com;Initial Catalog=IAM;Persist Security Info=True;User ID=admin;Password=jyE1EmejQt4LNLLo9Q8o");
            //connetionString = @"Data Source=BRZBEL000599L;Initial Catalog=IAM;Integrated Security=True";
            //connetionString = @"Data Source=CAAWSD003;Initial Catalog=IAM;Integrated Security=True";
            //cnn = new SqlConnection(connetionString);

            //cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;

            foreach (string table in tables)
            {
                Console.WriteLine("\nWorking with table " + table);

                foreach (string attribute in attributes)
                {
                    Console.WriteLine("\nWorking with attribute " + attribute);
                    //String queryRole = "SELECT * FROM " + table + " WHERE cn IN ('RS_UR_A_SAP_ROLE_12019','RS_SAP_ROLE_18157_BO','RS_SAP_ROLE_18158_BO','RS_SAP_ROLE_18159_BO','RS_SAP_ROLE_18177_BO','RS_SAP_ROLE_18178_BO','RS_SAP_ROLE_18179_BO','RS_SAP_ROLE_18180_BO','RS_SAP_ROLE_19824_BO','RS_SAP_ROLE_19826_BO','RS_SAP_ROLE_19827_BO','RS_SAP_ROLE_19828_BO','RS_SAP_ROLE_19830_BO','RS_SAP_ROLE_19833_BO','RS_SAP_ROLE_19834_BO','RS_SAP_ROLE_19835_BO','RS_SAP_ROLE_19837_BO','RS_SAP_ROLE_19838_BO','RS_SAP_ROLE_19843_BO','RS_SAP_ROLE_19844_BO','RS_SAP_ROLE_19848_BO','RS_SAP_ROLE_19853_BO','RS_SAP_ROLE_19857_BO','RS_SAP_ROLE_19861_BO','RS_SAP_ROLE_19867_BO','RS_SAP_ROLE_19907_BO','RS_SAP_ROLE_19909_BO','RS_SAP_ROLE_19910_BO','RS_SAP_ROLE_19911_BO','RS_SAP_ROLE_19912_BO','RS_SAP_ROLE_19913_BO','RS_SAP_ROLE_19914_BO','RS_SAP_ROLE_19915_BO','RS_SAP_ROLE_19916_BO','RS_SAP_ROLE_19917_BO','RS_SAP_ROLE_19918_BO','RS_SAP_ROLE_19919_BO','RS_SAP_ROLE_19920_BO','RS_SAP_ROLE_19921_BO','RS_SAP_ROLE_19922_BO','RS_SAP_ROLE_19923_BO','RS_SAP_ROLE_19924_BO','RS_SAP_ROLE_19925_BO','RS_SAP_ROLE_19926_BO','RS_SAP_ROLE_19927_BO','RS_SAP_ROLE_19928_BO','RS_SAP_ROLE_19929_BO','RS_SAP_ROLE_19930_BO','RS_SAP_ROLE_19931_BO','RS_SAP_ROLE_19932_BO','RS_SAP_ROLE_19934_BO','RS_SAP_ROLE_19935_BO','RS_SAP_ROLE_19937_BO','RS_SAP_ROLE_19938_BO','RS_SAP_ROLE_19939_BO','RS_SAP_ROLE_19940_BO','RS_SAP_ROLE_19941_BO','RS_SAP_ROLE_19942_BO','RS_SAP_ROLE_19943_BO','RS_SAP_ROLE_19945_BO','RS_SAP_ROLE_19946_BO','RS_SAP_ROLE_19948_BO','RS_SAP_ROLE_19959_BO','RS_SAP_ROLE_19960_BO','RS_SAP_ROLE_19961_BO','RS_SAP_ROLE_19962_BO','RS_SAP_ROLE_19963_BO','RS_SAP_ROLE_19964_BO','RS_SAP_ROLE_19965_BO','RS_SAP_ROLE_19966_BO','RS_SAP_ROLE_19967_BO','RS_SAP_ROLE_19968_BO','RS_SAP_ROLE_19969_BO','RS_SAP_ROLE_19970_BO','RS_SAP_ROLE_19971_BO','RS_SAP_ROLE_19972_BO','RS_SAP_ROLE_19975_BO','RS_SAP_ROLE_19976_BO','RS_SAP_ROLE_19977_BO','RS_SAP_ROLE_19978_BO','RS_SAP_ROLE_19979_BO','RS_SAP_ROLE_19980_BO','RS_SAP_ROLE_19981_BO','RS_SAP_ROLE_19982_BO','RS_SAP_ROLE_19983_BO','RS_SAP_ROLE_19984_BO','RS_SAP_ROLE_19985_BO','RS_SAP_ROLE_19986_BO','RS_SAP_ROLE_19987_BO','RS_SAP_ROLE_19988_BO','RS_SAP_ROLE_19989_BO','RS_SAP_ROLE_19990_BO','RS_SAP_ROLE_19991_BO','RS_SAP_ROLE_19992_BO','RS_SAP_ROLE_19993_BO','RS_SAP_ROLE_19994_BO','RS_SAP_ROLE_19995_BO','RS_SAP_ROLE_19996_BO','RS_SAP_ROLE_19997_BO','RS_SAP_ROLE_19998_BO','RS_SAP_ROLE_19999_BO','RS_SAP_ROLE_20000_BO','RS_SAP_ROLE_20001_BO','RS_SAP_ROLE_20003_BO','RS_SAP_ROLE_20005_BO','RS_SAP_ROLE_20012_BO','RS_SAP_ROLE_20013_BO','RS_SAP_ROLE_20014_BO','RS_SAP_ROLE_20015_BO','RS_SAP_ROLE_20016_BO','RS_SAP_ROLE_20262_BO','RS_SAP_ROLE_20263_BO','RS_SAP_ROLE_20264_BO','RS_SAP_ROLE_20265_BO','RS_SAP_ROLE_20266_BO','RS_SAP_ROLE_20267_BO','RS_SAP_ROLE_20268_BO','RS_SAP_ROLE_20269_BO','RS_SAP_ROLE_20270_BO','RS_UR_A__ROLE_19962_1','RS_UR_A__ROLE_19963_1','RS_UR_A__ROLE_19964_1','RS_UR_A__ROLE_19965_1','RS_UR_A__ROLE_19966_1','RS_UR_A__ROLE_19967_1','RS_UR_A__ROLE_19968_1','RS_UR_A__ROLE_19969_1','RS_UR_A__ROLE_19970_1','RS_UR_A__ROLE_19971_1','RS_UR_A__ROLE_19972_1','RS_UR_A_SAP_ROLE_12725','RS_UR_A_SAP_ROLE_16002','RS_UR_A_SAP_ROLE_16003_1','RS_UR_A_SAP_ROLE_16003','RS_UR_A_SAP_ROLE_16004','RS_UR_A_SAP_ROLE_16005','RS_UR_A_SAP_ROLE_16006','RS_UR_A_SAP_ROLE_16007','RS_UR_A_SAP_ROLE_16008','RS_UR_A_SAP_ROLE_16009','RS_UR_A_SAP_ROLE_16010','RS_UR_A_SAP_ROLE_16011','RS_UR_A_SAP_ROLE_16012','RS_UR_A_SAP_ROLE_16013','RS_UR_A_SAP_ROLE_16016','RS_UR_A_SAP_ROLE_16017','RS_UR_A_SAP_ROLE_16358','RS_UR_A_SAP_ROLE_16359','RS_UR_A_SAP_ROLE_16360','RS_UR_A_SAP_ROLE_16361','RS_UR_A_SAP_ROLE_16362','RS_UR_A_SAP_ROLE_16363','RS_UR_A_SAP_ROLE_16364','RS_UR_A_SAP_ROLE_16365','RS_UR_A_SAP_ROLE_16366','RS_UR_A_SAP_ROLE_16367','RS_UR_A_SAP_ROLE_16368','RS_UR_A_SAP_ROLE_16369','RS_UR_A_SAP_ROLE_16370','RS_UR_A_SAP_ROLE_16371','RS_UR_A_SAP_ROLE_16372','RS_UR_A_SAP_ROLE_16373','RS_UR_A_SAP_ROLE_16378','RS_UR_A_SAP_ROLE_16379','RS_UR_A_SAP_ROLE_16380','RS_UR_A_SAP_ROLE_16381','RS_UR_A_SAP_ROLE_16386','RS_UR_A_SAP_ROLE_16387','RS_UR_A_SAP_ROLE_16388','RS_UR_A_SAP_ROLE_16389','RS_UR_A_SAP_ROLE_16390','RS_UR_A_SAP_ROLE_16391','RS_UR_A_SAP_ROLE_16392','RS_UR_A_SAP_ROLE_16393','RS_UR_A_SAP_ROLE_16394','RS_UR_A_SAP_ROLE_16395','RS_UR_A_SAP_ROLE_16396','RS_UR_A_SAP_ROLE_16397','RS_UR_A_SAP_ROLE_16398','RS_UR_A_SAP_ROLE_16399','RS_UR_A_SAP_ROLE_16400','RS_UR_A_SAP_ROLE_16401','RS_UR_A_SAP_ROLE_16406','RS_UR_A_SAP_ROLE_16407','RS_UR_A_SAP_ROLE_16408','RS_UR_A_SAP_ROLE_16409','RS_UR_A_SAP_ROLE_18074','RS_UR_A_SAP_ROLE_18075','RS_UR_A_SAP_ROLE_18076','RS_UR_A_SAP_ROLE_18077','RS_UR_A_SAP_ROLE_18079','RS_UR_A_SAP_ROLE_18127','RS_UR_A_SAP_ROLE_18128','RS_UR_A_SAP_ROLE_18129','RS_UR_A_SAP_ROLE_18130','RS_UR_A_SAP_ROLE_18132','RS_UR_A_SAP_ROLE_18156_2','RS_UR_A_SAP_ROLE_18157_1','RS_UR_A_SAP_ROLE_18157_2','RS_UR_A_SAP_ROLE_18157','RS_UR_A_SAP_ROLE_18158_1','RS_UR_A_SAP_ROLE_18158_2','RS_UR_A_SAP_ROLE_18158','RS_UR_A_SAP_ROLE_18159_1','RS_UR_A_SAP_ROLE_18159_2','RS_UR_A_SAP_ROLE_18159','RS_UR_A_SAP_ROLE_18161_2','RS_UR_A_SAP_ROLE_18177_1','RS_UR_A_SAP_ROLE_18177_2','RS_UR_A_SAP_ROLE_18177','RS_UR_A_SAP_ROLE_18178_1','RS_UR_A_SAP_ROLE_18178_2','RS_UR_A_SAP_ROLE_18178','RS_UR_A_SAP_ROLE_18179_1','RS_UR_A_SAP_ROLE_18179_2','RS_UR_A_SAP_ROLE_18179','RS_UR_A_SAP_ROLE_18180_1','RS_UR_A_SAP_ROLE_18180_2','RS_UR_A_SAP_ROLE_18180','RS_UR_A_SAP_ROLE_18182_2','RS_UR_A_SAP_ROLE_18198','RS_UR_A_SAP_ROLE_18199','RS_UR_A_SAP_ROLE_18200','RS_UR_A_SAP_ROLE_18201','RS_UR_A_SAP_ROLE_18203','RS_UR_A_SAP_ROLE_18590','RS_UR_A_SAP_ROLE_18674','RS_UR_A_SAP_ROLE_18675','RS_UR_A_SAP_ROLE_18676','RS_UR_A_SAP_ROLE_18677','RS_UR_A_SAP_ROLE_18696','RS_UR_A_SAP_ROLE_18700','RS_UR_A_SAP_ROLE_18704','RS_UR_A_SAP_ROLE_18708','RS_UR_A_SAP_ROLE_18716','RS_UR_A_SAP_ROLE_18747','RS_UR_A_SAP_ROLE_18748','RS_UR_A_SAP_ROLE_18749','RS_UR_A_SAP_ROLE_18750','RS_UR_A_SAP_ROLE_18752','RS_UR_A_SAP_ROLE_18768','RS_UR_A_SAP_ROLE_18769','RS_UR_A_SAP_ROLE_18770','RS_UR_A_SAP_ROLE_18771','RS_UR_A_SAP_ROLE_18773','RS_UR_A_SAP_ROLE_18849','RS_UR_A_SAP_ROLE_18850','RS_UR_A_SAP_ROLE_18851','RS_UR_A_SAP_ROLE_18852','RS_UR_A_SAP_ROLE_18854','RS_UR_A_SAP_ROLE_19824_1','RS_UR_A_SAP_ROLE_19824_2','RS_UR_A_SAP_ROLE_19824_3','RS_UR_A_SAP_ROLE_19826_1','RS_UR_A_SAP_ROLE_19826_2','RS_UR_A_SAP_ROLE_19826_3','RS_UR_A_SAP_ROLE_19827_1','RS_UR_A_SAP_ROLE_19827_2','RS_UR_A_SAP_ROLE_19827_3','RS_UR_A_SAP_ROLE_19828_1','RS_UR_A_SAP_ROLE_19828_2','RS_UR_A_SAP_ROLE_19828_3','RS_UR_A_SAP_ROLE_19829_1','RS_UR_A_SAP_ROLE_19829_3','RS_UR_A_SAP_ROLE_19830_1','RS_UR_A_SAP_ROLE_19830_2','RS_UR_A_SAP_ROLE_19830_3','RS_UR_A_SAP_ROLE_19833_1','RS_UR_A_SAP_ROLE_19833_2','RS_UR_A_SAP_ROLE_19833_3','RS_UR_A_SAP_ROLE_19834_1','RS_UR_A_SAP_ROLE_19834_2','RS_UR_A_SAP_ROLE_19834_3','RS_UR_A_SAP_ROLE_19835_1','RS_UR_A_SAP_ROLE_19835_2','RS_UR_A_SAP_ROLE_19835_3','RS_UR_A_SAP_ROLE_19836_1','RS_UR_A_SAP_ROLE_19836_3','RS_UR_A_SAP_ROLE_19837_1','RS_UR_A_SAP_ROLE_19837_2','RS_UR_A_SAP_ROLE_19837_3','RS_UR_A_SAP_ROLE_19838_1','RS_UR_A_SAP_ROLE_19838_2','RS_UR_A_SAP_ROLE_19838_3','RS_UR_A_SAP_ROLE_19839_1','RS_UR_A_SAP_ROLE_19839_3','RS_UR_A_SAP_ROLE_19843_1','RS_UR_A_SAP_ROLE_19843_2','RS_UR_A_SAP_ROLE_19843_3','RS_UR_A_SAP_ROLE_19844_1','RS_UR_A_SAP_ROLE_19844_2','RS_UR_A_SAP_ROLE_19844_3','RS_UR_A_SAP_ROLE_19848_1','RS_UR_A_SAP_ROLE_19848_2','RS_UR_A_SAP_ROLE_19848_3','RS_UR_A_SAP_ROLE_19849_1','RS_UR_A_SAP_ROLE_19849_3','RS_UR_A_SAP_ROLE_19853_1','RS_UR_A_SAP_ROLE_19853_2','RS_UR_A_SAP_ROLE_19853_3','RS_UR_A_SAP_ROLE_19857_1','RS_UR_A_SAP_ROLE_19857_2','RS_UR_A_SAP_ROLE_19857_3','RS_UR_A_SAP_ROLE_19861_1','RS_UR_A_SAP_ROLE_19861_2','RS_UR_A_SAP_ROLE_19861_3','RS_UR_A_SAP_ROLE_19867_1','RS_UR_A_SAP_ROLE_19867_2','RS_UR_A_SAP_ROLE_19867_3','RS_UR_A_SAP_ROLE_19907_1','RS_UR_A_SAP_ROLE_19907_2','RS_UR_A_SAP_ROLE_19907_3','RS_UR_A_SAP_ROLE_19908_1','RS_UR_A_SAP_ROLE_19908_3','RS_UR_A_SAP_ROLE_19909_1','RS_UR_A_SAP_ROLE_19909_2','RS_UR_A_SAP_ROLE_19909_3','RS_UR_A_SAP_ROLE_19910_1','RS_UR_A_SAP_ROLE_19910_2','RS_UR_A_SAP_ROLE_19910_3','RS_UR_A_SAP_ROLE_19911_1','RS_UR_A_SAP_ROLE_19911_2','RS_UR_A_SAP_ROLE_19911_3','RS_UR_A_SAP_ROLE_19912_1','RS_UR_A_SAP_ROLE_19912_2','RS_UR_A_SAP_ROLE_19912_3','RS_UR_A_SAP_ROLE_19913_1','RS_UR_A_SAP_ROLE_19913_2','RS_UR_A_SAP_ROLE_19913_3','RS_UR_A_SAP_ROLE_19914_1','RS_UR_A_SAP_ROLE_19914_2','RS_UR_A_SAP_ROLE_19914_3','RS_UR_A_SAP_ROLE_19915_1','RS_UR_A_SAP_ROLE_19915_2','RS_UR_A_SAP_ROLE_19915_3','RS_UR_A_SAP_ROLE_19916_1','RS_UR_A_SAP_ROLE_19916_2','RS_UR_A_SAP_ROLE_19916_3','RS_UR_A_SAP_ROLE_19917_1','RS_UR_A_SAP_ROLE_19917_2','RS_UR_A_SAP_ROLE_19917_3','RS_UR_A_SAP_ROLE_19918_1','RS_UR_A_SAP_ROLE_19918_2','RS_UR_A_SAP_ROLE_19918_3','RS_UR_A_SAP_ROLE_19919_1','RS_UR_A_SAP_ROLE_19919_2','RS_UR_A_SAP_ROLE_19919_3','RS_UR_A_SAP_ROLE_19920_1','RS_UR_A_SAP_ROLE_19920_2','RS_UR_A_SAP_ROLE_19920_3','RS_UR_A_SAP_ROLE_19921_1','RS_UR_A_SAP_ROLE_19921_2','RS_UR_A_SAP_ROLE_19921_3','RS_UR_A_SAP_ROLE_19922_1','RS_UR_A_SAP_ROLE_19922_2','RS_UR_A_SAP_ROLE_19922_3','RS_UR_A_SAP_ROLE_19923_1','RS_UR_A_SAP_ROLE_19923_2','RS_UR_A_SAP_ROLE_19923_3','RS_UR_A_SAP_ROLE_19924_1','RS_UR_A_SAP_ROLE_19924_2','RS_UR_A_SAP_ROLE_19924_3','RS_UR_A_SAP_ROLE_19925_1','RS_UR_A_SAP_ROLE_19925_2','RS_UR_A_SAP_ROLE_19925_3','RS_UR_A_SAP_ROLE_19926_1','RS_UR_A_SAP_ROLE_19926_2','RS_UR_A_SAP_ROLE_19926_3','RS_UR_A_SAP_ROLE_19927_1','RS_UR_A_SAP_ROLE_19927_2','RS_UR_A_SAP_ROLE_19927_3','RS_UR_A_SAP_ROLE_19928_1','RS_UR_A_SAP_ROLE_19928_2','RS_UR_A_SAP_ROLE_19928_3','RS_UR_A_SAP_ROLE_19929_1','RS_UR_A_SAP_ROLE_19929_2','RS_UR_A_SAP_ROLE_19929_3','RS_UR_A_SAP_ROLE_19930_1','RS_UR_A_SAP_ROLE_19930_2','RS_UR_A_SAP_ROLE_19930_3','RS_UR_A_SAP_ROLE_19931_1','RS_UR_A_SAP_ROLE_19931_2','RS_UR_A_SAP_ROLE_19931_3','RS_UR_A_SAP_ROLE_19932_1','RS_UR_A_SAP_ROLE_19932_2','RS_UR_A_SAP_ROLE_19932_3','RS_UR_A_SAP_ROLE_19933_1','RS_UR_A_SAP_ROLE_19933_3','RS_UR_A_SAP_ROLE_19934_1','RS_UR_A_SAP_ROLE_19934_2','RS_UR_A_SAP_ROLE_19934_3','RS_UR_A_SAP_ROLE_19935_1','RS_UR_A_SAP_ROLE_19935_2','RS_UR_A_SAP_ROLE_19935_3','RS_UR_A_SAP_ROLE_19936_1','RS_UR_A_SAP_ROLE_19936_3','RS_UR_A_SAP_ROLE_19937_1','RS_UR_A_SAP_ROLE_19937_2','RS_UR_A_SAP_ROLE_19937_3','RS_UR_A_SAP_ROLE_19938_1','RS_UR_A_SAP_ROLE_19938_2','RS_UR_A_SAP_ROLE_19938_3','RS_UR_A_SAP_ROLE_19939_1','RS_UR_A_SAP_ROLE_19939_2','RS_UR_A_SAP_ROLE_19939_3','RS_UR_A_SAP_ROLE_19940_1','RS_UR_A_SAP_ROLE_19940_2','RS_UR_A_SAP_ROLE_19940_3','RS_UR_A_SAP_ROLE_19941_1','RS_UR_A_SAP_ROLE_19941_2','RS_UR_A_SAP_ROLE_19941_3','RS_UR_A_SAP_ROLE_19942_1','RS_UR_A_SAP_ROLE_19942_2','RS_UR_A_SAP_ROLE_19942_3','RS_UR_A_SAP_ROLE_19943_1','RS_UR_A_SAP_ROLE_19943_2','RS_UR_A_SAP_ROLE_19943_3','RS_UR_A_SAP_ROLE_19944_34','RS_UR_A_SAP_ROLE_19945_1','RS_UR_A_SAP_ROLE_19945_2','RS_UR_A_SAP_ROLE_19945_3','RS_UR_A_SAP_ROLE_19946_1','RS_UR_A_SAP_ROLE_19946_2','RS_UR_A_SAP_ROLE_19946_3','RS_UR_A_SAP_ROLE_19947_1','RS_UR_A_SAP_ROLE_19947_3','RS_UR_A_SAP_ROLE_19948_1','RS_UR_A_SAP_ROLE_19948_2','RS_UR_A_SAP_ROLE_19948_3','RS_UR_A_SAP_ROLE_19958_1','RS_UR_A_SAP_ROLE_19958_2','RS_UR_A_SAP_ROLE_19958_3','RS_UR_A_SAP_ROLE_19959_1','RS_UR_A_SAP_ROLE_19959_2','RS_UR_A_SAP_ROLE_19959_3','RS_UR_A_SAP_ROLE_19960_1','RS_UR_A_SAP_ROLE_19960_2','RS_UR_A_SAP_ROLE_19960_3','RS_UR_A_SAP_ROLE_19961_1','RS_UR_A_SAP_ROLE_19961_2','RS_UR_A_SAP_ROLE_19961_3','RS_UR_A_SAP_ROLE_19962_1','RS_UR_A_SAP_ROLE_19962_2','RS_UR_A_SAP_ROLE_19962_3','RS_UR_A_SAP_ROLE_19963_1','RS_UR_A_SAP_ROLE_19963_2','RS_UR_A_SAP_ROLE_19963_3','RS_UR_A_SAP_ROLE_19964_1','RS_UR_A_SAP_ROLE_19964_2','RS_UR_A_SAP_ROLE_19964_3','RS_UR_A_SAP_ROLE_19965_1','RS_UR_A_SAP_ROLE_19965_2','RS_UR_A_SAP_ROLE_19965_3','RS_UR_A_SAP_ROLE_19966_1','RS_UR_A_SAP_ROLE_19966_2','RS_UR_A_SAP_ROLE_19966_3','RS_UR_A_SAP_ROLE_19967_1','RS_UR_A_SAP_ROLE_19967_2','RS_UR_A_SAP_ROLE_19967_3','RS_UR_A_SAP_ROLE_19968_1','RS_UR_A_SAP_ROLE_19968_2','RS_UR_A_SAP_ROLE_19968_3','RS_UR_A_SAP_ROLE_19969_1','RS_UR_A_SAP_ROLE_19969_2','RS_UR_A_SAP_ROLE_19969_3','RS_UR_A_SAP_ROLE_19970_1','RS_UR_A_SAP_ROLE_19970_2','RS_UR_A_SAP_ROLE_19970_3','RS_UR_A_SAP_ROLE_19971_1','RS_UR_A_SAP_ROLE_19971_2','RS_UR_A_SAP_ROLE_19971_3','RS_UR_A_SAP_ROLE_19972_1','RS_UR_A_SAP_ROLE_19972_2','RS_UR_A_SAP_ROLE_19972_3','RS_UR_A_SAP_ROLE_19973_1','RS_UR_A_SAP_ROLE_19973_3','RS_UR_A_SAP_ROLE_19974_1','RS_UR_A_SAP_ROLE_19974_3','RS_UR_A_SAP_ROLE_19975_1','RS_UR_A_SAP_ROLE_19975_2','RS_UR_A_SAP_ROLE_19975_3','RS_UR_A_SAP_ROLE_19976_1','RS_UR_A_SAP_ROLE_19976_2','RS_UR_A_SAP_ROLE_19976_3','RS_UR_A_SAP_ROLE_19977_1','RS_UR_A_SAP_ROLE_19977_2','RS_UR_A_SAP_ROLE_19977_3','RS_UR_A_SAP_ROLE_19978_1','RS_UR_A_SAP_ROLE_19978_2','RS_UR_A_SAP_ROLE_19978_3','RS_UR_A_SAP_ROLE_19979_1','RS_UR_A_SAP_ROLE_19979_2','RS_UR_A_SAP_ROLE_19979_3','RS_UR_A_SAP_ROLE_19980_1','RS_UR_A_SAP_ROLE_19980_2','RS_UR_A_SAP_ROLE_19980_3','RS_UR_A_SAP_ROLE_19981_1','RS_UR_A_SAP_ROLE_19981_2','RS_UR_A_SAP_ROLE_19981_3','RS_UR_A_SAP_ROLE_19982_1','RS_UR_A_SAP_ROLE_19982_2','RS_UR_A_SAP_ROLE_19982_3','RS_UR_A_SAP_ROLE_19983_1','RS_UR_A_SAP_ROLE_19983_2','RS_UR_A_SAP_ROLE_19983_3','RS_UR_A_SAP_ROLE_19984_1','RS_UR_A_SAP_ROLE_19984_2','RS_UR_A_SAP_ROLE_19984_3','RS_UR_A_SAP_ROLE_19985_1','RS_UR_A_SAP_ROLE_19985_2','RS_UR_A_SAP_ROLE_19985_3','RS_UR_A_SAP_ROLE_19986_1','RS_UR_A_SAP_ROLE_19986_2','RS_UR_A_SAP_ROLE_19986_3','RS_UR_A_SAP_ROLE_19987_1','RS_UR_A_SAP_ROLE_19987_2','RS_UR_A_SAP_ROLE_19987_3','RS_UR_A_SAP_ROLE_19988_1','RS_UR_A_SAP_ROLE_19988_2','RS_UR_A_SAP_ROLE_19988_3','RS_UR_A_SAP_ROLE_19989_1','RS_UR_A_SAP_ROLE_19989_2','RS_UR_A_SAP_ROLE_19989_3','RS_UR_A_SAP_ROLE_19990_1','RS_UR_A_SAP_ROLE_19990_2','RS_UR_A_SAP_ROLE_19990_3','RS_UR_A_SAP_ROLE_19991_1','RS_UR_A_SAP_ROLE_19991_2','RS_UR_A_SAP_ROLE_19991_3','RS_UR_A_SAP_ROLE_19992_1','RS_UR_A_SAP_ROLE_19992_2','RS_UR_A_SAP_ROLE_19992_3','RS_UR_A_SAP_ROLE_19993_1','RS_UR_A_SAP_ROLE_19993_2','RS_UR_A_SAP_ROLE_19993_3','RS_UR_A_SAP_ROLE_19994_1','RS_UR_A_SAP_ROLE_19994_2','RS_UR_A_SAP_ROLE_19994_3','RS_UR_A_SAP_ROLE_19995_1','RS_UR_A_SAP_ROLE_19995_2','RS_UR_A_SAP_ROLE_19995_3','RS_UR_A_SAP_ROLE_19996_1','RS_UR_A_SAP_ROLE_19996_2','RS_UR_A_SAP_ROLE_19996_3','RS_UR_A_SAP_ROLE_19997_1','RS_UR_A_SAP_ROLE_19997_2','RS_UR_A_SAP_ROLE_19997_3','RS_UR_A_SAP_ROLE_19998_1','RS_UR_A_SAP_ROLE_19998_2','RS_UR_A_SAP_ROLE_19998_3','RS_UR_A_SAP_ROLE_19999_1','RS_UR_A_SAP_ROLE_19999_2','RS_UR_A_SAP_ROLE_19999_3','RS_UR_A_SAP_ROLE_20000_1','RS_UR_A_SAP_ROLE_20000_2','RS_UR_A_SAP_ROLE_20000_3','RS_UR_A_SAP_ROLE_20001_1','RS_UR_A_SAP_ROLE_20001_2','RS_UR_A_SAP_ROLE_20001_3','RS_UR_A_SAP_ROLE_20002_1','RS_UR_A_SAP_ROLE_20003_1','RS_UR_A_SAP_ROLE_20003_2','RS_UR_A_SAP_ROLE_20003_3','RS_UR_A_SAP_ROLE_20004_1','RS_UR_A_SAP_ROLE_20004_3','RS_UR_A_SAP_ROLE_20005_1','RS_UR_A_SAP_ROLE_20005_2','RS_UR_A_SAP_ROLE_20005_3','RS_UR_A_SAP_ROLE_20006_1','RS_UR_A_SAP_ROLE_20006_3','RS_UR_A_SAP_ROLE_20007_1','RS_UR_A_SAP_ROLE_20007_3','RS_UR_A_SAP_ROLE_20008_1','RS_UR_A_SAP_ROLE_20008_3','RS_UR_A_SAP_ROLE_20009_1','RS_UR_A_SAP_ROLE_20009_35','RS_UR_A_SAP_ROLE_20010_1','RS_UR_A_SAP_ROLE_20010_2','RS_UR_A_SAP_ROLE_20010_36','RS_UR_A_SAP_ROLE_20011_1','RS_UR_A_SAP_ROLE_20011_3','RS_UR_A_SAP_ROLE_20012_1','RS_UR_A_SAP_ROLE_20012_2','RS_UR_A_SAP_ROLE_20012_3','RS_UR_A_SAP_ROLE_20013_1','RS_UR_A_SAP_ROLE_20013_2','RS_UR_A_SAP_ROLE_20013_3','RS_UR_A_SAP_ROLE_20014_1','RS_UR_A_SAP_ROLE_20014_2','RS_UR_A_SAP_ROLE_20014_3','RS_UR_A_SAP_ROLE_20015_1','RS_UR_A_SAP_ROLE_20015_2','RS_UR_A_SAP_ROLE_20015_3','RS_UR_A_SAP_ROLE_20016_1','RS_UR_A_SAP_ROLE_20016_2','RS_UR_A_SAP_ROLE_20016_3','RS_UR_A_SAP_ROLE_20262_1','RS_UR_A_SAP_ROLE_20262_2','RS_UR_A_SAP_ROLE_20262_3','RS_UR_A_SAP_ROLE_20263_1','RS_UR_A_SAP_ROLE_20263_2','RS_UR_A_SAP_ROLE_20263_3','RS_UR_A_SAP_ROLE_20264_1','RS_UR_A_SAP_ROLE_20264_2','RS_UR_A_SAP_ROLE_20264_3','RS_UR_A_SAP_ROLE_20265_1','RS_UR_A_SAP_ROLE_20265_2','RS_UR_A_SAP_ROLE_20265_3','RS_UR_A_SAP_ROLE_20266_1','RS_UR_A_SAP_ROLE_20266_2','RS_UR_A_SAP_ROLE_20266_3','RS_UR_A_SAP_ROLE_20267_1','RS_UR_A_SAP_ROLE_20267_2','RS_UR_A_SAP_ROLE_20267_3','RS_UR_A_SAP_ROLE_20268_1','RS_UR_A_SAP_ROLE_20268_2','RS_UR_A_SAP_ROLE_20268_3','RS_UR_A_SAP_ROLE_20269_1','RS_UR_A_SAP_ROLE_20269_2','RS_UR_A_SAP_ROLE_20269_3','RS_UR_A_SAP_ROLE_20270_1','RS_UR_A_SAP_ROLE_20270_2','RS_UR_A_SAP_ROLE_20270_3','RS_UR_A_SAP_ROLE_20604_1','RS_UR_A_SAP_ROLE_20604_2','RS_UR_A_SAP_ROLE_20604_3','RS_UR_A_SAP_ROLE_20606_3','RS_UR_A_SAP_ROLE_20607_3','RS_UR_A_SAP_ROLE_20608_3','RS_UR_A_SAP_ROLE_20609_3','RS_UR_A_SAP_ROLE_20610_3','RS_UR_A_SAP_ROLE_20611_3','RS_UR_A_SAP_ROLE_20612_3','RS_UR_A_SAP_ROLE_20613_3','RS_UR_A_SAP_ROLE_20614_3','RS_UR_A_SAP_ROLE_20615_3','RS_UR_A_SAP_ROLE_20616_3','RS_UR_A_SAP_ROLE_20660_01','RS_UR_A_SAP_ROLE_20660_2','RS_UR_A_SAP_ROLE_20661_01','RS_UR_A_SAP_ROLE_20661_2','RS_UR_A_SAP_ROLE_20662_01','RS_UR_A_SAP_ROLE_20662_2','RS_UR_A_SAP_ROLE_20663_01','RS_UR_A_SAP_ROLE_20663_2','RS_UR_A_SAP_ROLE_20664_01','RS_UR_A_SAP_ROLE_20664_2','RS_UR_A_SAP_ROLE_20665_01','RS_UR_A_SAP_ROLE_20665_2','RS_UR_A_SAP_ROLE_20666_01','RS_UR_A_SAP_ROLE_20666_2','RS_UR_A_SAP_ROLE_20667_01','RS_UR_A_SAP_ROLE_20667_2','RS_UR_A_SAP_ROLE_20668_01','RS_UR_A_SAP_ROLE_20668_2','RS_UR_A_SAP_ROLE_20669_01','RS_UR_A_SAP_ROLE_20669_2','RS_UR_A_SAP_ROLE_20670_01','RS_UR_A_SAP_ROLE_20670_2','RS_UR_A_SAP_ROLE_20671_01','RS_UR_A_SAP_ROLE_20671_2','RS_UR_A_SAP_ROLE_20672_01','RS_UR_A_SAP_ROLE_20672_2','RS_UR_A_SAP_ROLE_20673_01','RS_UR_A_SAP_ROLE_20673_2','RS_UR_A_SAP_ROLE_20674_01','RS_UR_A_SAP_ROLE_20674_2','RS_UR_A_SAP_ROLE_20675_01','RS_UR_A_SAP_ROLE_20675_2','RS_UR_A_SAP_ROLE_20676_01','RS_UR_A_SAP_ROLE_20676_2','RS_UR_A_SAP_ROLE_20677_01','RS_UR_A_SAP_ROLE_20677_2','RS_UR_A_SAP_ROLE_20678_01','RS_UR_A_SAP_ROLE_20678_2','RS_UR_A_SAP_ROLE_20679_01','RS_UR_A_SAP_ROLE_20679_2','RS_UR_A_SAP_ROLE_20680_01','RS_UR_A_SAP_ROLE_20680_2','RS_UR_A_SAP_ROLE_22224_1','RS_UR_A_SAP_ROLE_22224_2','RS_UR_A_SAP_ROLE_22225_1','RS_UR_A_SAP_ROLE_22225_2','RS_UR_A_SAP_ROLE_22226_1','RS_UR_A_SAP_ROLE_22226_2','RS_UR_A_SAP_ROLE_22333_4','RS_UR_A_SAP_ROLE_22333_5','RS_UR_A_SAP_ROLE_22333_6','RS_UR_A_SAP_ROLE_22334_4','RS_UR_A_SAP_ROLE_22334_5','RS_UR_A_SAP_ROLE_22334_6','RS_UR_A_SAP_ROLE_22335_4','RS_UR_A_SAP_ROLE_22335_5','RS_UR_A_SAP_ROLE_22335_6','RS_UR_A_SAP_ROLE_22412_1','RS_UR_A_SAP_ROLE_22412_2','RS_UR_A_SAP_ROLE_22609_1','RS_UR_A_SAP_ROLE_22610_1','RS_UR_A_SAP_ROLE_22611_1','RS_UR_A_SAP_ROLE_23791_1','RS_UR_A_SAP_ROLE_23791_2','RS_UR_A_SAP_ROLE_23792_1','RS_UR_A_SAP_ROLE_23792_2','RS_UR_A_SAP_SAP_ROLE_16546')";
                    String queryRole = "SELECT * FROM " + table;
                    String output = String.Empty;
                    List<String> updateValue = new List<string>();

                    command = new SqlCommand(queryRole, cnn);
                    dataReader = command.ExecuteReader();

                    int att = 0;
                    if (attribute == "DirXML_Associations_Equalized")
                    {
                        att = DirXML_Associations_Equalized;
                    }

                    else if (attribute == "EquivalentToMe")
                    {
                        att = EquivalentToMe;
                    }
                    else if (attribute == "ValeChildRoles_Equalized")
                    {

                        att = ValeChildRoles_Equalized;
                    }

                    else if (attribute == "ValeParentRoles_Equalized")
                    {

                        att = ValeParentRoles_Equalized;
                    }

                    else if (attribute == "nrfMemberOf_Equalized")
                    {

                        att = nrfMemberOf_Equalized;
                    }

                    else if (attribute == "ValeLoginDisabled_Equalized")
                    {

                        att = ValeLoginDisabled_Equalized;
                    }

                    else if (attribute == "ValeLastLogonDate_Equalized")
                    {

                        att = ValeLastLogonDate_Equalized;
                    }

                    else if (attribute == "ObjectClass")
                    {

                        att = ObjectClass;
                    }

                    int count = 0;
                    Console.WriteLine("Buffering change number: ");
                    while (dataReader.Read())
                    {
                        output = dataReader.GetValue(att).ToString();
                        if (!String.IsNullOrEmpty(output))
                        {
                            
                            String DN = dataReader.GetValue(0).ToString().Replace("\'", "\''"); //DN MUST BE IN THE POSITION 0 IN ALL TABLES

                            /* USE THIS TO Get column names
                             

                                var reader = cmd.ExecuteReader();

                                var columns = new List<string>();

                                for(int i=0;i<reader.FieldCount;i++)
                                {
                                   columns.Add(reader.GetName(i));
                                }

                                or

                                var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();


                             */

                            //String[] outputArray = output.Split(";");
                            String[] outputArray = output.Split("|");
                            Array.Sort(outputArray);
                            String valores = String.Empty;
                            foreach (String valor in outputArray)
                            {
                                valores += valor + "|";
                            }

                            valores = valores.Remove(valores.Length - 1, 1);

                            updateValue.Add("UPDATE " + table + " SET " + attribute + " = '" + valores + "'" + " WHERE DN = '" + DN + "'");
                            count++;
                            //Console.WriteLine(count);
                            Console.Write("\r{0}   ", count);
                            //Console.WriteLine("Buffering change number: " + count);
                            //Console.SetCursorPosition(0, Console.CursorTop - 1);
                            //ClearCurrentConsoleLine();


                            //for (int i = 0; i < 100; ++i)
                            //{
                            //    Console.Write("\r{0}%   ", i);
                            //}

                        }

                    }


                    dataReader.Close();
                    command.Dispose();


                    //SqlCommand commandUpdate;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    int count2 = 0;
                    Console.WriteLine("\n\nUpdating change number: ");
                    foreach (String valor in updateValue)
                    {
                        //commandUpdate = new SqlCommand(valor, cnn);
                        adapter.UpdateCommand = new SqlCommand(valor, cnn);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        count2++;
                        Console.Write("\r{0}   ", count2);
                        //Console.Write("\r \r" + new string(' ', Console.WindowWidth) + "\r");
                        //Console.SetCursorPosition(0, Console.CursorTop - 1);
                        //ClearCurrentConsoleLine();

                    }

                }

            }

            cnn.Close();

        }

        public static void AssignedResourcesProcessing(List<string> attribute, string operation, List<string> destTables)
        {
            if (attribute[0] == "nrfAssignedResources")
            {

                //method = SQLInputMethod.Bulk;
                //string treatedResource = string.Empty;
                string resourceDN = string.Empty;
                string Resource_Base64_Remove = string.Empty;
                string Resource_Base64_Add = string.Empty;
                string valueWithNewDriverSet = string.Empty;
                const char carRet = '\r';
                //string linebreak = "|" ; //+ carRet + newline;                                
                string result = string.Empty;
                const char newline = '\n';//(char) 10;

                string linebreak = "" + carRet + newline;
                int destTableNum = 0;
                List<string> paths = new List<string>();
                    
                paths.Add(@"C:\Users\bcmenic\Desktop\DataAnalysis\Users nrfAssignedResources\IAM_IDV_Users_Resources_NEW.csv");

                if (operation != "LDIF")
                {
                    paths.Add(@"C:\Users\bcmenic\Desktop\DataAnalysis\Users nrfAssignedResources\IAM_IDV_Users_Resources_OLD.csv");
                    destTableNum = 1;
                }
                List<string> column1 = new List<string>();
                List<string> column2 = new List<string>();

                var dt_CSV = new DataTable();
                dt_CSV.Columns.Add("DN");
                dt_CSV.Columns.Add("nrfAssignedResources");


                string DN_CSV = string.Empty;
                string assignedResource_CSV = string.Empty;
                const string quote = "\"";
                string currentLine = ""; //var
                Boolean endResource = false; //var
                int count = 0;

                //string DN_CSV = string.Empty;
                //string assignedResource_CSV = string.Empty;
                //const string quote = "\"";

                string completRecord = string.Empty;
                bool mountingRecord = false;

                //string completeFile = File.ReadAllText(path);


                foreach (string path in paths)
                {
                   
                    using (var reader = new StreamReader(path))
                    {
                        ;
                        bool isHeader = true;

                        Console.WriteLine("\nWorking with file " + path);
                        Console.WriteLine("\nWorking with attribute " + attribute[0]);
                        Console.WriteLine("\nBuffering change number - Users: ");

                        while (!reader.EndOfStream)
                        {

                            #region Rafael´s code
                            //currentLine = reader.ReadLine();


                            //// verifica se a linha é um user DN
                            //if (!isHeader) // Bruno - Para pular cabeçalho
                            //{
                            //    currentLine = currentLine.Replace("\"\"", "\"");
                            //    count = count + 1;
                            //    if (currentLine.EndsWith("ou=People,o=vale" + (char)34 + ","))
                            //    {
                            //        // é um DN puro
                            //        //System.Diagnostics.Debug.WriteLine(currentLine);


                            //        // escapa para o arquivo e zera o acumulador
                            //        var lineDN = currentLine.Split(linebreak);
                            //        DN_CSV = lineDN[0].Substring(1, lineDN[0].IndexOf(quote + @",") - 1);
                            //        dt_CSV.Rows.Add(DN_CSV, "");

                            //        Console.Write("\r{0}   ", count);

                            //    }
                            //    else
                            //    {
                            //        // não é um DN sozinho
                            //        // verifica se é a primeira linha

                            //        var lineNDN = currentLine.Split(linebreak);
                            //        DN_CSV = lineNDN[0].Substring(1, lineNDN[0].IndexOf(quote + @",") - 1);
                            //        if (lineNDN[0].Length > DN_CSV.Length + 3)
                            //            assignedResource_CSV = lineNDN[0].Substring(DN_CSV.Length + 4) + (char)10; //Bruno - adicionando quebra de linha
                            //        endResource = false;
                            //        while (!endResource)
                            //        {
                            //            currentLine = reader.ReadLine();
                            //            if (!currentLine.EndsWith((char)34))
                            //            {
                            //                // em recurso, adiciona e mantém
                            //                assignedResource_CSV = assignedResource_CSV + currentLine + (char)10; //Bruno - adicionando quebra de linha
                            //            }
                            //            else
                            //            {
                            //                // fim do recurso, adiciona e sai
                            //                assignedResource_CSV = assignedResource_CSV + currentLine;
                            //                endResource = true;
                            //                dt_CSV.Rows.Add(DN_CSV, assignedResource_CSV);
                            //                Console.Write("\r{0}   ", count); //Bruno - saida na tela
                            //            }
                            //        }

                            //    }

                            //}
                            #endregion

                            #region Bruno´s Code
                            var line = reader.ReadLine();

                            if (!isHeader)
                            {

                                int Check_Size = 0;
                                if (mountingRecord == false)
                                {
                                    DN_CSV = line.Substring(1, line.IndexOf(quote + @",") - 1);
                                    Check_Size = DN_CSV.Length + 3;

                                    //if (DN_CSV.Contains("C0609801"))
                                    //    DN_CSV = DN_CSV;

                                }
                                //else
                                //    DN_CSV = string.Empty;

                                if (!line.Contains(@">" + quote)) // && !line.Contains(quote+@","))
                                {

                                    if (line.Length > Check_Size)
                                    {
                                        completRecord += line + (char)10;
                                        mountingRecord = true;
                                    }

                                }

                                if (line.Contains(@">" + quote) && mountingRecord == true)
                                {
                                    completRecord += line + (char)10;
                                    mountingRecord = false;
                                }

                                else if (line.Contains(@">" + quote) && mountingRecord == false)
                                {
                                    completRecord = line;
                                }

                                if (mountingRecord == false)
                                {
                                    //DN_CSV = line.Substring(1, line.IndexOf(quote + @",") - 1);

                                    if (line.Length > Check_Size)
                                    {

                                        assignedResource_CSV = completRecord.Substring(completRecord.IndexOf(quote + "," + quote) + 3);
                                    }

                                    assignedResource_CSV = assignedResource_CSV.Replace("\"\"", "\"");
                                    assignedResource_CSV = assignedResource_CSV.Replace("</assignment>" + quote + (char)10, "</assignment>");
                                    assignedResource_CSV = assignedResource_CSV.Replace("</assignment>" + quote, "</assignment>");


                                    dt_CSV.Rows.Add(DN_CSV, assignedResource_CSV);

                                    DN_CSV = string.Empty;
                                    assignedResource_CSV = string.Empty;
                                    completRecord = string.Empty;
                                    count = count++;
                                    Console.Write("\r{0}   ", count);

                                }

                                

                            }

                            #endregion

                            #region Bruno's Code ver 2


                            #endregion

                            isHeader = false;
                        }
                    }

                    Console.Write("\n\n");

           
                    if (operation == "LDIF")
                    {
                        Console.Write("\r{0}   ", "Creating LDIF file");

                        TextWriter tw = new StreamWriter(@"C:\LDIF\output.ldif");

                        int countRecordsWithResources = 0;
                        int countRecordsWithoutResources = 0;

                        for (int i = 0; i < dt_CSV.Rows.Count; i++)
                        //for (int i = 0; i < 100; i++)
                        {
                            Console.Write("\r{0}{1}{2}{3}{4}{5}{6}{7}", "Writing record ", countRecordsWithResources + 1, " of ", dt_CSV.Rows.Count, " - Skiping Record without resource ", countRecordsWithoutResources + 1, " of ", dt_CSV.Rows.Count);
                            string DN = string.Empty;
                            String output = String.Empty;
                           

                            if (dt_CSV.Rows[i]["nrfAssignedResources"].ToString() != string.Empty)
                            {
                                DN = dt_CSV.Rows[i]["DN"].ToString();
                                output = dt_CSV.Rows[i]["nrfAssignedResources"].ToString();

                                //if (DN.Contains("C0372581"))
                                //    DN = DN;

                                String[] outputArray = output.Split("|");

                                foreach (string valor in outputArray)
                                {
                                    
                                    Resource_Base64_Remove += "nrfAssignedResources:: " + EncodeToBase64(valor) + linebreak;
                                    valueWithNewDriverSet = TrataValorParaGerarLDIF(valor, attribute[0]);
                                    Resource_Base64_Add += "nrfAssignedResources:: " + EncodeToBase64(valueWithNewDriverSet) + linebreak;
                                }



                                result = "dn: " + DN + linebreak + "changetype: modify" + linebreak + "delete: nrfAssignedResources" + linebreak + Resource_Base64_Remove + "-" + linebreak + linebreak +
                                 "dn: " + DN + linebreak + "changetype: modify" + linebreak + "add: nrfAssignedResources" + linebreak + Resource_Base64_Add + linebreak;

                                Resource_Base64_Remove = string.Empty;
                                Resource_Base64_Add = string.Empty;

                                tw.WriteLine(result);
                                countRecordsWithResources = countRecordsWithResources + 1;
                            }

                            else
                                countRecordsWithoutResources = countRecordsWithoutResources + 1;

                        }

                        tw.Close();
                        Console.WriteLine("\n");

                    }

                    else
                    {
                        //escrever codigo para salvar no database remoto
                        

                        SqlConnection cnnRemote = new SqlConnection();

                        cnnRemote = Connection.DBConnection(@"Data Source=iamupgradeanalysis.ctvkpsgubutg.us-east-1.rds.amazonaws.com;Initial Catalog=IAM;Persist Security Info=True;User ID=admin;Password=jyE1EmejQt4LNLLo9Q8o");


                        SqlCommand commandRemote;
                        SqlDataReader dataReaderRemote;


                        String queryRoleRemote = "SELECT TOP 1 * FROM " + destTables[destTableNum];
                        commandRemote = new SqlCommand(queryRoleRemote, cnnRemote);
                        dataReaderRemote = commandRemote.ExecuteReader();

                        var columnsRemote = Enumerable.Range(0, dataReaderRemote.FieldCount).Select(dataReaderRemote.GetName).ToList(); //gets all the columns from the table

                        var dt = new DataTable(); //NEW
                        foreach (var column in columnsRemote) //NEW
                            dt.Columns.Add(column.ToString()); //NEW

                        //if (!dt.Columns.Contains("matchingValues"))
                        //    dt.Columns.Add("matchingValues");

                        //int att = columns.IndexOf(attribute); // get´s the index of the attribute

                        int count2 = 0;
                        Console.WriteLine("Processing data to update remote DBs ");
                        Console.WriteLine("\nBuffering change number - nrfAssignedResources: ");

                        string DN = string.Empty;
                        String output = String.Empty;
                        string treatedResource = string.Empty;

                        for (int i = 0; i < dt_CSV.Rows.Count; i++)
                        {
                            if (dt_CSV.Rows[i]["nrfAssignedResources"].ToString() != string.Empty)
                            {
                                DN = dt_CSV.Rows[i]["DN"].ToString();
                                output = dt_CSV.Rows[i]["nrfAssignedResources"].ToString();

                                if (output != string.Empty)
                                {
                                    String[] outputArray = output.Split("|");

                                    foreach (string valor in outputArray)
                                    {
                                        treatedResource = TrataValor(valor, attribute[0]);
                                        resourceDN = GetResourceDNFromnrfAssignedResource(treatedResource);

                                        dt.Rows.Add(DN, string.Empty, valor, treatedResource, DN + resourceDN, resourceDN); //NEW
                                        count2++;
                                    }
                                }
                            }

                            Console.Write("\r{0}   ", count2);

                        }

                        //dataReaderRemote.Close();
                        //commandRemote.Dispose();



                        SqlDataAdapter adapter = new SqlDataAdapter();
                        //int count2 = 0;


                        cnnRemote = Connection.DBConnection(@"Data Source=iamupgradeanalysis.ctvkpsgubutg.us-east-1.rds.amazonaws.com;Initial Catalog=IAM;Persist Security Info=True;User ID=admin;Password=jyE1EmejQt4LNLLo9Q8o");

                        //String queryRoleDest = "SELECT * FROM " + destTables[tables.IndexOf(table)];

                        //command = new SqlCommand(queryRoleDest, cnnRemote);                




                        Console.WriteLine("\n\nUpdating change number - nrfAssignedResources: ");

                        BulkInSQL(cnnRemote, destTableNum, destTables, dt, count2);
                        destTableNum = destTableNum - 1;
                        dt_CSV.Clear();
                        count = 0;
                        count2 = 0;
                    }

                }

            }

        }



        public static void Split(List<String> tables, List<String> attributes, String tableType, List<string> destTables, String dbPlace)
        {
            
            SqlConnection cnn = new SqlConnection();
            SqlConnection cnnRemote = new SqlConnection();

            if (dbPlace == "local")
                cnn = Connection.DBConnection(@"Data Source="+System.Environment.MachineName+";Initial Catalog=IAM;Integrated Security=True");

            else if (dbPlace == "remote")
                cnn = Connection.DBConnection(@"Data Source=iamupgradeanalysis.ctvkpsgubutg.us-east-1.rds.amazonaws.com;Initial Catalog=IAM;Persist Security Info=True;User ID=admin;Password=jyE1EmejQt4LNLLo9Q8o");

            cnnRemote = Connection.DBConnection(@"Data Source=iamupgradeanalysis.ctvkpsgubutg.us-east-1.rds.amazonaws.com;Initial Catalog=IAM;Persist Security Info=True;User ID=admin;Password=jyE1EmejQt4LNLLo9Q8o");

            SqlCommand command;
            SqlDataReader dataReader;

            SqlCommand commandRemote;
            SqlDataReader dataReaderRemote;

            SQLInputMethod method = SQLInputMethod.Insert;

            

            foreach (string table in tables)
            {
                Console.WriteLine("\nWorking with table " + table);

                foreach (string attribute in attributes)
                {
                    Console.WriteLine("\nWorking with attribute " + attribute);
                    //String queryRole = "SELECT * FROM " + table + " WHERE cn IN ('RS_UR_A_SAP_ROLE_12019','RS_SAP_ROLE_18157_BO','RS_SAP_ROLE_18158_BO','RS_SAP_ROLE_18159_BO','RS_SAP_ROLE_18177_BO','RS_SAP_ROLE_18178_BO','RS_SAP_ROLE_18179_BO','RS_SAP_ROLE_18180_BO','RS_SAP_ROLE_19824_BO','RS_SAP_ROLE_19826_BO','RS_SAP_ROLE_19827_BO','RS_SAP_ROLE_19828_BO','RS_SAP_ROLE_19830_BO','RS_SAP_ROLE_19833_BO','RS_SAP_ROLE_19834_BO','RS_SAP_ROLE_19835_BO','RS_SAP_ROLE_19837_BO','RS_SAP_ROLE_19838_BO','RS_SAP_ROLE_19843_BO','RS_SAP_ROLE_19844_BO','RS_SAP_ROLE_19848_BO','RS_SAP_ROLE_19853_BO','RS_SAP_ROLE_19857_BO','RS_SAP_ROLE_19861_BO','RS_SAP_ROLE_19867_BO','RS_SAP_ROLE_19907_BO','RS_SAP_ROLE_19909_BO','RS_SAP_ROLE_19910_BO','RS_SAP_ROLE_19911_BO','RS_SAP_ROLE_19912_BO','RS_SAP_ROLE_19913_BO','RS_SAP_ROLE_19914_BO','RS_SAP_ROLE_19915_BO','RS_SAP_ROLE_19916_BO','RS_SAP_ROLE_19917_BO','RS_SAP_ROLE_19918_BO','RS_SAP_ROLE_19919_BO','RS_SAP_ROLE_19920_BO','RS_SAP_ROLE_19921_BO','RS_SAP_ROLE_19922_BO','RS_SAP_ROLE_19923_BO','RS_SAP_ROLE_19924_BO','RS_SAP_ROLE_19925_BO','RS_SAP_ROLE_19926_BO','RS_SAP_ROLE_19927_BO','RS_SAP_ROLE_19928_BO','RS_SAP_ROLE_19929_BO','RS_SAP_ROLE_19930_BO','RS_SAP_ROLE_19931_BO','RS_SAP_ROLE_19932_BO','RS_SAP_ROLE_19934_BO','RS_SAP_ROLE_19935_BO','RS_SAP_ROLE_19937_BO','RS_SAP_ROLE_19938_BO','RS_SAP_ROLE_19939_BO','RS_SAP_ROLE_19940_BO','RS_SAP_ROLE_19941_BO','RS_SAP_ROLE_19942_BO','RS_SAP_ROLE_19943_BO','RS_SAP_ROLE_19945_BO','RS_SAP_ROLE_19946_BO','RS_SAP_ROLE_19948_BO','RS_SAP_ROLE_19959_BO','RS_SAP_ROLE_19960_BO','RS_SAP_ROLE_19961_BO','RS_SAP_ROLE_19962_BO','RS_SAP_ROLE_19963_BO','RS_SAP_ROLE_19964_BO','RS_SAP_ROLE_19965_BO','RS_SAP_ROLE_19966_BO','RS_SAP_ROLE_19967_BO','RS_SAP_ROLE_19968_BO','RS_SAP_ROLE_19969_BO','RS_SAP_ROLE_19970_BO','RS_SAP_ROLE_19971_BO','RS_SAP_ROLE_19972_BO','RS_SAP_ROLE_19975_BO','RS_SAP_ROLE_19976_BO','RS_SAP_ROLE_19977_BO','RS_SAP_ROLE_19978_BO','RS_SAP_ROLE_19979_BO','RS_SAP_ROLE_19980_BO','RS_SAP_ROLE_19981_BO','RS_SAP_ROLE_19982_BO','RS_SAP_ROLE_19983_BO','RS_SAP_ROLE_19984_BO','RS_SAP_ROLE_19985_BO','RS_SAP_ROLE_19986_BO','RS_SAP_ROLE_19987_BO','RS_SAP_ROLE_19988_BO','RS_SAP_ROLE_19989_BO','RS_SAP_ROLE_19990_BO','RS_SAP_ROLE_19991_BO','RS_SAP_ROLE_19992_BO','RS_SAP_ROLE_19993_BO','RS_SAP_ROLE_19994_BO','RS_SAP_ROLE_19995_BO','RS_SAP_ROLE_19996_BO','RS_SAP_ROLE_19997_BO','RS_SAP_ROLE_19998_BO','RS_SAP_ROLE_19999_BO','RS_SAP_ROLE_20000_BO','RS_SAP_ROLE_20001_BO','RS_SAP_ROLE_20003_BO','RS_SAP_ROLE_20005_BO','RS_SAP_ROLE_20012_BO','RS_SAP_ROLE_20013_BO','RS_SAP_ROLE_20014_BO','RS_SAP_ROLE_20015_BO','RS_SAP_ROLE_20016_BO','RS_SAP_ROLE_20262_BO','RS_SAP_ROLE_20263_BO','RS_SAP_ROLE_20264_BO','RS_SAP_ROLE_20265_BO','RS_SAP_ROLE_20266_BO','RS_SAP_ROLE_20267_BO','RS_SAP_ROLE_20268_BO','RS_SAP_ROLE_20269_BO','RS_SAP_ROLE_20270_BO','RS_UR_A__ROLE_19962_1','RS_UR_A__ROLE_19963_1','RS_UR_A__ROLE_19964_1','RS_UR_A__ROLE_19965_1','RS_UR_A__ROLE_19966_1','RS_UR_A__ROLE_19967_1','RS_UR_A__ROLE_19968_1','RS_UR_A__ROLE_19969_1','RS_UR_A__ROLE_19970_1','RS_UR_A__ROLE_19971_1','RS_UR_A__ROLE_19972_1','RS_UR_A_SAP_ROLE_12725','RS_UR_A_SAP_ROLE_16002','RS_UR_A_SAP_ROLE_16003_1','RS_UR_A_SAP_ROLE_16003','RS_UR_A_SAP_ROLE_16004','RS_UR_A_SAP_ROLE_16005','RS_UR_A_SAP_ROLE_16006','RS_UR_A_SAP_ROLE_16007','RS_UR_A_SAP_ROLE_16008','RS_UR_A_SAP_ROLE_16009','RS_UR_A_SAP_ROLE_16010','RS_UR_A_SAP_ROLE_16011','RS_UR_A_SAP_ROLE_16012','RS_UR_A_SAP_ROLE_16013','RS_UR_A_SAP_ROLE_16016','RS_UR_A_SAP_ROLE_16017','RS_UR_A_SAP_ROLE_16358','RS_UR_A_SAP_ROLE_16359','RS_UR_A_SAP_ROLE_16360','RS_UR_A_SAP_ROLE_16361','RS_UR_A_SAP_ROLE_16362','RS_UR_A_SAP_ROLE_16363','RS_UR_A_SAP_ROLE_16364','RS_UR_A_SAP_ROLE_16365','RS_UR_A_SAP_ROLE_16366','RS_UR_A_SAP_ROLE_16367','RS_UR_A_SAP_ROLE_16368','RS_UR_A_SAP_ROLE_16369','RS_UR_A_SAP_ROLE_16370','RS_UR_A_SAP_ROLE_16371','RS_UR_A_SAP_ROLE_16372','RS_UR_A_SAP_ROLE_16373','RS_UR_A_SAP_ROLE_16378','RS_UR_A_SAP_ROLE_16379','RS_UR_A_SAP_ROLE_16380','RS_UR_A_SAP_ROLE_16381','RS_UR_A_SAP_ROLE_16386','RS_UR_A_SAP_ROLE_16387','RS_UR_A_SAP_ROLE_16388','RS_UR_A_SAP_ROLE_16389','RS_UR_A_SAP_ROLE_16390','RS_UR_A_SAP_ROLE_16391','RS_UR_A_SAP_ROLE_16392','RS_UR_A_SAP_ROLE_16393','RS_UR_A_SAP_ROLE_16394','RS_UR_A_SAP_ROLE_16395','RS_UR_A_SAP_ROLE_16396','RS_UR_A_SAP_ROLE_16397','RS_UR_A_SAP_ROLE_16398','RS_UR_A_SAP_ROLE_16399','RS_UR_A_SAP_ROLE_16400','RS_UR_A_SAP_ROLE_16401','RS_UR_A_SAP_ROLE_16406','RS_UR_A_SAP_ROLE_16407','RS_UR_A_SAP_ROLE_16408','RS_UR_A_SAP_ROLE_16409','RS_UR_A_SAP_ROLE_18074','RS_UR_A_SAP_ROLE_18075','RS_UR_A_SAP_ROLE_18076','RS_UR_A_SAP_ROLE_18077','RS_UR_A_SAP_ROLE_18079','RS_UR_A_SAP_ROLE_18127','RS_UR_A_SAP_ROLE_18128','RS_UR_A_SAP_ROLE_18129','RS_UR_A_SAP_ROLE_18130','RS_UR_A_SAP_ROLE_18132','RS_UR_A_SAP_ROLE_18156_2','RS_UR_A_SAP_ROLE_18157_1','RS_UR_A_SAP_ROLE_18157_2','RS_UR_A_SAP_ROLE_18157','RS_UR_A_SAP_ROLE_18158_1','RS_UR_A_SAP_ROLE_18158_2','RS_UR_A_SAP_ROLE_18158','RS_UR_A_SAP_ROLE_18159_1','RS_UR_A_SAP_ROLE_18159_2','RS_UR_A_SAP_ROLE_18159','RS_UR_A_SAP_ROLE_18161_2','RS_UR_A_SAP_ROLE_18177_1','RS_UR_A_SAP_ROLE_18177_2','RS_UR_A_SAP_ROLE_18177','RS_UR_A_SAP_ROLE_18178_1','RS_UR_A_SAP_ROLE_18178_2','RS_UR_A_SAP_ROLE_18178','RS_UR_A_SAP_ROLE_18179_1','RS_UR_A_SAP_ROLE_18179_2','RS_UR_A_SAP_ROLE_18179','RS_UR_A_SAP_ROLE_18180_1','RS_UR_A_SAP_ROLE_18180_2','RS_UR_A_SAP_ROLE_18180','RS_UR_A_SAP_ROLE_18182_2','RS_UR_A_SAP_ROLE_18198','RS_UR_A_SAP_ROLE_18199','RS_UR_A_SAP_ROLE_18200','RS_UR_A_SAP_ROLE_18201','RS_UR_A_SAP_ROLE_18203','RS_UR_A_SAP_ROLE_18590','RS_UR_A_SAP_ROLE_18674','RS_UR_A_SAP_ROLE_18675','RS_UR_A_SAP_ROLE_18676','RS_UR_A_SAP_ROLE_18677','RS_UR_A_SAP_ROLE_18696','RS_UR_A_SAP_ROLE_18700','RS_UR_A_SAP_ROLE_18704','RS_UR_A_SAP_ROLE_18708','RS_UR_A_SAP_ROLE_18716','RS_UR_A_SAP_ROLE_18747','RS_UR_A_SAP_ROLE_18748','RS_UR_A_SAP_ROLE_18749','RS_UR_A_SAP_ROLE_18750','RS_UR_A_SAP_ROLE_18752','RS_UR_A_SAP_ROLE_18768','RS_UR_A_SAP_ROLE_18769','RS_UR_A_SAP_ROLE_18770','RS_UR_A_SAP_ROLE_18771','RS_UR_A_SAP_ROLE_18773','RS_UR_A_SAP_ROLE_18849','RS_UR_A_SAP_ROLE_18850','RS_UR_A_SAP_ROLE_18851','RS_UR_A_SAP_ROLE_18852','RS_UR_A_SAP_ROLE_18854','RS_UR_A_SAP_ROLE_19824_1','RS_UR_A_SAP_ROLE_19824_2','RS_UR_A_SAP_ROLE_19824_3','RS_UR_A_SAP_ROLE_19826_1','RS_UR_A_SAP_ROLE_19826_2','RS_UR_A_SAP_ROLE_19826_3','RS_UR_A_SAP_ROLE_19827_1','RS_UR_A_SAP_ROLE_19827_2','RS_UR_A_SAP_ROLE_19827_3','RS_UR_A_SAP_ROLE_19828_1','RS_UR_A_SAP_ROLE_19828_2','RS_UR_A_SAP_ROLE_19828_3','RS_UR_A_SAP_ROLE_19829_1','RS_UR_A_SAP_ROLE_19829_3','RS_UR_A_SAP_ROLE_19830_1','RS_UR_A_SAP_ROLE_19830_2','RS_UR_A_SAP_ROLE_19830_3','RS_UR_A_SAP_ROLE_19833_1','RS_UR_A_SAP_ROLE_19833_2','RS_UR_A_SAP_ROLE_19833_3','RS_UR_A_SAP_ROLE_19834_1','RS_UR_A_SAP_ROLE_19834_2','RS_UR_A_SAP_ROLE_19834_3','RS_UR_A_SAP_ROLE_19835_1','RS_UR_A_SAP_ROLE_19835_2','RS_UR_A_SAP_ROLE_19835_3','RS_UR_A_SAP_ROLE_19836_1','RS_UR_A_SAP_ROLE_19836_3','RS_UR_A_SAP_ROLE_19837_1','RS_UR_A_SAP_ROLE_19837_2','RS_UR_A_SAP_ROLE_19837_3','RS_UR_A_SAP_ROLE_19838_1','RS_UR_A_SAP_ROLE_19838_2','RS_UR_A_SAP_ROLE_19838_3','RS_UR_A_SAP_ROLE_19839_1','RS_UR_A_SAP_ROLE_19839_3','RS_UR_A_SAP_ROLE_19843_1','RS_UR_A_SAP_ROLE_19843_2','RS_UR_A_SAP_ROLE_19843_3','RS_UR_A_SAP_ROLE_19844_1','RS_UR_A_SAP_ROLE_19844_2','RS_UR_A_SAP_ROLE_19844_3','RS_UR_A_SAP_ROLE_19848_1','RS_UR_A_SAP_ROLE_19848_2','RS_UR_A_SAP_ROLE_19848_3','RS_UR_A_SAP_ROLE_19849_1','RS_UR_A_SAP_ROLE_19849_3','RS_UR_A_SAP_ROLE_19853_1','RS_UR_A_SAP_ROLE_19853_2','RS_UR_A_SAP_ROLE_19853_3','RS_UR_A_SAP_ROLE_19857_1','RS_UR_A_SAP_ROLE_19857_2','RS_UR_A_SAP_ROLE_19857_3','RS_UR_A_SAP_ROLE_19861_1','RS_UR_A_SAP_ROLE_19861_2','RS_UR_A_SAP_ROLE_19861_3','RS_UR_A_SAP_ROLE_19867_1','RS_UR_A_SAP_ROLE_19867_2','RS_UR_A_SAP_ROLE_19867_3','RS_UR_A_SAP_ROLE_19907_1','RS_UR_A_SAP_ROLE_19907_2','RS_UR_A_SAP_ROLE_19907_3','RS_UR_A_SAP_ROLE_19908_1','RS_UR_A_SAP_ROLE_19908_3','RS_UR_A_SAP_ROLE_19909_1','RS_UR_A_SAP_ROLE_19909_2','RS_UR_A_SAP_ROLE_19909_3','RS_UR_A_SAP_ROLE_19910_1','RS_UR_A_SAP_ROLE_19910_2','RS_UR_A_SAP_ROLE_19910_3','RS_UR_A_SAP_ROLE_19911_1','RS_UR_A_SAP_ROLE_19911_2','RS_UR_A_SAP_ROLE_19911_3','RS_UR_A_SAP_ROLE_19912_1','RS_UR_A_SAP_ROLE_19912_2','RS_UR_A_SAP_ROLE_19912_3','RS_UR_A_SAP_ROLE_19913_1','RS_UR_A_SAP_ROLE_19913_2','RS_UR_A_SAP_ROLE_19913_3','RS_UR_A_SAP_ROLE_19914_1','RS_UR_A_SAP_ROLE_19914_2','RS_UR_A_SAP_ROLE_19914_3','RS_UR_A_SAP_ROLE_19915_1','RS_UR_A_SAP_ROLE_19915_2','RS_UR_A_SAP_ROLE_19915_3','RS_UR_A_SAP_ROLE_19916_1','RS_UR_A_SAP_ROLE_19916_2','RS_UR_A_SAP_ROLE_19916_3','RS_UR_A_SAP_ROLE_19917_1','RS_UR_A_SAP_ROLE_19917_2','RS_UR_A_SAP_ROLE_19917_3','RS_UR_A_SAP_ROLE_19918_1','RS_UR_A_SAP_ROLE_19918_2','RS_UR_A_SAP_ROLE_19918_3','RS_UR_A_SAP_ROLE_19919_1','RS_UR_A_SAP_ROLE_19919_2','RS_UR_A_SAP_ROLE_19919_3','RS_UR_A_SAP_ROLE_19920_1','RS_UR_A_SAP_ROLE_19920_2','RS_UR_A_SAP_ROLE_19920_3','RS_UR_A_SAP_ROLE_19921_1','RS_UR_A_SAP_ROLE_19921_2','RS_UR_A_SAP_ROLE_19921_3','RS_UR_A_SAP_ROLE_19922_1','RS_UR_A_SAP_ROLE_19922_2','RS_UR_A_SAP_ROLE_19922_3','RS_UR_A_SAP_ROLE_19923_1','RS_UR_A_SAP_ROLE_19923_2','RS_UR_A_SAP_ROLE_19923_3','RS_UR_A_SAP_ROLE_19924_1','RS_UR_A_SAP_ROLE_19924_2','RS_UR_A_SAP_ROLE_19924_3','RS_UR_A_SAP_ROLE_19925_1','RS_UR_A_SAP_ROLE_19925_2','RS_UR_A_SAP_ROLE_19925_3','RS_UR_A_SAP_ROLE_19926_1','RS_UR_A_SAP_ROLE_19926_2','RS_UR_A_SAP_ROLE_19926_3','RS_UR_A_SAP_ROLE_19927_1','RS_UR_A_SAP_ROLE_19927_2','RS_UR_A_SAP_ROLE_19927_3','RS_UR_A_SAP_ROLE_19928_1','RS_UR_A_SAP_ROLE_19928_2','RS_UR_A_SAP_ROLE_19928_3','RS_UR_A_SAP_ROLE_19929_1','RS_UR_A_SAP_ROLE_19929_2','RS_UR_A_SAP_ROLE_19929_3','RS_UR_A_SAP_ROLE_19930_1','RS_UR_A_SAP_ROLE_19930_2','RS_UR_A_SAP_ROLE_19930_3','RS_UR_A_SAP_ROLE_19931_1','RS_UR_A_SAP_ROLE_19931_2','RS_UR_A_SAP_ROLE_19931_3','RS_UR_A_SAP_ROLE_19932_1','RS_UR_A_SAP_ROLE_19932_2','RS_UR_A_SAP_ROLE_19932_3','RS_UR_A_SAP_ROLE_19933_1','RS_UR_A_SAP_ROLE_19933_3','RS_UR_A_SAP_ROLE_19934_1','RS_UR_A_SAP_ROLE_19934_2','RS_UR_A_SAP_ROLE_19934_3','RS_UR_A_SAP_ROLE_19935_1','RS_UR_A_SAP_ROLE_19935_2','RS_UR_A_SAP_ROLE_19935_3','RS_UR_A_SAP_ROLE_19936_1','RS_UR_A_SAP_ROLE_19936_3','RS_UR_A_SAP_ROLE_19937_1','RS_UR_A_SAP_ROLE_19937_2','RS_UR_A_SAP_ROLE_19937_3','RS_UR_A_SAP_ROLE_19938_1','RS_UR_A_SAP_ROLE_19938_2','RS_UR_A_SAP_ROLE_19938_3','RS_UR_A_SAP_ROLE_19939_1','RS_UR_A_SAP_ROLE_19939_2','RS_UR_A_SAP_ROLE_19939_3','RS_UR_A_SAP_ROLE_19940_1','RS_UR_A_SAP_ROLE_19940_2','RS_UR_A_SAP_ROLE_19940_3','RS_UR_A_SAP_ROLE_19941_1','RS_UR_A_SAP_ROLE_19941_2','RS_UR_A_SAP_ROLE_19941_3','RS_UR_A_SAP_ROLE_19942_1','RS_UR_A_SAP_ROLE_19942_2','RS_UR_A_SAP_ROLE_19942_3','RS_UR_A_SAP_ROLE_19943_1','RS_UR_A_SAP_ROLE_19943_2','RS_UR_A_SAP_ROLE_19943_3','RS_UR_A_SAP_ROLE_19944_34','RS_UR_A_SAP_ROLE_19945_1','RS_UR_A_SAP_ROLE_19945_2','RS_UR_A_SAP_ROLE_19945_3','RS_UR_A_SAP_ROLE_19946_1','RS_UR_A_SAP_ROLE_19946_2','RS_UR_A_SAP_ROLE_19946_3','RS_UR_A_SAP_ROLE_19947_1','RS_UR_A_SAP_ROLE_19947_3','RS_UR_A_SAP_ROLE_19948_1','RS_UR_A_SAP_ROLE_19948_2','RS_UR_A_SAP_ROLE_19948_3','RS_UR_A_SAP_ROLE_19958_1','RS_UR_A_SAP_ROLE_19958_2','RS_UR_A_SAP_ROLE_19958_3','RS_UR_A_SAP_ROLE_19959_1','RS_UR_A_SAP_ROLE_19959_2','RS_UR_A_SAP_ROLE_19959_3','RS_UR_A_SAP_ROLE_19960_1','RS_UR_A_SAP_ROLE_19960_2','RS_UR_A_SAP_ROLE_19960_3','RS_UR_A_SAP_ROLE_19961_1','RS_UR_A_SAP_ROLE_19961_2','RS_UR_A_SAP_ROLE_19961_3','RS_UR_A_SAP_ROLE_19962_1','RS_UR_A_SAP_ROLE_19962_2','RS_UR_A_SAP_ROLE_19962_3','RS_UR_A_SAP_ROLE_19963_1','RS_UR_A_SAP_ROLE_19963_2','RS_UR_A_SAP_ROLE_19963_3','RS_UR_A_SAP_ROLE_19964_1','RS_UR_A_SAP_ROLE_19964_2','RS_UR_A_SAP_ROLE_19964_3','RS_UR_A_SAP_ROLE_19965_1','RS_UR_A_SAP_ROLE_19965_2','RS_UR_A_SAP_ROLE_19965_3','RS_UR_A_SAP_ROLE_19966_1','RS_UR_A_SAP_ROLE_19966_2','RS_UR_A_SAP_ROLE_19966_3','RS_UR_A_SAP_ROLE_19967_1','RS_UR_A_SAP_ROLE_19967_2','RS_UR_A_SAP_ROLE_19967_3','RS_UR_A_SAP_ROLE_19968_1','RS_UR_A_SAP_ROLE_19968_2','RS_UR_A_SAP_ROLE_19968_3','RS_UR_A_SAP_ROLE_19969_1','RS_UR_A_SAP_ROLE_19969_2','RS_UR_A_SAP_ROLE_19969_3','RS_UR_A_SAP_ROLE_19970_1','RS_UR_A_SAP_ROLE_19970_2','RS_UR_A_SAP_ROLE_19970_3','RS_UR_A_SAP_ROLE_19971_1','RS_UR_A_SAP_ROLE_19971_2','RS_UR_A_SAP_ROLE_19971_3','RS_UR_A_SAP_ROLE_19972_1','RS_UR_A_SAP_ROLE_19972_2','RS_UR_A_SAP_ROLE_19972_3','RS_UR_A_SAP_ROLE_19973_1','RS_UR_A_SAP_ROLE_19973_3','RS_UR_A_SAP_ROLE_19974_1','RS_UR_A_SAP_ROLE_19974_3','RS_UR_A_SAP_ROLE_19975_1','RS_UR_A_SAP_ROLE_19975_2','RS_UR_A_SAP_ROLE_19975_3','RS_UR_A_SAP_ROLE_19976_1','RS_UR_A_SAP_ROLE_19976_2','RS_UR_A_SAP_ROLE_19976_3','RS_UR_A_SAP_ROLE_19977_1','RS_UR_A_SAP_ROLE_19977_2','RS_UR_A_SAP_ROLE_19977_3','RS_UR_A_SAP_ROLE_19978_1','RS_UR_A_SAP_ROLE_19978_2','RS_UR_A_SAP_ROLE_19978_3','RS_UR_A_SAP_ROLE_19979_1','RS_UR_A_SAP_ROLE_19979_2','RS_UR_A_SAP_ROLE_19979_3','RS_UR_A_SAP_ROLE_19980_1','RS_UR_A_SAP_ROLE_19980_2','RS_UR_A_SAP_ROLE_19980_3','RS_UR_A_SAP_ROLE_19981_1','RS_UR_A_SAP_ROLE_19981_2','RS_UR_A_SAP_ROLE_19981_3','RS_UR_A_SAP_ROLE_19982_1','RS_UR_A_SAP_ROLE_19982_2','RS_UR_A_SAP_ROLE_19982_3','RS_UR_A_SAP_ROLE_19983_1','RS_UR_A_SAP_ROLE_19983_2','RS_UR_A_SAP_ROLE_19983_3','RS_UR_A_SAP_ROLE_19984_1','RS_UR_A_SAP_ROLE_19984_2','RS_UR_A_SAP_ROLE_19984_3','RS_UR_A_SAP_ROLE_19985_1','RS_UR_A_SAP_ROLE_19985_2','RS_UR_A_SAP_ROLE_19985_3','RS_UR_A_SAP_ROLE_19986_1','RS_UR_A_SAP_ROLE_19986_2','RS_UR_A_SAP_ROLE_19986_3','RS_UR_A_SAP_ROLE_19987_1','RS_UR_A_SAP_ROLE_19987_2','RS_UR_A_SAP_ROLE_19987_3','RS_UR_A_SAP_ROLE_19988_1','RS_UR_A_SAP_ROLE_19988_2','RS_UR_A_SAP_ROLE_19988_3','RS_UR_A_SAP_ROLE_19989_1','RS_UR_A_SAP_ROLE_19989_2','RS_UR_A_SAP_ROLE_19989_3','RS_UR_A_SAP_ROLE_19990_1','RS_UR_A_SAP_ROLE_19990_2','RS_UR_A_SAP_ROLE_19990_3','RS_UR_A_SAP_ROLE_19991_1','RS_UR_A_SAP_ROLE_19991_2','RS_UR_A_SAP_ROLE_19991_3','RS_UR_A_SAP_ROLE_19992_1','RS_UR_A_SAP_ROLE_19992_2','RS_UR_A_SAP_ROLE_19992_3','RS_UR_A_SAP_ROLE_19993_1','RS_UR_A_SAP_ROLE_19993_2','RS_UR_A_SAP_ROLE_19993_3','RS_UR_A_SAP_ROLE_19994_1','RS_UR_A_SAP_ROLE_19994_2','RS_UR_A_SAP_ROLE_19994_3','RS_UR_A_SAP_ROLE_19995_1','RS_UR_A_SAP_ROLE_19995_2','RS_UR_A_SAP_ROLE_19995_3','RS_UR_A_SAP_ROLE_19996_1','RS_UR_A_SAP_ROLE_19996_2','RS_UR_A_SAP_ROLE_19996_3','RS_UR_A_SAP_ROLE_19997_1','RS_UR_A_SAP_ROLE_19997_2','RS_UR_A_SAP_ROLE_19997_3','RS_UR_A_SAP_ROLE_19998_1','RS_UR_A_SAP_ROLE_19998_2','RS_UR_A_SAP_ROLE_19998_3','RS_UR_A_SAP_ROLE_19999_1','RS_UR_A_SAP_ROLE_19999_2','RS_UR_A_SAP_ROLE_19999_3','RS_UR_A_SAP_ROLE_20000_1','RS_UR_A_SAP_ROLE_20000_2','RS_UR_A_SAP_ROLE_20000_3','RS_UR_A_SAP_ROLE_20001_1','RS_UR_A_SAP_ROLE_20001_2','RS_UR_A_SAP_ROLE_20001_3','RS_UR_A_SAP_ROLE_20002_1','RS_UR_A_SAP_ROLE_20003_1','RS_UR_A_SAP_ROLE_20003_2','RS_UR_A_SAP_ROLE_20003_3','RS_UR_A_SAP_ROLE_20004_1','RS_UR_A_SAP_ROLE_20004_3','RS_UR_A_SAP_ROLE_20005_1','RS_UR_A_SAP_ROLE_20005_2','RS_UR_A_SAP_ROLE_20005_3','RS_UR_A_SAP_ROLE_20006_1','RS_UR_A_SAP_ROLE_20006_3','RS_UR_A_SAP_ROLE_20007_1','RS_UR_A_SAP_ROLE_20007_3','RS_UR_A_SAP_ROLE_20008_1','RS_UR_A_SAP_ROLE_20008_3','RS_UR_A_SAP_ROLE_20009_1','RS_UR_A_SAP_ROLE_20009_35','RS_UR_A_SAP_ROLE_20010_1','RS_UR_A_SAP_ROLE_20010_2','RS_UR_A_SAP_ROLE_20010_36','RS_UR_A_SAP_ROLE_20011_1','RS_UR_A_SAP_ROLE_20011_3','RS_UR_A_SAP_ROLE_20012_1','RS_UR_A_SAP_ROLE_20012_2','RS_UR_A_SAP_ROLE_20012_3','RS_UR_A_SAP_ROLE_20013_1','RS_UR_A_SAP_ROLE_20013_2','RS_UR_A_SAP_ROLE_20013_3','RS_UR_A_SAP_ROLE_20014_1','RS_UR_A_SAP_ROLE_20014_2','RS_UR_A_SAP_ROLE_20014_3','RS_UR_A_SAP_ROLE_20015_1','RS_UR_A_SAP_ROLE_20015_2','RS_UR_A_SAP_ROLE_20015_3','RS_UR_A_SAP_ROLE_20016_1','RS_UR_A_SAP_ROLE_20016_2','RS_UR_A_SAP_ROLE_20016_3','RS_UR_A_SAP_ROLE_20262_1','RS_UR_A_SAP_ROLE_20262_2','RS_UR_A_SAP_ROLE_20262_3','RS_UR_A_SAP_ROLE_20263_1','RS_UR_A_SAP_ROLE_20263_2','RS_UR_A_SAP_ROLE_20263_3','RS_UR_A_SAP_ROLE_20264_1','RS_UR_A_SAP_ROLE_20264_2','RS_UR_A_SAP_ROLE_20264_3','RS_UR_A_SAP_ROLE_20265_1','RS_UR_A_SAP_ROLE_20265_2','RS_UR_A_SAP_ROLE_20265_3','RS_UR_A_SAP_ROLE_20266_1','RS_UR_A_SAP_ROLE_20266_2','RS_UR_A_SAP_ROLE_20266_3','RS_UR_A_SAP_ROLE_20267_1','RS_UR_A_SAP_ROLE_20267_2','RS_UR_A_SAP_ROLE_20267_3','RS_UR_A_SAP_ROLE_20268_1','RS_UR_A_SAP_ROLE_20268_2','RS_UR_A_SAP_ROLE_20268_3','RS_UR_A_SAP_ROLE_20269_1','RS_UR_A_SAP_ROLE_20269_2','RS_UR_A_SAP_ROLE_20269_3','RS_UR_A_SAP_ROLE_20270_1','RS_UR_A_SAP_ROLE_20270_2','RS_UR_A_SAP_ROLE_20270_3','RS_UR_A_SAP_ROLE_20604_1','RS_UR_A_SAP_ROLE_20604_2','RS_UR_A_SAP_ROLE_20604_3','RS_UR_A_SAP_ROLE_20606_3','RS_UR_A_SAP_ROLE_20607_3','RS_UR_A_SAP_ROLE_20608_3','RS_UR_A_SAP_ROLE_20609_3','RS_UR_A_SAP_ROLE_20610_3','RS_UR_A_SAP_ROLE_20611_3','RS_UR_A_SAP_ROLE_20612_3','RS_UR_A_SAP_ROLE_20613_3','RS_UR_A_SAP_ROLE_20614_3','RS_UR_A_SAP_ROLE_20615_3','RS_UR_A_SAP_ROLE_20616_3','RS_UR_A_SAP_ROLE_20660_01','RS_UR_A_SAP_ROLE_20660_2','RS_UR_A_SAP_ROLE_20661_01','RS_UR_A_SAP_ROLE_20661_2','RS_UR_A_SAP_ROLE_20662_01','RS_UR_A_SAP_ROLE_20662_2','RS_UR_A_SAP_ROLE_20663_01','RS_UR_A_SAP_ROLE_20663_2','RS_UR_A_SAP_ROLE_20664_01','RS_UR_A_SAP_ROLE_20664_2','RS_UR_A_SAP_ROLE_20665_01','RS_UR_A_SAP_ROLE_20665_2','RS_UR_A_SAP_ROLE_20666_01','RS_UR_A_SAP_ROLE_20666_2','RS_UR_A_SAP_ROLE_20667_01','RS_UR_A_SAP_ROLE_20667_2','RS_UR_A_SAP_ROLE_20668_01','RS_UR_A_SAP_ROLE_20668_2','RS_UR_A_SAP_ROLE_20669_01','RS_UR_A_SAP_ROLE_20669_2','RS_UR_A_SAP_ROLE_20670_01','RS_UR_A_SAP_ROLE_20670_2','RS_UR_A_SAP_ROLE_20671_01','RS_UR_A_SAP_ROLE_20671_2','RS_UR_A_SAP_ROLE_20672_01','RS_UR_A_SAP_ROLE_20672_2','RS_UR_A_SAP_ROLE_20673_01','RS_UR_A_SAP_ROLE_20673_2','RS_UR_A_SAP_ROLE_20674_01','RS_UR_A_SAP_ROLE_20674_2','RS_UR_A_SAP_ROLE_20675_01','RS_UR_A_SAP_ROLE_20675_2','RS_UR_A_SAP_ROLE_20676_01','RS_UR_A_SAP_ROLE_20676_2','RS_UR_A_SAP_ROLE_20677_01','RS_UR_A_SAP_ROLE_20677_2','RS_UR_A_SAP_ROLE_20678_01','RS_UR_A_SAP_ROLE_20678_2','RS_UR_A_SAP_ROLE_20679_01','RS_UR_A_SAP_ROLE_20679_2','RS_UR_A_SAP_ROLE_20680_01','RS_UR_A_SAP_ROLE_20680_2','RS_UR_A_SAP_ROLE_22224_1','RS_UR_A_SAP_ROLE_22224_2','RS_UR_A_SAP_ROLE_22225_1','RS_UR_A_SAP_ROLE_22225_2','RS_UR_A_SAP_ROLE_22226_1','RS_UR_A_SAP_ROLE_22226_2','RS_UR_A_SAP_ROLE_22333_4','RS_UR_A_SAP_ROLE_22333_5','RS_UR_A_SAP_ROLE_22333_6','RS_UR_A_SAP_ROLE_22334_4','RS_UR_A_SAP_ROLE_22334_5','RS_UR_A_SAP_ROLE_22334_6','RS_UR_A_SAP_ROLE_22335_4','RS_UR_A_SAP_ROLE_22335_5','RS_UR_A_SAP_ROLE_22335_6','RS_UR_A_SAP_ROLE_22412_1','RS_UR_A_SAP_ROLE_22412_2','RS_UR_A_SAP_ROLE_22609_1','RS_UR_A_SAP_ROLE_22610_1','RS_UR_A_SAP_ROLE_22611_1','RS_UR_A_SAP_ROLE_23791_1','RS_UR_A_SAP_ROLE_23791_2','RS_UR_A_SAP_ROLE_23792_1','RS_UR_A_SAP_ROLE_23792_2','RS_UR_A_SAP_SAP_ROLE_16546')";
                    String queryRole = "SELECT * FROM " + table;
                    String output = String.Empty;
                    List<String> updateValue = new List<string>();

                    
                    command = new SqlCommand(queryRole, cnn);
                    dataReader = command.ExecuteReader();

                    String queryRoleRemote = "SELECT TOP 1 * FROM " + destTables[tables.IndexOf(table)];
                    commandRemote = new SqlCommand(queryRoleRemote, cnnRemote);
                    dataReaderRemote = commandRemote.ExecuteReader();

                    var columns = Enumerable.Range(0, dataReader.FieldCount).Select(dataReader.GetName).ToList(); //gets all the columns from the table
                    var columnsRemote = Enumerable.Range(0, dataReaderRemote.FieldCount).Select(dataReaderRemote.GetName).ToList(); //gets all the columns from the table


                    var dt = new DataTable(); //NEW
                    foreach (var column in columnsRemote) //NEW
                        dt.Columns.Add(column.ToString()); //NEW
                    
                    //if (!dt.Columns.Contains("matchingValues"))
                    //    dt.Columns.Add("matchingValues");

                    int att = columns.IndexOf(attribute); // get´s the index of the attribute

                    int count = 0;
                    Console.WriteLine("Buffering change number: ");

                    
                    while (dataReader.Read())
                    {
                        output = dataReader.GetValue(att).ToString();

                        if (!String.IsNullOrEmpty(output))
                        {
                            String DN = dataReader.GetValue(0).ToString(); //DN MUST BE IN THE POSITION 0 IN ALL TABLES

                            //String[] outputArray = output.Split(";");
                            String[] outputArray = output.Split("|");
                            String valores = String.Empty;

                            //if  (  attribute == "ValeLastLogonDate")
                            //{

                            //    string treatedResource = string.Empty;
                            //    string treatedDN = string.Empty;

                            //    foreach (String valor in outputArray)
                            //    {
                            //        treatedDN = TrataValor(DN, String.Empty);
                            //        treatedResource = TrataValor(valor, attribute);
                            //        updateValue.Add("INSERT INTO " + destTables[tables.IndexOf(table)] + "(DN," + attribute + ", matchingValues) VALUES ('" + treatedDN + "','" + treatedResource + "','" + treatedDN + treatedResource + "')");
                            //        count++;
                            //        Console.Write("\r{0}   ", count);

                            //    }
                            //}

                            if (attribute == "DN")
                            {
                                method = SQLInputMethod.Bulk;
                                string cn = String.Empty;
                                string container = String.Empty;

                                foreach (String valor in outputArray)
                                {
                                    cn = GetCNFromDN(valor);
                                    container = GetContainerFromUserDN(valor);
                                    dt.Rows.Add(DN, cn, container);
                                    count++;

                                }

                                Console.Write("\r{0}   ", count);

                            }

                           else if (attribute == "DirXML_Accounts" ||  attribute == "DirXML-Associations" || attribute == "ValeDriverDN" || attribute == "EquivalentToMe" 
                                || attribute == "ValeChildRoles" || attribute == "nrfResource" || attribute == "nrfRole" || attribute == "nrfMemberOf"
                                || attribute == "nrfResourceHistory" || attribute == "ValeLoginDisabled" || attribute == "ValeLastLogonDate" || attribute == "ACL"
                                || attribute == "securityEquals")
                            {
                                method = SQLInputMethod.Bulk;
                                string treatedResource = string.Empty;
                                string treatedDN = string.Empty;

                                foreach (String valor in outputArray)
                                {
                                    treatedDN = TrataValor(DN, String.Empty);
                                    treatedResource = TrataValor(valor, attribute);
                                    dt.Rows.Add(treatedDN, treatedResource, treatedDN + treatedResource); //NEW
                                    count++;
                                }

                                Console.Write("\r{0}   ", count);

                            }

                            //else if (attribute == "nrfAssignedResources")
                            //{

                            //    method = SQLInputMethod.Bulk;
                            //    string treatedResource = string.Empty;
                            //    string resourceDN = string.Empty;
                            //    string Resource_Base64 = string.Empty;
                            //    string valueWithNewDriverSet = string.Empty;

                            //    foreach (String valor in outputArray)
                            //    {
                                                                
                            //        treatedResource = TrataValor(valor, attribute);
                            //        //resourceDN = valor.Substring(3, valor.IndexOf(",") - 3);
                            //        resourceDN = GetResourceDNFromnrfAssignedResource(treatedResource);
                            //        valueWithNewDriverSet = valor.Replace("VALE_IDV_DR_SET", "DriverSet");
                            //        Resource_Base64 = EncodeToBase64(valueWithNewDriverSet);
                            //        dt.Rows.Add(DN, string.Empty, valor, treatedResource, DN + resourceDN, resourceDN, Resource_Base64); //NEW
                            //        //dt.Rows.Add(DN, string.Empty, treatedResource, treatedResource, DN + treatedResource, resourceDN); //NEW
                            //        count++;
                            //    }

                            //    Console.Write("\r{0}   ", count);
                            //}

                            

                            else if (attribute == "nrfAssignedRoles")
                            {
                                method = SQLInputMethod.Bulk;

                                //string attToBeUpdate = "nrfAssignedRoles_Equalized";
                                string treatedResource = string.Empty;
                                string roleDN = string.Empty;
                                List<string> details;
 

                                foreach (String valor in outputArray)
                                {
                                    
                                    treatedResource = TrataValor(valor, attribute);
                                    roleDN = GetRoleDNFromnrfAssignedRole(treatedResource);
                                    details = GetDetailsFromnrfAssignedRole(valor);
                                    //dt.Rows.Add(DN, treatedResource, DN + treatedResource, treatedResource, roleDN, details[0], details[1], details[2], details[3], DN + roleDN); //NEW
                                    dt.Rows.Add(DN, treatedResource, DN + roleDN, treatedResource, roleDN, details[0], details[1], details[2], details[3], DN + roleDN); //NEW
                                    count++;
                                    Console.Write("\r{0}   ", count);

                                }
                            }

        

                            else if (attribute == "EntitlementRef")
                            {
                                method = SQLInputMethod.Bulk;

                                string treatedResource = string.Empty;
                                string application = string.Empty;
                                string param = string.Empty;
                                string cn = string.Empty;
                                string status = string.Empty;

                                //foreach (String valor in outputArray)
                                //{
                                    
                                //    treatedResource = TrataValor(valor, attribute);
                                //    application = GetAppFromEntitlement(valor);
                                //    updateValue.Add("INSERT INTO " + destTables[tables.IndexOf(table)] + "(DN," + attribute + ", matchingValues, application) VALUES ('" + DN + "','" + treatedResource + "','" + DN + treatedResource + "','" + application + "')");
                                //    count++;
                                //    Console.Write("\r{0}   ", count);
                                //}

                               
                                foreach (String valor in outputArray)
                                {
                                    treatedResource = TrataValor(valor, attribute);
                                    application = GetAppFromEntitlement(valor);
                                    param = GetEntitlementParam(valor);
                                    cn = GetEntitlementCN(valor);
                                    if (valor.Contains("#0#"))
                                        status = "Disabled";
                                    else if (valor.Contains("#1#"))
                                        status = "Enabled";
                                    dt.Rows.Add(DN, valor, treatedResource, DN + treatedResource, application, param, cn, status); //NEW
                                    count++;
                                    Console.Write("\r{0}   ", count);

                                }


                            }

                            else if (attribute == "nrfEntitlementRef")
                            {
                                method = SQLInputMethod.Bulk;

                                string treatedResource = string.Empty;
                                string entitlementCN = string.Empty;
                                string entitlementParam = string.Empty;
                                string DN_Equalized = string.Empty;
                              


                                foreach (String valor in outputArray)
                                {
                                    DN_Equalized = TrataValor(DN, "nrfResource");
                                    treatedResource = TrataValor(valor, attribute);
                                    entitlementCN = GetEntitlementCN(treatedResource);
                                    entitlementParam = GetEntitlementParam(treatedResource);
                                    
                                    dt.Rows.Add(DN_Equalized, treatedResource, DN_Equalized + treatedResource, entitlementCN, entitlementParam); //NEW
                                    count++;
                                    Console.Write("\r{0}   ", count);

                                }



                            }



                            else if (attribute == "groupMembership")
                            {

                                //string application = string.Empty;

                                //foreach (String valor in outputArray)
                                //{
                                //    //string treatedResource = string.Empty;
                                //    //treatedResource = TrataValor(valor, attribute);
                                //    //application = GetAppFromEntitlement(valor);
                                //    updateValue.Add("INSERT INTO " + destTables[tables.IndexOf(table)] + "(DN," + attribute + ", matchingValues) VALUES ('" + DN + "','" + valor + "','" + DN + valor + "')");
                                //    count++;
                                //    Console.Write("\r{0}   ", count);
                                //}

                                method = SQLInputMethod.Bulk;

                                foreach (String valor in outputArray)
                                {
                                    
                                    //application = GetAppFromEntitlement(valor);
                                    dt.Rows.Add(DN, valor, DN + valor); //NEW
                                    count++;
                                    Console.Write("\r{0}   ", count);

                                }

                            }

                            else if (attribute == "Member")
                            {


                                //foreach (String valor in outputArray)
                                //{

                                //    updateValue.Add("INSERT INTO " + destTables[tables.IndexOf(table)] + "(DN," + attribute + ", matchingValues) VALUES ('" + DN + "','" + valor + "','" + DN + valor + "')");
                                //    count++;
                                //    Console.Write("\r{0}   ", count);

                                //}



                                method = SQLInputMethod.Bulk;

                                foreach (String valor in outputArray)
                                {
                                    dt.Rows.Add(DN, valor, DN + valor); //NEW
                                    count++;
                                }

                                Console.Write("\r{0}   ", count);
                            }

                        }

                    }

                    dataReader.Close();
                    command.Dispose();

                    dataReaderRemote.Close();
                    commandRemote.Dispose();

                  

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    int count2 = 0;


                    cnnRemote = Connection.DBConnection(@"Data Source=iamupgradeanalysis.ctvkpsgubutg.us-east-1.rds.amazonaws.com;Initial Catalog=IAM;Persist Security Info=True;User ID=admin;Password=jyE1EmejQt4LNLLo9Q8o");

                    //String queryRoleDest = "SELECT * FROM " + destTables[tables.IndexOf(table)];

                    //command = new SqlCommand(queryRoleDest, cnnRemote);                


                    

                    Console.WriteLine("\n\nUpdating change number: ");

                    if (method == SQLInputMethod.Bulk)
                        BulkInSQL(cnnRemote, tables.IndexOf(table), destTables, dt, count);

                    else
                    {

                        foreach (String valor in updateValue) //update the remote tables
                        {
                            adapter.UpdateCommand = new SqlCommand(valor, cnnRemote);
                            adapter.UpdateCommand.ExecuteNonQuery();
                            count2++;
                            Console.Write("\r{0}   ", count2);
                        }
                    }

                }

            }

            cnn.Close();
        }

        public static void CheckMissingAssociation (List<String> tables)
        {

            SqlConnection cnn = new SqlConnection();
            cnn = Connection.DBConnection(@"Data Source=" + System.Environment.MachineName + ";Initial Catalog=IAM;Integrated Security=True");

            SqlCommand command;
            SqlDataReader dataReader;

            foreach (string table in tables)
            {
                Console.WriteLine("\nWorking with table " + table);

                String queryRole = "SELECT * FROM " + table;
                String output = String.Empty;
                List<String> updateValue = new List<string>();

                command = new SqlCommand(queryRole, cnn);
                dataReader = command.ExecuteReader();

                int count = 0;
                string attribute = "comments";
                string result = string.Empty;
                Console.WriteLine("Buffering change number: ");
                while (dataReader.Read())
                {
                    output = dataReader.GetValue(1).ToString(); //obtem Association
                    if (!String.IsNullOrEmpty(output))
                    {
                        String DN = dataReader.GetValue(0).ToString(); //DN MUST BE IN THE POSITION 0 IN ALL TABLES

                        if (output.Contains("DRV-eDirectory")) // se contem associação
                        {
                            result = "Contains eDir association";
                        }

                        else 
                        {
                            result = "Does Not Contains eDir association";
                        }


                        updateValue.Add("UPDATE " + table + " SET " + attribute + " = '" + result + "'" + " WHERE DN = '" + DN + "'");
                        count++;
                        //Console.WriteLine(count);
                        Console.Write("\r{0}   ", count);
                    }
                }

                dataReader.Close();
                command.Dispose();

                SqlDataAdapter adapter = new SqlDataAdapter();
                int count2 = 0;
                Console.WriteLine("\n\nUpdating change number: ");
                foreach (String valor in updateValue)
                {
                    //commandUpdate = new SqlCommand(valor, cnn);
                    adapter.UpdateCommand = new SqlCommand(valor, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    count2++;
                    Console.Write("\r{0}   ", count2);
                    //Console.Write("\r \r" + new string(' ', Console.WindowWidth) + "\r");
                    //Console.SetCursorPosition(0, Console.CursorTop - 1);
                    //ClearCurrentConsoleLine();

                }
            }

            cnn.Close();
        }
        public static void BulkInSQL (SqlConnection cnnRemote, int tableIndex, List<string> destTables, DataTable dt, int count)
        {
            using (var sqlBulk = new SqlBulkCopy(cnnRemote))
            {
                sqlBulk.BulkCopyTimeout = 36000; //1 hour timeout 
                sqlBulk.NotifyAfter = 1000;
                sqlBulk.SqlRowsCopied += (sender, eventArgs) => Console.Write("\r{0}", eventArgs.RowsCopied);
                sqlBulk.DestinationTableName = destTables[tableIndex];
                sqlBulk.WriteToServer(dt);

            }

            Console.Write("\r{0}{1}", count, "\n");

        }

        public static String TrataValor(String value, string attType)
        {
            String result = String.Empty;

            //transformation rules

            if (attType == "DirXML-Associations" && value.Contains("DRV-eDirectoryIDV")) //if the association is related to the eDir to eDir driver, remove the GUID once will be different between the environments
                value = value.Substring(3, value.IndexOf("#") - 3);

            value = value.Replace("DRV-eDirectoryAuth", "DRV-eDirectory");
            value = value.Replace("DRV-eDirectoryIDV", "DRV-eDirectory");
            value = value.Replace("User Application", "UserApplication");
            value = value.Replace("Vale_IDV_DR_SET", "DriverSet");
            value = value.Replace("CURRIDV2IDV", "IDV2IDV");
            value = value.Replace("NEWIDV2IDV", "IDV2IDV");
            value = value.Replace("DriverSetSync", "DriverSet");
            value = value.Replace("\'", "\''");


            //if (attType == "nrfAssignedResources")
            //   result = value.Substring(0, value.IndexOf("<start_tm>")) + value.Substring(value.IndexOf("<req_desc>"), value.IndexOf("</assignment>") - value.IndexOf("<req_desc>") + 13);

            //else if (attType == "nrfAssignedRoles")
            //    result = value.Substring(0, value.IndexOf("o=vale", 0) + 6);


            //else
            if (attType == "EntitlementRef" || attType == "nrfEntitlementRef")
            {

                value = Regex.Replace(value, @"\s*(<[^>]+>)\s", "$1", RegexOptions.Singleline);
                //value = Regex.Replace(value, @"\s*(?<capture><(?<markUp>\w+)>.*<\/\k<markUp>>)\s*", "$1", RegexOptions.Singleline);

                //value = value.Replace(">  <", "><");
                //value = value.Replace("> <", "><");

                ////value. regex

                while (value.Contains("\n"))
                {
                    value = value.Replace("\n", "");
                }

                result = value;
            }

            else
                result = value;

            return result;
        }

        public static String TrataValorParaGerarLDIF (String value, string attType)
        {
            

            value = value.Replace("User Application", "UserApplication");
            value = value.Replace("Vale_IDV_DR_SET", "DriverSet");

            return value;
        }

        public static String GetCNFromDN(string value)
        {
            string result = String.Empty;

            if (value.Length > 0 & value.Contains(','))
            {
                result = value.Substring(3, value.IndexOf(',') -3);
            }

            return result;
        }

        public static String GetContainerFromUserDN(string value)
        {
            string result = String.Empty;

            if (value.Length > 0 & value.Contains(','))
            {
                result = value.Substring(value.IndexOf(',') + 1, value.Length - value.IndexOf(',') - 1);
            }

            return result;
        }

        public static String GetEntitlementCN (string value)
        {
            string result = "Verify";

            if (value.Length > 0 & value.Contains(','))
            {
                result = value.Substring(0, value.IndexOf('#'));
            }

            return result;
        }

        public static String GetEntitlementParam(string value)
        {
            string result = "Verify";

            if (value.Length > 0 & value.Contains(',') & value.Contains("<param>"))
            {
                result = value.Substring(value.IndexOf("<param>") + 7, value.IndexOf("</param>") - value.IndexOf("<param>") -7);
            }

            return result;
        }

        public static String EncodeToBase64(string value)
        {
            string result = "Empty";

           if (value != string.Empty)
            {
               var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
               result = System.Convert.ToBase64String(plainTextBytes);
            }

            return result;
        }

        public static String GetAppFromEntitlement(string value)
        {
            string result = "Verify";

            if (value.Length > 0 & value.Contains(','))
            {
                result = value.Substring(value.IndexOf(',') + 1, value.Length - value.IndexOf(',') - 1);
                result = result.Substring(3, result.IndexOf(',') - 3);
            }

            return result;
        }

        public static String GetRoleDNFromnrfAssignedRole (string value)
        {
            string result = string.Empty;

            if (value.Length > 0 & value.Contains(','))
            {
                result = value.Substring(0, value.IndexOf('#'));

            }

            return result;
        }

        public static String GetResourceDNFromnrfAssignedResource(string value)
        {
            string result = string.Empty;

            if (value.Length > 0 & value.Contains(','))
            {
                result = value.Substring(0, value.IndexOf('#'));

            }

            return result;
        }

        public static List<String> GetDetailsFromnrfAssignedRole(string value)
        {
            List<string> result = new List<string>();

            if (value.Length > 0 & value.Contains(','))
            {
                result.Add (value.Substring(value.IndexOf("<start_tm>") + 10,(value.IndexOf("</start_tm>") - value.IndexOf("<start_tm>"))-17)); //start time
                result.Add(value.Substring(value.IndexOf("<req_tm>") + 8, (value.IndexOf("</req_tm>") - value.IndexOf("<req_tm>")) - 15)); //request time
                result.Add(value.Substring(value.IndexOf("<req>") + 5, (value.IndexOf("</req>") - value.IndexOf("<req>")) - 5)); //requester
                result.Add(value.Substring(value.IndexOf("<req_desc>") + 10, (value.IndexOf("</req_desc>") - value.IndexOf("<req_desc>")) - 10)); //request description
            }

            return result;
        }
    }

 }


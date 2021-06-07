using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

///This class uses the ado.net OleDb classes to provide a connection to an sql server database.
///it is free for use by anybody so long as you give credit to the original author i.e me
///Matthew Dean mjdean@dmu.ac.uk De Montfort University 2013

public class clsDataConnectionOLDB
{
    //connection object used to connect to the database
    OleDbConnection connectionToDB = new OleDbConnection();
    //data adapter used to transfer data to and from the database
    OleDbDataAdapter dataChannel = new OleDbDataAdapter();
    //ado.net class for building the OleDb commands
    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder();
    //stores a list of all of the OleDb parameters
    List<OleDbParameter> OleDbParams = new List<OleDbParameter>();
    //data table used to store the results of the stored procedure
    DataTable dataTable = new DataTable();
    //string variable used to store the connection string
    private string connectionString;

    public clsDataConnectionOLDB()
    {
        //build up the connection string for the OleDb server database Visual Studio 2010
        //connectionString = "Data Source=.\\OleDbEXPRESS;AttachDbFilename=" + GetDBName() + ";Integrated Security=True;User Instance=True";
        //build up the connection string for the OleDb server database Visual Studio 2012
        //connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=" + GetDBName() + ";Integrated Security=True;Connect Timeout=30";
        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + GetDBName();
        //connectionString = "DSN=dbase;Uid=myUsername;Pwd=;";
    }

    

    public string GetDBName()
    {

        //array to store folder names
        string[] filePaths;
        //var to store index for path array
        Int32 PathArrayIndex = 0;
        string[] dirConts;
        //flag for found
        Boolean Found = false;
        Int32 Counter;
        List<string> DBNames = new List<string>();
        //get the folder for the project
        string BaseDir = TrimPath(System.AppDomain.CurrentDomain.BaseDirectory);
        do
        {
            //get the list of files in the folder
            filePaths = System.IO.Directory.GetDirectories(BaseDir);
            PathArrayIndex = 0;
            while (PathArrayIndex < filePaths.Length & Found == false)
            {
                //make path lowercase
                filePaths[PathArrayIndex] = filePaths[PathArrayIndex].ToLower();
                //if the file is not a system database file
                if (filePaths[PathArrayIndex].Contains("app_data") == true)
                {
                    //get the list of files in the folder
                    dirConts = System.IO.Directory.GetFiles(filePaths[PathArrayIndex], "*.mdb", System.IO.SearchOption.AllDirectories);
                    Counter = 0;
                    //while there are files to process
                    while (Counter < dirConts.Length)
                    {
                        //if the file is not a system database file
                        if (dirConts[Counter].Contains("ASPNETDB.MDF") == false)
                        {
                            //add the file to the list of db names
                            DBNames.Add(dirConts[Counter]);
                        }
                        //inc the counter
                        Counter++;
                    }
                    if (DBNames.Count == 1)
                    {
                        //flag found
                        Found = true;
                    }
                    else
                    {
                        //inc the counter
                        PathArrayIndex++;
                    }
                }
                else
                {
                    //inc the counter
                    PathArrayIndex++;
                }
            }
            if (Found == false)
            {
                //move up a path and try again
                BaseDir = TrimPath(BaseDir);
            }
        }
        while (BaseDir != "" & Found == false);
        //if one database name is found use that
        if (DBNames.Count == 1)
        {
            //return the full path of the db
            return DBNames[0];
        }
        //if no database is founjd throw an exception
        else if (DBNames.Count == 0)
        {
            throw new System.Exception("There is no database in your App_Data folder");
        }
        //if multiple database have been found throw an error
        else
        {
            throw new System.Exception("You have too many database files in your App_Data folder");
        }
    }

    private string TrimPath(string OldPath)
    {
        Int32 Posn = 0;
        //find the right most instance of \\
        Posn = OldPath.LastIndexOf("\\");
        if (Posn != -1)
        {
            //split the path at that point
            OldPath = OldPath.Substring(0, Posn);
        }
        else
        {
            OldPath = "";
        }
        return OldPath;
    }

    public void AddParameter(string ParamName, object ParamValue)
    ///public method allowing the addition of an OleDb parameter to the list of parameters
    ///it accepts two parameters the name of the parameter and its value
    {
        //create a new instance of the OleDb parameter object
        OleDbParameter AParam = new OleDbParameter(ParamName, ParamValue);
        //add the parameter to the list
        OleDbParams.Add(AParam);
    }

    public Int32 Execute(string SProcName)
    {
        ///public method used to execute the named stored procedure
        ///accepts one parameter which is the name of the stored procedure to use
        //open the stored procedure
        //initialise the connection to the database
        connectionToDB = new OleDbConnection(connectionString);
        //open the database
        connectionToDB.Open();
        //initialise the command builder for this connection
        OleDbCommand dataCommand = new OleDbCommand(SProcName, connectionToDB);
        //add the parameters to the command builder
        //loop through each parameter
        for (int Counter = 0; Counter < OleDbParams.Count; Counter += 1)
        {
            //add it to the command builder
            dataCommand.Parameters.Add(OleDbParams[Counter]);
        }
        ////create an instance of the OleDbParameter class
        //OleDbParameter returnValue = new OleDbParameter();
        ////set the direction as the return value
        //returnValue.Direction = ParameterDirection.ReturnValue;
        ////add this parameter to the Command builder
        //dataCommand.Parameters.Add(returnValue);
        //set the command type as stored procedure
        dataCommand.CommandType = CommandType.StoredProcedure;
        //initialise the data adapter
        dataChannel = new OleDbDataAdapter(SProcName, connectionToDB);
        //set the select command property for the data adapter
        dataChannel.SelectCommand = dataCommand;
        //use the copmmand builder to generate the OleDb insert delete etc
        commandBuilder = new OleDbCommandBuilder(dataChannel);
        //fill the data adapter
        dataChannel.Fill(dataTable);
        //try
        //{
        //    dataChannel.Fill(dataTable);
        //}
        //catch
        //{
        //    throw new System.Exception("Could not get the data.  Check that you have the correct name for your stored procedure.");
        //}
        //close the connection
        connectionToDB.Close();
        //return the result of the stored procedure
        return 0;//Convert.ToInt32(returnValue.Value);
    }

    public Int32 Count
    //property that returns the count of records in the query results
    {
        get
        {
            //return the count of the query results
            return dataTable.Rows.Count;
        }
    }

    public DataTable DataTable
    //public property that provides access to the query results
    {
        get
        {
            //return the query results
            return dataTable;
        }
        set
        {
            //set the query results
            dataTable = value;
        }
    }



}


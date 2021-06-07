using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

///<summary>
///Summary description for clsDataConnection
///</summary>
public class clsDataConnection
{

    //declare a connection object
    OleDbConnection mNewConnection;
    //declare a data adapter
    OleDbDataAdapter mDataAdapter;
    //declare data table member variable
    DataTable mTableData = new DataTable();
    //declare a data row object for use as blank record
    DataRow mARow;

    public Int32 Count
    {
        get
        {
            return mTableData.Rows.Count;
        }
    }

    public DataTable DataTable
    {
        get
        {
            return mTableData;
        }
    }

    public clsDataConnection(string SQL)
    {
        OpenDatabase(SQL);
    }

    private void OpenDatabase(string SQL)
    {
        //declare a new command builder
        OleDbCommandBuilder CB;
        //declare a variable to store the application path
        string DbPath = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\dvd.mdb";
        //this is the connection string to an Access 2003 file uncomment as reguired
        string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DbPath + ";Persist Security Info=False";
        //opens the connection object based on the connection string
        mNewConnection = new OleDbConnection(ConnectionString);
        try
        {
            //open the connection
            mNewConnection.Open();
        }
        catch
        {
            //Catch ex As Exception
            throw new SystemException("The database file could not be opened");
        }
        //connect the data adapter to the connection object using the specified sql
        mDataAdapter = new OleDbDataAdapter(SQL, mNewConnection);
        //initialise the select command of the data adapter
        mDataAdapter.SelectCommand = new OleDbCommand(SQL, mNewConnection);
        //initialise the command builder to initialis the other commands of the data adapter
        CB = new OleDbCommandBuilder(mDataAdapter);
        //Try
        try
        {
            //populate the datatable via the data adapter 
            mDataAdapter.Fill(mTableData);
        }
        catch
        {
            throw new SystemException("The SQL statement '" + SQL + "' contains errors.");
        }
        //take a copy of the record structure
        mARow = mTableData.NewRow();
        //close the connection
        mNewConnection.Close();
        //End Sub
    }

    private DataTable GetDataTable(string TableName)
    {
        //declare a new Data table
        DataTable DT = new DataTable();
        string SQL;
        //declare a new command builder
        OleDbCommandBuilder CB;
        SQL = "select * from " + TableName;
        //connect the data adapter to the connection object using the specified sql
        mDataAdapter = new OleDbDataAdapter(SQL, mNewConnection);
        //initilaise the select command of the data adapter
        mDataAdapter.SelectCommand = new OleDbCommand(SQL, mNewConnection);
        //ise the command builder to initialis the other commands of the data adapter
        CB = new OleDbCommandBuilder(mDataAdapter);
        //populate the datatable via the data adapter 
        mDataAdapter.Fill(DT);
        return DT;
    }

    public void AddNewRecord()
    {
        //add the new record to the datatable
        mTableData.Rows.Add(mARow);
        //create a new blank record
        mARow = mTableData.NewRow();
    }

    public void SaveChanges()
    {
        //save the changes to the database file
        mDataAdapter.Update(mTableData);
    }

    public DataRow GetFields()
    {
        DataRow ARow;
        ARow = mTableData.NewRow();
        return ARow;
    }

    public DataRow NewRecord
    {
        get
        {
            return mARow;
        }
    }

    public void RemoveRecord(Int32 Index)
    {
        DataTable.Rows[Index].Delete();
    }
}
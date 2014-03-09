using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Project
/// </summary>
public class Project
{
	public Project()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string Comment;

    public string Comment1
    {
        get { return Comment; }
        set { Comment = value; }
    }
    DateTime ExpirationDate;

    public DateTime ExpirationDate1
    {
        get { return ExpirationDate; }
        set { ExpirationDate = value; }
    }

    DateTime OpenedDate;

    public DateTime OpenedDate1
    {
        get { return OpenedDate; }
        set { OpenedDate = value; }
    }

    string ContractorName;

    public string ContractorName1
    {
        get { return ContractorName; }
        set { ContractorName = value; }
    }
    int ContractorPhone;

    public int ContractorPhone1
    {
        get { return ContractorPhone; }
        set { ContractorPhone = value; }
    }
    string ArchitectName;

    public string ArchitectName1
    {
        get { return ArchitectName; }
        set { ArchitectName = value; }
    }
    int ArchitectPhone;

    public int ArchitectPhone1
    {
        get { return ArchitectPhone; }
        set { ArchitectPhone = value; }
    }
    string SupervisorName;

    public string SupervisorName1
    {
        get { return SupervisorName; }
        set { SupervisorName = value; }
    }
    int SupervisorPhone;

    public int SupervisorPhone1
    {
        get { return SupervisorPhone; }
        set { SupervisorPhone = value; }
    }
    int Cid;

    public int Cid1
    {
        get { return Cid; }
        set { Cid = value; }
    }
    int Pid;

    public int Pid1
    {
        get { return Pid; }
        set { Pid = value; }
    }

}
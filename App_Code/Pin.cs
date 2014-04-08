using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Pin
/// </summary>
public class Pin
{
	public Pin()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    int pinID;
    public int PinID
    {
        get { return pinID; }
        set { pinID = value; }
    }

    double coordinateX;
    public double CoordinateX
    {
        get { return coordinateX; }
        set { coordinateX = value; }
    }

    double coordinateY;
    public double CoordinateY
    {
        get { return coordinateY; }
        set { coordinateY = value; }
    }

    string comment;
    public string Comment
    {
        get { return comment; }
        set { comment = value; }
    }

    string audioURL;
    public string AudioURL
    {
        get { return audioURL; }
        set { audioURL = value; }
    }

    string videoURL;
    public string VideoURL
    {
        get { return videoURL; }
        set { videoURL = value; }
    }
}
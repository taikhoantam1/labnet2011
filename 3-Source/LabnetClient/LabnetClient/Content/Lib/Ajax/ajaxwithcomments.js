/* Copyright 2007 Codevendor.com */

// For registering namespaces
var Namespace = {
    
    Register : function(_Name)
    {
        var o = window;
        var x = false;
        for (var a = _Name.split("."); a.length > 0;)
        {
            var s = a.shift();
            if(a.length==0){ if(o[s]){x=true;} }
            if(!o[s]){o[s]={};}
            o = o[s];
        }
        
        if(x){ return 1; }
    }
}

//Create Namespaces----------------------------------------
Namespace.Register("System.Net.Ajax");
//---------------------------------------------------------

//Enumeration-----------------
System.Net.Ajax.RequestMethod = { Get:"GET", Post:"POST" };
//----------------------------

//Handles page requests in a collection----------------
System.Net.Ajax.PageRequests = function(){ return {

    Requests : null,
    
    //Gets the type of object---------------------------------------------
    GetType : function(){ return "System.Net.Ajax.PageRequests"; },
    //--------------------------------------------------------------------
    
    //Initializer---------------------------------------------------------
    Init : function()
    {
        this.Requests = new Array();
        if(arguments[0].length==1){ this.Requests.push(arguments[0][0]); }
        return this; 
    },
    //--------------------------------------------------------------------
    
    //Adds Requests to the collection
    AddRequest : function()
    {
        if(arguments.length==0 || arguments[0].GetType()!="System.Net.Ajax.Request"){ return; }
        this.Requests.push(arguments[0]);            
    }
    
}.Init(arguments);}

//Single Page Request
System.Net.Ajax.Request = function(){ return {

    Method : null,
    URL : null,
    Params : null,
    Callback : null,
    Async : false,
    UserObject : null,
    
    //Gets the type of object---------------------------------------------
    GetType : function(){ return "System.Net.Ajax.Request"; },
    //--------------------------------------------------------------------
    
    //Initializer---------------------------------------------------------
    Init : function()
    {
        switch(arguments[0].length)
        {
            case 1 : this.Method = arguments[0][0]; break;
            case 2 : this.Method = arguments[0][0]; this.URL = arguments[0][1]; break;
            case 3 : this.Method = arguments[0][0]; this.URL = arguments[0][1]; this.Callback = arguments[0][2]; break;
            case 4 : this.Method = arguments[0][0]; this.URL = arguments[0][1]; this.Callback = arguments[0][2]; this.Async = arguments[0][3]; break;
            case 5 : this.Method = arguments[0][0]; this.URL = arguments[0][1]; this.Callback = arguments[0][2]; this.Async = arguments[0][3]; this.UserObject = arguments[0][4]; break;
        }
        
        this.Params = new Array();
        return this; 
    },
    //--------------------------------------------------------------------
    
    //Adds Parameters to the parameter array collection
    AddParam : function()
    {
        switch(arguments.length)
        {
            case 1 : this.Params.push(arguments[0]); break;
            case 2 : this.Params.push(new System.Net.Ajax.Parameter(arguments[0], arguments[1])); break;
        }
    }
    
}.Init(arguments);}

//Page Request Parameter Object
System.Net.Ajax.Parameter = function(){ return {

    Name : null,
    Value : null,

    //Gets the type of object---------------------------------------------
    GetType : function(){ return "System.Net.Ajax.Parameter"; },
    //--------------------------------------------------------------------
    
    //Initializer---------------------------------------------------------
    Init : function()
    { 
        if(arguments[0].length==2){ this.Name = arguments[0][0]; this.Value = arguments[0][1]; }
        return this; 
    }
    //--------------------------------------------------------------------

}.Init(arguments);}

System.Net.Ajax.ActiveObject = 0; //For knowing what type of active X object.

//For handling ajax connections
System.Net.Ajax.Connection = function(){ return {
    
    ActiveXObject : null,
    PageRequests : null,
    Current : null,
    
    //Gets the type of object---------------------------------------------
    GetType : function(){ return "System.Net.Ajax.Connection"; },
    //--------------------------------------------------------------------
    
    //Initializer---------------------------------------------------------
    Init : function()
    { 
        if(arguments[0].length==1){ this.PageRequests = arguments[0][0]; }
        return this; 
    },
    //--------------------------------------------------------------------
    
    //Creates the active x object for use
    Create : function()
    {
        switch(System.Net.Ajax.ActiveObject)
        {
            case 0:
                if(window.ActiveXObject)
                {
                    try 
                    { 
                        System.Net.Ajax.ActiveObject = 2;
                        return new ActiveXObject("Msxml2.XMLHTTP");        
                    } 
                    catch(e) 
                    {  
                        System.Net.Ajax.ActiveObject = 3;
                        return new ActiveXObject("Microsoft.XMLHTTP");
                    }
                }
                else
                {
                    if(window.XMLHttpRequest)
                    {
                        System.Net.Ajax.ActiveObject = 1;
                        return new XMLHttpRequest();
                    }
                }
            
            case 1: return new XMLHttpRequest();           
            case 2: return new ActiveXObject("Msxml2.XMLHTTP");           
            case 3: return new ActiveXObject("Microsoft.XMLHTTP");
            default: break;
        }
        
        //No Ajax Object Found-----------
        System.Net.Ajax.ActiveObject = -1;
        throw "Missing a required ajax object.";
        return false;
        //-------------------------------
    },
    
    Open : function()
    {
        //Check if page requests has something------
        if(this.PageRequests==null){ return; }
        //------------------------------------------
        
        //Create Variables--------------------------
        var obj = this;
        var Data = "";
        //------------------------------------------
        
        //Create ActiveX----------------------------
        this.ActiveXObject = this.Create();
        //------------------------------------------
        
        //Get Request-------------------------------
        this.Current = this.PageRequests.Requests.shift();
        //------------------------------------------
        
        //Open Connection---------------------------
        this.ActiveXObject.open(this.Current.Method, this.Current.URL, this.Current.Async);
        //------------------------------------------
        
        //Create ActiveX Callback-------------------
        this.ActiveXObject.onreadystatechange = function() {obj.OnReadyStateChange();}
        //------------------------------------------
        
        //Open Ajax Request-------------------------------------------------------------------
        if(this.Current.Method=="POST")
        {
            this.ActiveXObject.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
               
            if(this.Current.Params!=null && this.Current.Params.length!=0)
            {
                for(var Param in this.Current.Params) 
                {
                    Data += (Data=="") ? this.Current.Params[Param].Name + "=" + this.Current.Params[Param].Value : "&" + this.Current.Params[Param].Name + "=" + this.Current.Params[Param].Value;
                }
            }
            this.ActiveXObject.send(encodeURI(Data));
        }
        else
        {
            this.ActiveXObject.send(null);
        }
        //------------------------------------------------------------------------------------
    },
    
    //ActiveXObject callback
    OnReadyStateChange : function()
    {
        //Get Ajax objects for return-----------------
        var r = {};
        r.ReadyState = this.ActiveXObject.readyState;
        r.ResponseText = (this.ActiveXObject.readyState==4)?this.ActiveXObject.responseText:null;
        r.Status = (this.ActiveXObject.readyState==4)?this.ActiveXObject.status:null;
        r.URL = this.Current.URL;
        r.UserObject = this.Current.UserObject;
        r.Complete = (this.ActiveXObject.readyState==4 && this.PageRequests.Requests.length==0) ? true : false;
        //--------------------------------------------
        
        //Call Callback Method---------------
        if(this.Current.Callback!=null){this.Current.Callback(r);}
        //-----------------------------------
        
        //Loop For Many URLS
        if(this.ActiveXObject.readyState==4)
        { 
            if(r.Complete){ this.PageRequests=null; this.ActiveXObject.abort(); this.Current=null; }
            else{ this.Open();}
        }
    }
      
}.Init(arguments);}
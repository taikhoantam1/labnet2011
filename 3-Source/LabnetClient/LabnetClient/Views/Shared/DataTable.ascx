<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.JQGridModel>" %>
<link rel="stylesheet" type="text/css" media="screen" href="/Content/Style/redmond/jquery-ui-1.8.16.custom.css" />
<link rel="stylesheet" type="text/css" media="screen" href="/Content/Style/ui.jqgrid.css" />
<link href="/Content/Lib/jqGrid/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
<script src="/Content/Lib/jqGrid/i18n/grid.locale-en.js" type="text/javascript"></script>
<script type="text/javascript">
    jQuery.jgrid.no_legacy_api = false;
</script>
<script src="/Content/Lib/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
<script src="/Content/Lib/jqGrid/jqModal.js" type="text/javascript"></script>
<script src="/Content/Lib/jqGrid/jqDnR.js" type="text/javascript"></script>
<script src="/Content/Lib/postify.js" type="text/javascript"></script>
<script type="text/javascript">
    function DataTableGetDataSource_<%=Model.TableId %>()
    {
        var array=$("#<%=Model.TableId %>").jqGrid().getRowData();
        var obj=new Object();
        obj.Rows=array;
        return $.postify(obj);
    }

    function DataTableGetArrayDataSource_<%=Model.TableId %>()
    {
        var array=$("#<%=Model.TableId %>").jqGrid().getRowData();
        return array;
    }

    jQuery(document).ready(function () {
        var selICol_<%=Model.TableId %>; //index Col of selected cell
        var selRowIndex_<%=Model.TableId %>; //index Row of selected cell
        var selRowId_<%=Model.TableId %>;//Id of row selected
        var mydata_<%=Model.TableId %> = <%=Model.JsonDataArray %> 

        jQuery("#<%=Model.TableId %>").jqGrid({
        <%if(!string.IsNullOrEmpty(Model.RequestUrl)){ %> 
            url: '<%=Model.RequestUrl %>',
            datatype: 'json',
        <%}else{ %> 
            
            datatype: "local",
        <%} %>           
            jsonReader: {
                root: "rows",
                page: "CurrentPage",
                total: "Pages",
                records: "NumberRecords",
                repeatitems: true,
                cell: "cell",
                id: "id"
            },  
            autowidth: false,//When set to true, the grid width is recalculated automatically to the width of the parent element. This is done only initially when the grid is created. In order to resize the grid when the parent element changes width you should apply custom code and use a setGridWidth method for this purpose
            width:<%=Model.Width??700 %>,
            height:<%=Model.Height??300 %>,//The height of the grid. Can be set as number (in this case we mean pixels) or as percentage (only 100% is acceped) or value of auto is acceptable.
            mtype: 'GET',//Defines the type of request to make (“POST” or “GET”)
            colNames:[ <%=string.Join(",",Model.Columns.Select(p=>"'"+p.DisplayName+"'"))%>], //['Inv No', 'Date', 'Amount', 'Tax', 'Total', 'Notes'],
            colModel: [<%=Model.ColModelScript %>],
            cellsubmit: 'clientArray',
           
        <%if(string.IsNullOrEmpty(Model.RequestUrl)){ %> 
            loadonce: true,
        <%} %>  
            cellEdit     : <%=Model.AllowEdit?"true":"false" %>,
            beforeEditCell : function(rowid, cellname, value, iRow, iCol)
            {
                    
                selICol_<%=Model.TableId %> = iCol;
                selRowIndex_<%=Model.TableId %> = iRow;
                selRowId_<%=Model.TableId %>=rowid;
            },
            afterEditCell : function(rowid, cellname, value, iRow, iCol)
            {
                $("#list tr:eq("+iRow+") td:eq("+iCol+") input").select();
            },
            page:1,//Set the initial number of page when we make the request.This parameter is passed to the url for use by the server routine retrieving the data
            pager: $("#pager"),
            rowNum: 15,
            rowList: [10, 20, 30],
            sortname: 'id',
            sortorder: "desc",
            viewrecords: true,
            altRows: true,//Set a zebra-striped grid
            altclass: "odd",//The class that is used for alternate rows. You can construct your own class and replace this value. This option is valid only if altRows options is set to true
            imgpath: 'themes/basic/images',
            caption: '<%=Model.GridTitle%>',//Defines the Caption layer for the grid. This caption appears above the Header layer. If the string is empty the caption does not appear.
            forceFit:true,
            //idPrefix:"jqGrid",//When set this string is added as prefix to the id of the row when it is build
            loadtext:'Vui lòng chờ ...',
            //rownumbers:true,//f this option is set to true, a new column at left of the grid is added. The purpose of this column is to count the number of available rows, beginning from 1. In this case colModel is extended automatically with new element with name - 'rn'. Also, be careful not to use the name 'rn' in colModel
            scroll:false,//Creates dynamic scrolling grids. When enabled, the pager elements are disabled and we can use the vertical scrollbar to load data. When set to true the grid will always hold all the items from the start through to the latest point ever visited. When scroll is set to value (eg 1), the grid will just hold the visible lines. This allow us to load the data at portions whitout to care about the memory leaks. Additionally this we have optional extension to the server protocol: npage (see prmNames array). If you set the npage option in prmNames, then the grid will sometimes request more than one page at a time, if not it will just perform multiple gets.
            sortable:true,
            toolbar:[true,"both"],//This option defines the toolbar of the grid. This is array with two values in which the first value enables the toolbar and the second defines the position relative to body Layer. Possible values “top”,”bottom”, “both”. When we set toolbar: [true,”both”] two toolbars are created – one on the top of table data and one of the bottom of the table data. When we have two toolbars then we create two elements (div). The id of the top bar is constructed like “t_”+id of the grid and the bottom toolbar the id is “tb_”+id of the grid. In case when only one toolbar is created we have the id as “t_” + id of the grid, independent of where this toolbar is created (top or bottom)
               
        })/*.navGrid('#pager', { view: true, del: true, add: true, edit: true,  cloneToTop:true},
                {}, // default settings for edit
                {}, // default settings for add
                {}, // delete
                {
                  
                closeOnEscape: true, 
                multipleSearch: true, 
                closeAfterSearch: true }, // search options
                {}
        );*/
        
        function editlink_<%=Model.TableId %>( cellvalue, options, rowObject ){
	        return "<a class='editlink_<%=Model.TableId %> editLink' href='"+cellvalue+"' title='Cập Nhật'></a>";
        }
        
        function deletelink_<%=Model.TableId %>( cellvalue, options, rowObject ){
	        return "<a class='deletelink_<%=Model.TableId %> deleteLink'  href='"+cellvalue+"' title='Xóa'></a>";
        }

        jQuery("#<%=Model.TableId %>").jqGrid('filterToolbar',{stringResult: true,searchOnEnter : false});
        
        <%if(Model.UseLocalData){ %>
            for(var i=0;i<=mydata_<%=Model.TableId %>.length;i++)
	            jQuery("#<%=Model.TableId %>").jqGrid('addRowData',i+1,mydata_<%=Model.TableId %>[i]);
        <% }%>


        $("#DataTableSaveButton_<%=Model.TableId %>").click(function(){
            var array=$("#<%=Model.TableId %>").jqGrid().getRowData();
            var obj=new Object();
            obj.Rows=array;
            $.ajax({
                url:"<%= Model.PostBackUrl %>",
                data: $.postify(obj),
                type:"POST",
                async:false,
                success:function(data){
                    if(data.trim().toLowerCase().indexOf("success")==-1)
                        alert(data);
                }
            });
        });
            
    });  

</script>
<input type="button" class="hide" id="DataTableSaveButton_<%=Model.TableId %>" />
<table id="<%=Model.TableId %>" class="scroll">
</table>
<div id="pager" class="scroll" style="text-align: center;">
</div>

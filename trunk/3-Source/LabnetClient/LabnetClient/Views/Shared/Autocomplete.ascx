<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.AutocompleteModel>" %>
<script type="text/javascript">
    function AutocompleSelect(id,label,tag)
    {
    }
    $(document).ready(function () {
    <%if(Model.UseAjaxLoading){ %>
       var input= $("#<%= Model.AutoCompleteId %> .autoComplete").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "<%=Model.GetDataUrl %>", 
                    type: "POST", 
                    dataType: "json",
                    data: { 
                            searchText: request.term ,
                            searchType:"word"
                            //searchType:"contains"
                        },
                    success: function (data) {
                        response($.map(data, function (item) {
                                return { label: item.Label, id: item.Value ,tag: item.Tag }
                        }))
                    }
                })
            },
            select: function (event, ui) {
                $("#<%= Model.AutoCompleteId %> .autoCompleteBindingValue").val(ui.item.id);
                $("#<%= Model.AutoCompleteId %>_SelectedValue").val(ui.item.id);
                $("#<%= Model.AutoCompleteId %>_SelectedText").val(ui.item.label);
                $("#<%= Model.AutoCompleteId %>_SelectedTag").val(ui.item.tag);
            },
            delay:0,
            selectFirst: true,
            change: function(event, ui) {
                var matcher = new RegExp( "^" + $.ui.autocomplete.escapeRegex( $(this).val() ) + "$", "i" ),
                valid = false;
                $("ul.ui-autocomplete li.ui-menu-item a.ui-corner-all").each(function() {
                    if ( $(this).html().match( matcher ) ) {
                        valid = true;
                        return false;
                    }
                });
                if ( !valid ) {
                    // remove invalid value, as it didn't match anything
                    $( this ).val( "" );
                    $("#<%= Model.AutoCompleteId %> .autoCompleteBindingValue").val("");
                    $("#<%= Model.AutoCompleteId %>_SelectedValue").val("");
                    $("#<%= Model.AutoCompleteId %>_SelectedText").val("");
                    $("#<%= Model.AutoCompleteId %>_SelectedTag").val("");	
                     $("#<%= Model.AutoCompleteId %> .autoCompleteText").data( "autocomplete" ).term = "";
                    return false;
                }
                AutocompleChange(ui.item.id,ui.item.label,ui.item.tag);
            }
        })
		.addClass("ui-widget ui-widget-content ui-corner-left")
        .css({'padding':'5px','border-radius': '0px','margin': '0px 3px 0px 0px'}).val("");
    <%}%>
    <%else {%>
        var autoCompleteData =<%=Model.JsonData %>;
      var input= $("#<%= Model.AutoCompleteId %> .autoComplete").autocomplete({
            source: function (req, responseFn) {
                            var re = $.ui.autocomplete.escapeRegex($.fn.nomalizeString(req.term));
                           // var matcher = new RegExp( "[*]?" + re, "i" ); // Search contains
                            var matcher = new RegExp( "^" + re, "i" );//Search by word
                            var a = $.map( autoCompleteData, function(item,index){
                               var label=$.fn.nomalizeString(item.Label);
                                if(matcher.test(label)|| re.toString().length==0)
                                    return { label: item.Label, id: item.Value,tag: item.Tag  }
                            });
                            responseFn( a );
                    },
            select: function (event, ui) {
                $("#<%= Model.AutoCompleteId %> .autoCompleteBindingValue").val(ui.item.id);
                $("#<%= Model.AutoCompleteId %>_SelectedValue").val(ui.item.id);
                $("#<%= Model.AutoCompleteId %>_SelectedText").val(ui.item.label);
                $("#<%= Model.AutoCompleteId %>_SelectedTag").val(ui.item.tag);
                AutocompleSelect(ui.item.id,ui.item.label,ui.item.tag);
            },
            minLength:0,
            delay:0,
            selectFirst: true,
            change: function(event, ui) {
                var matcher = new RegExp( "^" + $.ui.autocomplete.escapeRegex( $(this).val() ) + "$", "i" ),
                valid = false;
                $("ul.ui-autocomplete li.ui-menu-item a.ui-corner-all").each(function() {
                    if ( $(this).html().trim().match( matcher ) ) {
                        valid = true;
                        return false;
                    }
                });
                if ( !valid ) {
                    // remove invalid value, as it didn't match anything
                    $( this ).val( "" );
                    $("#<%= Model.AutoCompleteId %> .autoCompleteBindingValue").val("");
                    $("#<%= Model.AutoCompleteId %>_SelectedValue").val("");
                    $("#<%= Model.AutoCompleteId %>_SelectedText").val("");
                    $("#<%= Model.AutoCompleteId %>_SelectedTag").val("");	
                     $("#<%= Model.AutoCompleteId %> .autoCompleteText").data( "autocomplete" ).term = "";
                    return false;
                }
                   
            }
        }).focus(function(){  
            $(this).autocomplete( "search", "" );			
        })
		.addClass("ui-widget ui-widget-content ui-corner-left")
        .css({'padding':'5px','border-radius': '0px','margin': '0px 3px 0px 0px'}).val("");

    <%} %>
        input.keyup(function(event){
            if(input.val()=="")
            {
                $("#<%= Model.AutoCompleteId %> .autoCompleteBindingValue").val("");
                $("#<%= Model.AutoCompleteId %>_SelectedValue").val("");
                $("#<%= Model.AutoCompleteId %>_SelectedText").val("");
                $("#<%= Model.AutoCompleteId %>_SelectedTag").val("");
            }
        });
    });
</script>
<div id="<%= Model.AutoCompleteId %>" class="autoCompleteContainer">
    <input type="text" class="autoComplete <%= Model.CustomeCss %> textInput220" value="<%= Model.SelectedText %>" />
    <input type="hidden" class="autoCompleteBindingValue" value="<%= Model.SelectedValue %>" name="<%=Model.BindingName%>" />
    <input type="hidden" class="autoCompleteTag" id="<%= Model.AutoCompleteId %>_SelectedTag" value="<%= Model.SelectedTag %>" name="<%=Model.SelectedTag%>" />
    <input type="hidden" class="autoCompleteValue" id="<%= Model.AutoCompleteId %>_SelectedValue" value="<%= Model.SelectedValue %>" name="Autocomplete.SelectedValue" />
    <input type="hidden" class="autoCompleteText" id="<%= Model.AutoCompleteId %>_SelectedText" value="<%= Model.SelectedText %>" name="Autocomplete.SelectedText" />
</div>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.AutocompleteModel>" %>
<script type="text/javascript">
    $(document).ready(function () {
    <%if(Model.IsAjaxLoading){ %>
        $("#<%= Model.AutoCompleteId %> .autoCompleteText").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "<%=Model.ActionLink %>", 
                    type: "POST", 
                    dataType: "json",
                    data: { searchText: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                                return { label: item.Label, id: item.Value }
                        }))
                    }
                })
            },
            select: function (event, ui) {
                $("#<%= Model.AutoCompleteId %> .autoCompleteValue").val(ui.item.id);
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
                    return false;
                }
            }
        });
    <%}%>
    <%else {%>
        $.data($("#<%= Model.AutoCompleteId %>"),'<%=Model.JsonData %>');
        var availableTags =<%=Model.JsonData %>;
        $("#<%= Model.AutoCompleteId %> .autoCompleteText").autocomplete({
            source: function (req, responseFn) {
                            var re = $.ui.autocomplete.escapeRegex($.fn.nomalizeString(req.term));
                            var matcher = new RegExp( "^" + re, "i" );
                            var a = $.map( availableTags, function(item,index){
                               var label=$.fn.nomalizeString(item.Label);
                                if(matcher.test(label))
                                    return { label: item.Label, id: item.Value }
                            });
                            responseFn( a );
                    },
            select: function (event, ui) {
                $("#<%= Model.AutoCompleteId %> .autoCompleteValue").val(ui.item.id);
            },
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
                    return false;
                }
                   
            }
        });
    <%} %>
    });
</script>
<div id="<%= Model.AutoCompleteId %>" class="autoCompleteContainer">
    <input type="text" class="autoCompleteText <%= Model.CustomeCss %>" />
    <input type="hidden" class="autoCompleteValue <%= Model.CustomeCss %>" name="<%=Model.BindingName%>" />
</div>

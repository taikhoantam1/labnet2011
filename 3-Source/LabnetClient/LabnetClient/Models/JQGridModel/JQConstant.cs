using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabnetClient.Models
{
    public class JQConstant
    {

        public const string columnTemplate = @" name: '{0}', index: '{1}', width: {2} , editable:{3}, edittype:'{4}',hidden:{5} ";
        public const string EditOptions = @",editoptions: //Place this code in the col options of the last column in your grid  it listens for the tab button being pressed
        {
            // dataInit : function (elem) { $(elem).focus(function(){  this.select();}) },
            dataEvents: [
                { 
                    type: 'keydown', 
                    fn: function(e) { 
                        var key = e.charCode || e.keyCode;
                        if(key == 9)   // tab
                        {
                            var nextCol=0;
                            var colModels=jQuery('#list').jqGrid ('getGridParam', 'colModel');
                            for(var i=0;i<colModels.length;i++)
                            {
                                if(colModels[i].editable===true &&colModels[i].hidden===false)
                                {
                                    nextCol=i;
                                    break;
                                }
                            }
                            setTimeout(&#34;$('#list').editCell(&#34; + selRowIndex + &#34; + 1,&#34; +nextCol+&#34;, true);&#34;, 100);
                        }
                    }
                } ]
        }";
    }
}
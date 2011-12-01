  <script type='text/javascript'>
        jQuery(document).ready(function () {

            jQuery('#JQGrid1').jqGrid({
                url: '/grid/SearchGridDataRequested?jqGridID=JQGrid1',
                editurl: '/grid/EditRows?jqGridID=JQGrid1&editMode=1',
                mtype: 'GET',
                datatype: 'json',
                page: 1,
                colNames: ["OrderID", "OrderDate", "CustomerID", "Freight", "ShipName"],
                colModel: [
                    {
                        "search": true,
                        "key": true,
                        "index": "OrderID",
                        "width": 50,
                        "name": "OrderID"
                    },
                    {
                        "search": false,
                        "editable": true,
                        "index": "OrderDate",
                        "width": 100,
                        "name": "OrderDate"
                    },
                    {
                        "editoptions": { "value": "ALFKI:ALFKI;ANATR:ANATR;ANTON:ANTON;AROUT:AROUT;BERGS:BERGS;BLAUS:BLAUS;BLONP:BLONP;BOLID:BOLID;BONAP:BONAP;BSBEV:BSBEV;CACTU:CACTU;CENTC:CENTC;CHOPS:CHOPS;CONSH:CONSH;DRACD:DRACD;DUMON:DUMON;EASTC:EASTC;ERNSH:ERNSH;FISSA:FISSA;FOLIG:FOLIG;FOLKO:FOLKO;FRANK:FRANK;FRANR:FRANR;FRANS:FRANS;FURIB:FURIB;GALED:GALED;GODOS:GODOS;KOENE:KOENE;LACOR:LACOR;LAMAI:LAMAI;LEHMS:LEHMS;MAGAA:MAGAA;MAISD:MAISD;MORGK:MORGK;NORTS:NORTS;OCEAN:OCEAN;OTTIK:OTTIK;PARIS:PARIS;PERIC:PERIC;PICCO:PICCO;PRINI:PRINI;QUICK:QUICK;RANCH:RANCH;REGGC:REGGC;RICSU:RICSU;ROMEY:ROMEY;SANTG:SANTG;SEVES:SEVES;SIMOB:SIMOB;SPECD:SPECD;SUPRD:SUPRD;TOMSP:TOMSP;TORTU:TORTU;VAFFE:VAFFE;VICTE:VICTE;VINET:VINET;WANDK:WANDK;WARTH:WARTH;WILMK:WILMK;WOLZA:WOLZA;OLDWO:OLDWO;BOTTM:BOTTM;LAUGB:LAUGB;LETSS:LETSS;HUNGO:HUNGO;GROSR:GROSR;SAVEA:SAVEA;ISLAT:ISLAT;LILAS:LILAS;THECR:THECR;RATTC:RATTC;LINOD:LINOD;GREAL:GREAL;HUNGC:HUNGC;LONEP:LONEP;THEBI:THEBI;MEREP:MEREP;HANAR:HANAR;QUEDE:QUEDE;RICAR:RICAR;COMMI:COMMI;FAMIA:FAMIA;GOURL:GOURL;QUEEN:QUEEN;TRADH:TRADH;WELLI:WELLI;HILAA:HILAA;LAZYK:LAZYK;TRAIH:TRAIH;WHITC:WHITC;SPLIR:SPLIR" },
                        "editable": true,
                        "searchoptions": { "value": ":All;ALFKI:ALFKI;ANATR:ANATR;ANTON:ANTON;AROUT:AROUT;BERGS:BERGS;BLAUS:BLAUS;BLONP:BLONP;BOLID:BOLID;BONAP:BONAP;BSBEV:BSBEV;CACTU:CACTU;CENTC:CENTC;CHOPS:CHOPS;CONSH:CONSH;DRACD:DRACD;DUMON:DUMON;EASTC:EASTC;ERNSH:ERNSH;FISSA:FISSA;FOLIG:FOLIG;FOLKO:FOLKO;FRANK:FRANK;FRANR:FRANR;FRANS:FRANS;FURIB:FURIB;GALED:GALED;GODOS:GODOS;KOENE:KOENE;LACOR:LACOR;LAMAI:LAMAI;LEHMS:LEHMS;MAGAA:MAGAA;MAISD:MAISD;MORGK:MORGK;NORTS:NORTS;OCEAN:OCEAN;OTTIK:OTTIK;PARIS:PARIS;PERIC:PERIC;PICCO:PICCO;PRINI:PRINI;QUICK:QUICK;RANCH:RANCH;REGGC:REGGC;RICSU:RICSU;ROMEY:ROMEY;SANTG:SANTG;SEVES:SEVES;SIMOB:SIMOB;SPECD:SPECD;SUPRD:SUPRD;TOMSP:TOMSP;TORTU:TORTU;VAFFE:VAFFE;VICTE:VICTE;VINET:VINET;WANDK:WANDK;WARTH:WARTH;WILMK:WILMK;WOLZA:WOLZA;OLDWO:OLDWO;BOTTM:BOTTM;LAUGB:LAUGB;LETSS:LETSS;HUNGO:HUNGO;GROSR:GROSR;SAVEA:SAVEA;ISLAT:ISLAT;LILAS:LILAS;THECR:THECR;RATTC:RATTC;LINOD:LINOD;GREAL:GREAL;HUNGC:HUNGC;LONEP:LONEP;THEBI:THEBI;MEREP:MEREP;HANAR:HANAR;QUEDE:QUEDE;RICAR:RICAR;COMMI:COMMI;FAMIA:FAMIA;GOURL:GOURL;QUEEN:QUEEN;TRADH:TRADH;WELLI:WELLI;HILAA:HILAA;LAZYK:LAZYK;TRAIH:TRAIH;WHITC:WHITC;SPLIR:SPLIR" },
                        "width": 100,
                        "edittype": "select",
                        "stype": "select",
                        "name": "CustomerID",
                        "index": "CustomerID"
                    },
                    {
                        "editable": true,
                        "searchoptions": { "value": ":All;10:\u003e 10;30:\u003e 30;50:\u003e 50;100:\u003e 100" },
                        "width": 75,
                        "stype": "select",
                        "name": "Freight",
                        "index": "Freight"
                    },
                    {
                        "editable": true,
                        "index": "ShipName",
                        "searchoptions": {},
                        "name": "ShipName"
                    }
                ],
                viewrecords: true,
                scrollrows: false,
                prmNames: { id: "OrderID" },
                pager: jQuery('#JQGrid1_pager'),
                loadError: jqGrid_aspnet_loadErrorHandler,
                hoverrows: false,
                rowNum: 10,
                rowList: [10, 20, 30],
                editDialogOptions: 
                {
                    "closeAfterEdit": true,
                    "recreateForm": true,
                    errorTextFormat: function (data) { return 'Error: ' + data.responseText },
                    editData: { __RequestVerificationToken: jQuery('input[name=__RequestVerificationToken]').val() }
                },
                addDialogOptions: 
                {
                    "closeAfterAdd": true,
                    "recreateForm": true,
                    errorTextFormat: function (data) { return 'Error: ' + data.responseText },
                    editData: { __RequestVerificationToken: jQuery('input[name=__RequestVerificationToken]').val() }
                },
                delDialogOptions:
                {
                    "recreateForm": true,
                    errorTextFormat: function (data) { return 'Error: ' + data.responseText },
                    delData: { __RequestVerificationToken: jQuery('input[name=__RequestVerificationToken]').val() }
                },
                searchDialogOptions: { "resize": false, "recreateForm": true },
                jsonReader: { id: "OrderID" },
                sortorder: 'asc',
                width: '640',
                viewsortcols: [false, 'vertical', true]
            })
            .navGrid(
                '#JQGrid1_pager',
                { "edit": true, "add": true, "del": true, "search": false, "refresh": true, "view": false, "position": "left", "cloneToTop": true },
                jQuery('#JQGrid1').getGridParam('editDialogOptions'),
                jQuery('#JQGrid1').getGridParam('addDialogOptions'),
                jQuery('#JQGrid1').getGridParam('delDialogOptions'),
                jQuery('#JQGrid1').getGridParam('searchDialogOptions')
            ).bindKeys();

            function jqGrid_aspnet_loadErrorHandler(xht, st, handler) {
                jQuery(document.body).css('font-size', '100%');
                jQuery(document.body).html(xht.responseText);
            };
            jQuery('#JQGrid1').filterToolbar({});
        });
    </script>
!function (window, $) {
    'use strict';
    var LookupModal = function (options) {
        this._template = '<div class="modal fade" tabindex="-1" role="dialog" style="display: none">' +
                    '<div class="modal-header">' +
                        '<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>' +
				        '<h4 class="modal-title">Responsive</h4>' +
			        '</div><div class="modal-body"></div>' +
			        '<div class="modal-footer">' +
				        '<button type="button" data-dismiss="modal" class="btn btn-default">Close</button>' +
				        '<button type="button" class="btn blue">OK</button></div>' +
        '</div><!-- /.modal -->';
        
        this._$elem = $(this._template);
        
        this._defaultOptions = {
            width: 760
        };

        options = $.extend(this._defaultOptions, options);

        this._init.call(this, options);
    };

    // Psedo constructor
    LookupModal.prototype._init = function (options) {
        var elem = this._$elem;
        this._$elem.modal(options);
        var lookup_body = $(".modal-body:first", elem)
            .append("<div id='lookup_filter' class='lookup-filter'></div>" +
            "<div id='lookup_grid' class='lookup-grid'></div>");

        var lookup_filter = $(".lookup-filter:first", lookup_body)
            .append("<input type='text' class='lookup-search'/>" +
            "<button class='lookup-search'>v Search</button>");
        var lookup_grid = $(".lookup-grid:first", lookup_body);
        var txtSearch = lookup_filter.find("input:first");
        var btnSearch = lookup_filter.find("button:first");
        
        //Get infomation for grid
        var textbox = $(options.target);
        var vVar = textbox.attr("vvar");
        var initFilter = GetInitFilter(textbox);//Chua viet ham
        function GetInitFilter(txt) {
            return txt.attr("initfilter");
        }

        //alert(vVar);
        //vi du da lay duoc thong tin
        var look_fields = ["ContactName", "ContactTitle", "CompanyName", "Country"];
        var look_titles = ["Mã KH", "Tên khách hàng", "Địa chỉ", "Mã số thuế"];
        var look_widths = [100, 250, 300, 150];
        var columns = [];
        for (var i = 0; i < look_fields.length; i++) {
            columns.push({ field: look_fields[i], title: look_titles[i], width: look_widths[i] });
        }

        elem.find(".btn.blue").click(function(e) {
            //Get selected value (s)
            var selected_value = "selected";
            //Set target value
            textbox.val(selected_value);
            //Close grid

        });


        lookup_grid.kendoGrid({
            dataSource: {
                type: "odata",
                transport: {
                    read: "//demos.telerik.com/kendo-ui/service/Northwind.svc/Customers"
                },
                pageSize: 20
            },
            height: 550,
            groupable: true,
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: columns,
            //    [{
            //    template: "<div class='customer-photo'" +
            //                    "style='background-image: url(../content/web/Customers/#:data.CustomerID#.jpg);'></div>" +
            //                "<div class='customer-name'>#: ContactName #</div>",
            //    field: "ContactName",
            //    title: "Contact Name",
            //    width: 240
            //}, {
            //    field: "ContactTitle",
            //    title: "Contact Title"
            //}, {
            //    field: "CompanyName",
            //    title: "Company Name"
            //}, {
            //    field: "Country",
            //    width: 150
            //}],
            selectable: "row"
        });

    };


    /*
    * Static function(s)
    */

    LookupModal.destroy = function () {
        LookupModal = null;
        delete window.LookupModal;
    };

    window.LookupModal = LookupModal;
}(window, jQuery);
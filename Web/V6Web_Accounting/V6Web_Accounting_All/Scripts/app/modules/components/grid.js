(function (module) {
    module.controller("ComponentGridController", ComponentGridController);
    ComponentGridController.$inject = ["$scope", "KendoGridOptionsFactory"];

    function ComponentGridController($scope, KendoGridOptionsFactory) {
        var model = this;
        model.selectedItem = null;
        model.testdata = [
            { id: 1, name: "Duy Lam", age: 27, birthday: new Date(1988, 9, 15), gender: 'male' },
            { id: 2, name: "Tuấn Cún", age: 27, birthday: new Date(1988, 8, 15), gender: 'pede' },
            { id: 3, name: "Huy Béo", age: 27, birthday: new Date(1988, 9, 12), gender: 'female' },
            { id: 4, name: "Ngọc kave", age: 27, birthday: new Date(1984, 9, 15), gender: 'male' }
        ];

        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: new kendo.data.DataSource({
                data: model.testdata,
                schema: {
                    model: {
                        id: "id"
                    }
                }
            }),
           
            change: function () {
                var selectedRow = this.select();
                model.selectedItem = this.dataItem(selectedRow[0]);
                $scope.$apply();
            },
            remove: function (e) {
                console.log("Removing", e.model.name);
                model.testdata.splice(0, 4);
                this.dataItems(model.testdata);
            },
            columns: [
                {
                    field: "name",
                    title: "Name",
                },
                {
                    field: "age",
                    title: "Age",
                }, {
                    field: "birthday",
                    title: "Birthday",
                    template: '#: kendo.toString( birthday , "D") #',
                    type: "date"
                }, {
                    field: "gender",
                    title: "Gender",
                    editor: genderEditor
                },
                { command: ["edit", "destroy"] }
            ]
        });

        var genderData = ["male", "female", "pede", "gay"];

        model.genderDataOptions = {
            dataSource: new kendo.data.DataSource({
                data: genderData
            }),
        };

        function genderEditor(container, options) {
            $('<select kendo-combo-box options="model.genderDataOptions" data-bind="value:' + options.field + '"></select>')
                .appendTo(container);
        }

        
    }
})(angular.module("app.component"))
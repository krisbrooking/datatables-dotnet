(function (module) {

    function tableController($scope, DTOptionsBuilder, DTColumnBuilder) {

        $scope.dtOptions = DTOptionsBuilder
            .newOptions()
            .withOption('ajax', {
                dataSrc: "data",
                url: '/api/users',
                type: "GET"
            })
            .withDataProp('data') // This is the name of the value in the returned recordset which contains the actual data
            .withOption('processing', true)
            .withOption('serverSide', true)
            .withOption('paging', true)
            .withOption('autoWidth', false);

        $scope.dtColumns = [
            DTColumnBuilder.newColumn('id'),
            DTColumnBuilder.newColumn('name')
        ];
    };

    module.controller("tableController", ["$scope", "DTOptionsBuilder", "DTColumnBuilder", tableController]);

}(angular.module("datatables-queryable")));
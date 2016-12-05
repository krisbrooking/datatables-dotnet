(function () {
    function configState($routeProvider, $compileProvider) {

        $compileProvider.debugInfoEnabled(true);

        $routeProvider

            .when('/', {
                templateUrl: "datatable.html"
            })
    };

    angular.module('datatables-queryable', ['ngRoute', 'datatables'])

    .config(['$routeProvider', '$compileProvider', configState])
})();
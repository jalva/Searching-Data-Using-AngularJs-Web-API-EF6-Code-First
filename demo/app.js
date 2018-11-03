var app = angular.module('searchFiltersApp', ['ngRoute']);

app.config(function ($routeProvider) {
    
    $routeProvider.when("", {
        redirectTo: "/book"
    });
    $routeProvider.when("/", {
        redirectTo: "/book"
    });
    $routeProvider.when("/book", {
        template: '<search-root columns="$resolve.columns"></search-root>',
        resolve: {
            columns: function (schemaService) {
                return schemaService.getColumns();
            }
        },
        reloadOnSearch: false
    });

});

app.constant('searchUrl', '../../demo/api/books.json');
app.constant('getFiltersUrl', '../../demo/api/filters.json');
app.constant('getColumnsUrl', '../../demo/api/schema.json');


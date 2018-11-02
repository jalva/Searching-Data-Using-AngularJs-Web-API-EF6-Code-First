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

app.constant('searchUrl', '/Search-with-Filters-in-AngularJs-Web-API-EF6-Code-First/demo/api/books.json');
app.constant('getFiltersUrl', '/Search-with-Filters-in-AngularJs-Web-API-EF6-Code-First/demo/api/filters.json');
app.constant('getColumnsUrl', '/Search-with-Filters-in-AngularJs-Web-API-EF6-Code-First/demo/api/schema.json');


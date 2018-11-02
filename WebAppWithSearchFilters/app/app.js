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

app.constant('searchUrl', '/api/book');
app.constant('getFiltersUrl', '/api/bookFilter');
app.constant('getColumnsUrl', '/api/bookSchema');


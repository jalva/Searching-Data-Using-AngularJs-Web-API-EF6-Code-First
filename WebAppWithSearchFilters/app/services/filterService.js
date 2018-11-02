'use strict';
app.service('filterService', [
    '$http', 'getFiltersUrl', function ($http, getFiltersUrl) {

        this.getSearchFilters = function (qs) {
            return $http.get(getFiltersUrl + '?' + qs);
        }

    }
]);